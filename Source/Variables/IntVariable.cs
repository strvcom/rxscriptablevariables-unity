using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Int", order = 51)]
    public class IntVariable : Variable<int>
    {
        protected override void SyncValue()
        {
            
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