using UnityEditor;
using UnityEngine;

namespace STRV.Variables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableDrawer : VariableDrawer<Vector3>
    { }
}