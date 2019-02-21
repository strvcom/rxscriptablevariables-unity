using Game.Src.Boot;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int", order = 51)]
    public class IntVariable : Variable<int>
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
                var remoteValue = (int)firebaseValue.LongValue;
                HandleValueChange(remoteValue);
            }
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