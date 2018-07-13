using UnityEditor;
using UnityEngine;

namespace STRV.Variables.Persistance
{
    [CustomEditor(typeof(VariablePersistor))]
    public class VariablePersistorEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            
            VariablePersistor persistor = (VariablePersistor) target;

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Save"))
            {
                persistor.Save();
            }
            if(GUILayout.Button("Load"))
            {
                persistor.Load();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}