using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableDrawer : VariableDrawer<Vector2>
    { }
}