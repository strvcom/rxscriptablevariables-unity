using System.Globalization;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float", order = 52)]
    public class FloatVariable : Variable<float>
    {
        protected override void SyncValue()
        {
            var firebaseValue = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(RemoteSettingsId);
            if (!string.IsNullOrEmpty(firebaseValue.StringValue))
            {
                var remoteValue = (float)firebaseValue.LongValue;
                Debug.Log(this.name + remoteValue);
                HandleValueChange(remoteValue);
            }
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