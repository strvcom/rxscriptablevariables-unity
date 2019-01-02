using System;

namespace STRV.Variables
{
    public static class VariablesUpdatedHandler
    {
        public delegate void UpdatedEventHandler();
        public static event UpdatedEventHandler Updated;
        
        public static void VariablesUpdated()
        {
            UpdatedEventHandler updated = Updated;
            updated?.Invoke();
        }
    }
}