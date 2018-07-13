using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int", order = 51)]
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

        public override string GetStringValue()
        {
            return CurrentValue.ToString();
        }

        public override void SetStringValue(string value)
        {
            HandleValueChange(int.Parse(value));
        }
    }
}