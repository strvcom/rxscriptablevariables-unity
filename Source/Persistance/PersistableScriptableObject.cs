using System;
using STRV.Variables.Persistance;
using UnityEngine;

namespace Variables.Source.Persistance
{
    public abstract class PersistableScriptableObject: ScriptableObject, ISerializable
    {
        /// if this is set to true, initialisation of the variable with default value will not be performed
        /// This is mainly used by the persistor to prevent overriding loaded value with default value but it can also be used by programmer with a good reason to do so 
        [NonSerialized]
        public bool SkipDefaultValueReset = false;
        
        public abstract string GetKey();

        public abstract string GetStringValue();

        public abstract void SetStringValue(string value);
    }
}