using Game.Src.Boot;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/String", order = 53)]
    public class StringVariable : Variable<string>
    {
        protected override void SyncValue()
        {
            /*
            if (!FirebaseInit.IsInitialized)
            {
                Debug.Log("Trying to sync remote variable before firebase is initialized");
                return;
            } 
            
            var firebaseValue = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(RemoteSettingsId);
            if (!string.IsNullOrEmpty(firebaseValue.StringValue))
            {
                var remoteValue = firebaseValue.StringValue;
                HandleValueChange(remoteValue);
            }
            */
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