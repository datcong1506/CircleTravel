using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[Serializable]
public class DynamicData
{
    [Header("This should be changed in runtime(ExacllyLevel)")]
    public string CurrentLevel;
    public bool UseSound;
    public bool UseVib;

    public DynamicData()
    {
        CurrentLevel = "Level_1";
        UseSound = true;
        UseVib = true;
    }
}
