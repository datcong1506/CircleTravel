using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public enum CircleState
{
    Init,
    Move,
    Take,
    Drop,
    Place,
}

public class CircleController : MonoBehaviour
{
    private PathCreator pathCreator;
    [SerializeField]private float speed;
    [SerializeField] private MeshRenderer meshRender;
    public Color SkinColor
    {
        get
        {
            return meshRender.material.color;
        }
    }
    private float timeFromSpawn;
    [SerializeField] private Transform selfTransform;
    //State
    private CircleState circleState;
    //
    

    public void Init(PathCreator pathCreator, float speed, Material material)
    {
        this.pathCreator = pathCreator;
        this.speed = speed;
        meshRender.material = material;
        circleState = CircleState.Move;
        timeFromSpawn = 0;
    }
    public void Init(PathCreator pathCreator, Material material)
    {
        this.pathCreator = pathCreator;
        meshRender.material = material;
        circleState = CircleState.Move;
        timeFromSpawn = 0;
    }
     public void Init(PathCreator pathCreator)
    {
        this.pathCreator = pathCreator;
        circleState = CircleState.Move;
        timeFromSpawn = 0;
        selfTransform.position = pathCreator.path.GetPointAtDistance(speed * timeFromSpawn);

    }


    private void Update()
    {
        Move();
    }

    
    private void Move()
    {
        timeFromSpawn += Time.deltaTime;
        if (circleState == CircleState.Move)
        {
            var distance = speed * timeFromSpawn;
            if (distance >= pathCreator.path.length - 0.0001f)
            {
                gameObject.SetActive(false);
            }
            selfTransform.position = pathCreator.path.GetPointAtDistance(speed * timeFromSpawn);
        }
    }

    public void OnBeTakingHandle(Vector3 touchPosision)
    {
        
    }

    public void OnDropHandle()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("okia");
    }
}
