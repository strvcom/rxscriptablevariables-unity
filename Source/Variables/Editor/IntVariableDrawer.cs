using UnityEditor;

namespace STRV.Variables
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableDrawer : VariableDrawer<int>
    { }
}