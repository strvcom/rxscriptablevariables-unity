﻿using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CreateAssetMenu(menuName = "Variables/Bool", order = 50)]
    public class BoolVariable : Variable<bool>
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
            HandleValueChange(bool.Parse(value));
        }
    }
}