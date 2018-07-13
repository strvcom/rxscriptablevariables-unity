using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
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
    }
}