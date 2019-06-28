using System;
using System.Collections.Generic;
using STRV.Variables.Utils;
using UnityEngine.Assertions;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables.Persistance
{
    /// Purpose of this class is to persist given values (scriptable variables)
    [CreateAssetMenu(menuName = "Variables/Peristance/Persistor", order = 1)]
    public class VariablePersistor: ScriptableObject, ISerializable
    {
        [SerializeField] private string _filename;
        
        [Header("To be saved:")]
        [SerializeField] private List<Variable> _serializedItems;

        [Header("Destination:")]
        [SerializeField] private ValueSerializer _serializer;
        
        private void OnEnable()
        {
            Load();
        }

        public List<Variable> TestOnly_GetItems()
        {
            return _serializedItems;
        }

        public void Save()
        {
            Debug.Log("<color=#4169E1>Persistor</color> - Saving data to persistent storage");
            if (_serializer != null)
            {
                if (!_serializer.Save(this))
                {
                    Debug.LogError("<color=#4169E1>Persistor</color> - Save was unsuccesfull!");
                }
            }
            else
            {
                Debug.LogError("<color=#4169E1>Persistor</color> - Serializer is missing, please assing one");
            }
        }

        public void Load()
        {
            Debug.Log("<color=#4169E1>Persistor</color> - Loading data from persistent storage");
            if (_serializer != null)
            {
                if (!_serializer.Load(this))
                {
                    Debug.LogWarning("<color=#4169E1>Persistor</color> - Load was unsuccesfull, this might be because no save file was created previously");
                }
            }
            else
            {
                Debug.LogError("<color=#4169E1>Persistor</color> - Serializer is missing, please assing one");
            }
        }

        // Create string value to be serialized
        public string GetKey()
        {
            return _filename;
        }

        public string GetStringValue()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in _serializedItems)
            {
                dictionary[item.GetKey()] = item.GetStringValue();
            }
            
            return JsonHelper.DictionaryToJson(dictionary);
        }

        // Take loaded string value and fill it into the variables
        public void SetStringValue(string value)
        {
            var values = JsonHelper.DictionaryFromJson(value);
            foreach (var item in _serializedItems)
            {
                var key = item.GetKey();
                if (values.ContainsKey(key))
                {
                    try
                    {
                        item.SetStringValue(values[key]);
                        item.SkipDefaultValueReset = true;
                    }
                    catch (FormatException e)
                    {
                        Debug.LogErrorFormat("<color=#4169E1>Persistor</color> - Problem loading variable \"{0}\", exception: {1}", key, e);
                    }
                }
                else
                {
                    Debug.LogWarningFormat("<color=#4169E1>Persistor</color> - Item \"{0}\" not found in persistent storage", key);
                }
            }
        }
    }
}