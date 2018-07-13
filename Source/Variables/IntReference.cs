using System;

namespace STRV.Variables
{
    [Serializable]
    public class IntReference : Reference<int>
    {
        public IntVariable VariableReference;

        public override Variable<int> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}