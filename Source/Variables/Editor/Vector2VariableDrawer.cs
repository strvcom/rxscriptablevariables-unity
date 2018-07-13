using UnityEditor;
using UnityEngine;

namespace STRV.Variables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableDrawer : VariableDrawer<Vector2>
    { }
}