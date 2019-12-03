using System.Globalization;
using System.Linq;
using UnityEngine;
using Variables.Source.Utils;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Float", order = 52)]
    public class FloatVariable : Variable<float>
    {
        protected override void SyncValue()
        {
            if (!FirebaseInit.IsInitialized)
            {
                Debug.Log("<color=#ffa500ff>Remote Config</color> - Trying to sync remote variable before firebase is initialized");
                return;
            } 
            
            var firebaseValue = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(RemoteSettingsId);
            if (!string.IsNullOrEmpty(firebaseValue.StringValue))
            {
                var value = firebaseValue.StringValue;
                var remoteValue = float.Parse(value, CultureInfo.InvariantCulture);
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