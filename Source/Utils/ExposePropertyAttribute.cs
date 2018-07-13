﻿using System;

// Taken from http://wiki.unity3d.com/index.php/ExposePropertiesInInspector_SetOnlyWhenChanged
// Original author: Mift (mift)
// Variant author: Venryx(venryx)

// ReSharper disable once CheckNamespace
namespace STRV.Variables.Utils
{
    [AttributeUsage(AttributeTargets.Property)] public class ExposePropertyAttribute : Attribute { }
}