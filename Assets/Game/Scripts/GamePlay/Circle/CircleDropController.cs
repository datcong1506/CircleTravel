using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class CircleDropController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private Transform selfTransform;

    private Vector3 startPosision;
    [SerializeField]private Vector3 target;
    private bool moveToTarget=true;
    
   [SerializeField]private PathCreator pathCreator;
   private bool isDisablePath;
    [SerializeField] private float speed = 5f;
    private float s;
    private void OnDisable()
    {
        OnDespawn();
    }
    public void Init(Color skinColor,Vector3 dropPosision)
    {
        render.material.color = skinColor;
        moveToTarget = true;
        target = dropPosision;
        s = 0;
        pathCreator = PollManager.Instance.PathPolling.Instantiate().GetComponent<PathCreator>();
        isDisablePath = false;
        SetPath();
    }

   

    private void OnDespawn()
    {
        if (!isDisablePath)
        {
            if (pathCreator != null)
            {
                pathCreator.gameObject.SetActive(false);
            }
        }
        
    }
    
    private void Update()
    {
        MoveToTarget();
    }

    public void SetPosision(Vector3 posision)
    {
        selfTransform.position = posision;
        startPosision = posision;
    }


    private void MoveToTarget()
    {
        if (moveToTarget)
        {
            s += speed * Time.deltaTime;
            if (s >= pathCreator.path.length)
            {
                s = pathCreator.path.length - 0.01f;
            }

            selfTransform.position = pathCreator.path.GetPointAtDistance(s);
            if ((selfTransform.position - target).magnitude < 0.1f)
            {
                rigidbody.velocity=Vector3.down;
                pathCreator.gameObject.SetActive(false);
                moveToTarget = false;
                isDisablePath = true;
            }
        }
    }

    private void SetPath()
    {
        pathCreator.bezierPath.SetPoint(0,startPosision);
        pathCreator.bezierPath.SetPoint(1,startPosision+Vector3.up*3);
        pathCreator.bezierPath.SetPoint(2,target+Vector3.up*3);
        pathCreator.bezierPath.SetPoint(3,target);
        
    }
    
    
}
