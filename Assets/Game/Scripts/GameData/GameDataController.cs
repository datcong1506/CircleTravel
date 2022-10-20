using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using DatCong;

public class GameDataController : MonoBehaviour
{

    [SerializeField]private DynamicData dynamicData=null;
    public DynamicData DynamicData
    {
        get
        {
            return dynamicData;
        }
    }

    [SerializeField] private StaticData staticData;

    private string dataStoragePath
    {
        get
        {
            return Application.dataPath+"/GameData.json";
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetData();
    }
    
    
    
    // CallByGameManager
    public void OnDeSpawn()
    {
        Helper.WriteData(DynamicData,dataStoragePath);
    }
    
    
    private void SetData()
    {
        dynamicData = Helper.LoadData<DynamicData>(dataStoragePath);
        if (dynamicData == null)
        {
            dynamicData = new DynamicData();
        }
    }

    
    public GameObject GetCirclePrefab()
    {
        return staticData.CirclePrefabs[0];
    }


    public LevelData GetCurrentLevel()
    {
        return staticData.LevelDatas[DynamicData.CurrentLevel];
    }


    public void NextLevel()
    {
        var currentLevel = GetCurrentLevel();
        var hasNextLevel = currentLevel.Nextlevel != null;
        if (hasNextLevel)
        {
            DynamicData.CurrentLevel = currentLevel.Nextlevel.name;
        }
    }



    [ContextMenu("DeleteData")]
    private void DeleteData()
    {
        Helper.DeleteData(dataStoragePath);
    }
}
