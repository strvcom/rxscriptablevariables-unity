using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/String", order = 53)]
    public class StringVariable : Variable<string>
    {
        protected override void SyncValue()
        {
            var remoteValue = RemoteSettings.GetString(RemoteSettingsId, CurrentValue);
            HandleValueChange(remoteValue);
        }

        public override bool SupportsRemoteSettings()
        {
            return true;
        }
        
        public override string GetStringValue()
        {
            return CurrentValue;
        }

        public override void SetStringValue(string value)
        {
            HandleValueChange(value);
        }
    }
}