using System;
using System.IO;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables.Persistance
{
    [CreateAssetMenu(menuName = "Variables/Peristance/Persistent Data Serializer", order = 13)]
    public class PersistentDataSerializer: ValueSerializer
    {
        public override bool Load(ISerializable target)
        {
            var path = DataPathForFilename(target.GetKey());
            Debug.LogFormat("<color=#4169E1>Persistor</color> - Loading data from \"{0}\"", path);
            if (File.Exists(path))
            {
                try
                {
                    var textValue = File.ReadAllText(path);
                    target.SetStringValue(textValue);
                    return true;
                }
                catch (IOException e)
                {
                    Debug.LogError(e);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override bool Save(ISerializable value)
        {
            var path = DataPathForFilename(value.GetKey());
            Debug.LogFormat("<color=#4169E1>Persistor</color> - Saving data to \"{0}\"", path);
            try
            {
                File.WriteAllText(path, value.GetStringValue());
                return true;
            }
            catch (IOException e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        private string DataPathForFilename(string filename)
        {
            return Path.Combine(Application.persistentDataPath, filename);
        }
    }
}