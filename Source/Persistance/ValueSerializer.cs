using UnityEngine;

namespace STRV.Variables.Persistance
{
    public abstract class ValueSerializer: ScriptableObject
    {
        /// Loads data from local storage into the given target 
        public abstract bool Load(ISerializable target);
        
        /// Saves the current local cache into the persistent storage 
        public abstract bool Save(ISerializable value);
    }
}