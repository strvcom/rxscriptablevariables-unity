using UnityEditor;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableDrawer : VariableDrawer<int>
    { }
}