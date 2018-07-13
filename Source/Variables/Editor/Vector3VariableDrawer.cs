using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableDrawer : VariableDrawer<Vector3>
    { }
}