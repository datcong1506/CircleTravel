using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

[Serializable]
public enum CircleState
{
    Init,
    Move,
    Take,
    Drop,
    Place,
}

public class CircleController : MonoBehaviour,IInitAble,IDeSpawn
{
    private PathCreator pathCreator;
    [SerializeField]private float speed;
    [SerializeField] private MeshRenderer meshRender;
    
    private Color skinColor;
    public Color SkinColor
    {
        get
        {
            return skinColor;
        }
    }
    private float timeFromSpawn;
    [SerializeField] private Transform selfTransform;
    public Transform SelfTransform => selfTransform;
    //State
    [SerializeField]private CircleState circleState;
    //

    [SerializeField] private GameObject circleOnAirPrefab;

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        DeSpawn();
    }


    public void Init(PathCreator pathCreator, float speed, Material material)
    {
        this.pathCreator = pathCreator;
        this.speed = speed;
        meshRender.material = material;
        circleState = CircleState.Move;
        timeFromSpawn = 0;
        selfTransform.position = pathCreator.path.GetPointAtDistance(speed * timeFromSpawn);

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


     public void SetColor(Color color)
     {
         skinColor = color;
         meshRender.material.color = skinColor;
     }
     
     
     
    private void Update()
    {
        Move();
    }

    
    private void Move()
    {
        timeFromSpawn += Time.deltaTime;
        if (circleState == CircleState.Move||circleState==CircleState.Take)
        {
            var distance = speed * timeFromSpawn;
            if (distance >= pathCreator.path.length - 0.0001f)
            {
                gameObject.SetActive(false);
                // lose game
                GameManager.Instance.Lose();
            }
            selfTransform.position = pathCreator.path.GetPointAtDistance(speed * timeFromSpawn);
        }
    }
    


    public bool CanBeTaken()
    {
        return true;
    }

    public GameObject OnBeTake()
    {
        circleState = CircleState.Take;
        var color = GetColor(0.4f);
        meshRender.material.color = color;
        return PollManager.Instance.CircleOnAirPoll.Instantiate(circleOnAirPrefab);
    }

    private Color GetColor(float alpha)
    {
        var color = SkinColor;
        color.a = alpha;
        return color;
    }
    
    public void BackToRoad()
    {
        circleState = CircleState.Move;
        meshRender.material.color = skinColor;
    }

    public void DropToPost()
    {
        gameObject.SetActive(false);
    }
    
    
    public void OnDropHandle()
    {
        
    }

    private void UpdatePosision(Vector3 posision)
    {
        selfTransform.position = posision;
    }

    public void Init()
    {
        CacheComponentManager.Instance.CCCache.Add(gameObject);
    }

    public void DeSpawn()
    {
        if (CacheComponentManager.Instance != null)
        {
            CacheComponentManager.Instance.CCCache.Remove(gameObject);
        }
    }
}
