using UnityEditor;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableDrawer : VariableDrawer<bool>
    { }
}