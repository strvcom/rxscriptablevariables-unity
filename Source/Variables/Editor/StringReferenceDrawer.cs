using UnityEditor;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : ReferenceDrawer
    { }
}