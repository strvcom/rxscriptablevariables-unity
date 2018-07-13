using System;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [Serializable]
    public class FloatReference : Reference<float>
    {
        public FloatVariable VariableReference;

        public override Variable<float> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}