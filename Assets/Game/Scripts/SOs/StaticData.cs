using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DatCong;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticData",menuName = "SO/StaticData")]
public class StaticData : ScriptableObject
{
    [SerializeField] private GameObject[] circlePrefabs;
    public GameObject[] CirclePrefabs => circlePrefabs;


    [Header("Levels")] [SerializeField] private List<ListToDictionNary<string,LevelData>> levelDatas;
    //Dictionnary for fast 
    private Dictionary<string, LevelData> levelDatasDic;
    public Dictionary<string, LevelData> LevelDatas
    {
        get
        {
            if (levelDatasDic == null)
            {
                levelDatasDic = levelDatas.ConvertListToDic();
            }

            return levelDatasDic;
        }
    }



}


