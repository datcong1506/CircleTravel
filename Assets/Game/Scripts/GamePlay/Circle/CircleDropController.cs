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
        Throw();
    }

    
    private void OnDespawn()
    {
      
    }
    
    public void SetPosision(Vector3 posision)
    {
        selfTransform.position = posision;
        startPosision = posision;
    }

    
    private float gravity = -10f;
    private void Throw()
    {
        var direcToTarget = target - startPosision;
        float vy0 = 9f;

        float deltaY = direcToTarget.y;

        var timeToTarget = Mathf.Sqrt(Mathf.Abs(2 * deltaY / gravity));

        var direcXZtoTarget = Vector3.Scale(direcToTarget, new Vector3(1, 0, 1));
        float vxz0 = direcXZtoTarget.magnitude / timeToTarget;

        Vector3 startVelocity = direcXZtoTarget.normalized * vxz0;

        rigidbody.velocity = startVelocity;

    }
    
}
