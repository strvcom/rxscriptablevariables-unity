using UnityEditor;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableDrawer : VariableDrawer<float>
    { }
}