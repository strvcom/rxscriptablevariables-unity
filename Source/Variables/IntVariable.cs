using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class IntVariable : Variable<int>
    {
        protected override void SyncValue()
        {
            var remoteValue = RemoteSettings.GetInt(RemoteSettingsId, CurrentValue);
            HandleValueChange(remoteValue);
        }

        public override bool SupportsRemoteSettings()
        {
            return true;
        }
    }
}