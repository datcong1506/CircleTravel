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
        float t = 0.2f;
        float vzx = Vector3.Scale(direcToTarget, new Vector3(1, 0, 1)).magnitude / t;
        float vy = (direcToTarget.y - gravity * t * t * 0.5f) / t;
        Vector3 startVelocity = Vector3.Scale(direcToTarget, new Vector3(1, 0, 1)).normalized * vzx + Vector3.up * vy;
        rigidbody.velocity = startVelocity;
        rigidbody.angularVelocity=Vector3.zero;
        StartCoroutine(MergePosision(t));
    }

    IEnumerator MergePosision(float t)
    {
        yield return new WaitForSeconds(t);
        selfTransform.position = target;
        rigidbody.velocity =
            Vector3.Lerp(rigidbody.velocity, Vector3.up*-5f,0.7f);
    }
    
}
