using UnityEngine;
using UnityEditor;
using STRV.Variables.Utils;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif
using System;
using System.Linq;
using STRV.Variables.Persistance;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(Variable<>))]
    public class VariableDrawer<T> : Editor
    {
        private Variable<T> _target;
        private PropertyField[] _fields;

        private IDisposable _disposable;

        private VariablePersistor _persistor;
        
        public void OnEnable() {
            _target = target as Variable<T>;
            _fields = ExposeProperties.GetProperties(_target);
            
#if REACTIVE_VARIABLE_RX_ENABLED
            _disposable = _target.AsObservable().Subscribe(value => {
                Repaint();
            });
#else
            _target.OnValueChanged += HandleValueChanged;
#endif

            _persistor = GetPersistor();
        }
        
        private VariablePersistor GetPersistor()
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:"+ typeof(VariablePersistor).Name);  //FindAssets uses tags check documentation for more info
            VariablePersistor[] a = new VariablePersistor[guids.Length];
            for(int i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<VariablePersistor>(path);
            }
 
            return a.First();
#else
            return null;
#endif
        }

        public void OnDisable() {
#if REACTIVE_VARIABLE_RX_ENABLED
            if (_disposable != null) {
                _disposable.Dispose();
                _disposable = null;
            }
#else
            _target.OnValueChanged -= HandleValueChanged;
#endif
        }

        private void HandleValueChanged(T value)
        {
            Repaint();
        }

        public override void OnInspectorGUI() {

            if (_target == null)
                return;
            
            // Draw script
            serializedObject.Update();
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_persistenceKey"), true);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Persisted:");
            if (_persistor.TestOnly_GetItems().Contains(target))
            {
                GUILayout.Label("TRUE",  EditorStyles.boldLabel);    
            }
            else
            {
                GUILayout.Label("FALSE",  EditorStyles.boldLabel);
            }
            EditorGUILayout.EndHorizontal();
            
            if (_target.SupportsRemoteSettings())
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("RemoteSettingsVariable"), true);  
            }
            else
            {
                _target.RemoteSettingsVariable = false;
            }

            if (_target.RemoteSettingsVariable)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("RemoteSettingsId"), true);
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("DeveloperDescription"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultValue"), true);
            ExposeProperties.Expose(_fields); // Current value

            if (_target != null && _target.CurrentValue != null && _target.DefaultValue != null && !_target.CurrentValue.Equals(_target.DefaultValue)) {
                if (GUILayout.Button("Current -> Default")) {
                    _target.DefaultValue = _target.CurrentValue;
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}