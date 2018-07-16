using UnityEditor;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableDrawer : VariableDrawer<string>
    { }
}