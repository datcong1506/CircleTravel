using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DatCong;
using UnityEngine;



[CreateAssetMenu(fileName = "Level_",menuName = "SO/Level")]
[Serializable]
public class LevelData:ScriptableObject
{
    public GameObject LevelPrefab;
    
    public LevelData Nextlevel;
}
