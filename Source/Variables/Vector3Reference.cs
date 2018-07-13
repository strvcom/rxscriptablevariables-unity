using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [Serializable]
    public class Vector3Reference : Reference<Vector3>
    {
        public Vector3Variable VariableReference;

        public override Variable<Vector3> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}