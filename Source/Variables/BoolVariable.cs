using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Bool")]
    public class BoolVariable : Variable<bool>
    {
        protected override void SyncValue()
        {
            var remoteValue = RemoteSettings.GetBool(RemoteSettingsId, CurrentValue);
            HandleValueChange(remoteValue);
        }

        public override bool SupportsRemoteSettings()
        {
            return true;
        }
    }
}