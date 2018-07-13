using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    public class FloatVariable : Variable<float>
    {
        protected override void SyncValue()
        {
            var remoteValue = RemoteSettings.GetFloat(RemoteSettingsId, CurrentValue);
            HandleValueChange(remoteValue);
        }

        public override bool SupportsRemoteSettings()
        {
            return true;
        }
    }
}