using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float spawnRate;
    [SerializeField] private float startSpawnTime;
    
    [Header("Set In Runtime")]
    [SerializeField] private int spawnCount;
    
    //NOTE:this can be drag from inspector
    private LevelController levelController;
    public void Init(LevelController levelController,int spawnCount)
    {
        this.levelController = levelController;
        this.spawnCount = spawnCount;
        StartCoroutine(SpawnCircles());
    }

    private IEnumerator SpawnCircles()
    {
        yield return new WaitForSeconds(startSpawnTime);
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnCircle();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnCircle()
    {
        var circleCCL = levelController.SpawnCircle();
        circleCCL.Init(pathCreator);
    }
}
