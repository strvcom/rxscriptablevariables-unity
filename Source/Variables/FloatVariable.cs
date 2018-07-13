using System.Globalization;
using UnityEngine;

namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float", order = 52)]
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
        
        public override string GetStringValue()
        {
            return CurrentValue.ToString(CultureInfo.InvariantCulture);
        }

        public override void SetStringValue(string value)
        {
            HandleValueChange(float.Parse(value));
        }
    }
}