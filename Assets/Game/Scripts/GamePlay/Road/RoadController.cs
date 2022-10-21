using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float spawnRate;
    [SerializeField] private float startSpawnTime;
    
    //NOTE:this can be drag from inspector
    private LevelController levelController;
    public void Init(LevelController levelController)
    {
        this.levelController = levelController;
    }
    
    public void SpawnCircle(CircleController circleController)
    {
        circleController.Init(pathCreator);
    }
}
