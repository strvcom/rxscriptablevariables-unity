﻿using System;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [Serializable]
    public class BoolReference : Reference<bool>
    {
        public BoolVariable VariableReference;

        public override Variable<bool> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}