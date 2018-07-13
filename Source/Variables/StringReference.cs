using System;

namespace STRV.Variables
{
    [Serializable]
    public class StringReference : Reference<string>
    {
        public StringVariable VariableReference;

        public override Variable<string> Variable
        {
            get {
                return VariableReference;
            }
        }
    }
}