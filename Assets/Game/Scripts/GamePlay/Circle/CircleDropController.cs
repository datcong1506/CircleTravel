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
    [SerializeField] private Vector3 target;
    private bool moveToTarget = true;
    private float gravity = -10f;

    private Quaternion startQuaternion;
    private Quaternion targetQuaternion;

    private void OnDisable()
    {
        OnDespawn();
    }
    public void Init(Color skinColor, Vector3 dropPosision)
    {
        render.material.color = skinColor;
        moveToTarget = true;
        target = dropPosision;
        Throw();
    }

    // NOTUSE
    private void OnDespawn()
    {

    }

    public void SetPosision(Vector3 posision)
    {
        selfTransform.position = posision;
        startPosision = posision;
    }


    public void SetRotation(Quaternion rotation)
    {
        selfTransform.rotation = rotation;
        startQuaternion = rotation;
    }


    private void CalTargetQuaternion()
    {
        var mainCam = Camera.main;

        var selfPosisionView = mainCam.WorldToViewportPoint(selfTransform.position);
        selfPosisionView.z = 0;
        var targetView = mainCam.WorldToViewportPoint(target + Vector3.down * 1f);
        targetView.z = 0;

        var directotarget = targetView - selfPosisionView;
        var distanceXZ = directotarget.magnitude;

        bool setDefault = false;
        if (distanceXZ > 0.07f) // if drop effect descistion
        {
            setDefault = true;
        }

        if (selfTransform.up.y > 0 && setDefault)
        {
            // best perform =>> cache Quaternion.Euler(180f, 0, 0)
            targetQuaternion = Quaternion.Euler(180f, 0, 0);
        }
        else
        {
            // best perform =>> cache Quaternion.identity
            targetQuaternion = Quaternion.identity;
        }
    }


    ///<sumary>
    /// cal drop velocity and handle drop to target
    ///</sumary>
    private void Throw()
    {
        var direcToTarget = target - startPosision;
        float t = 0.25f;
        float vzx = Vector3.Scale(direcToTarget, new Vector3(1, 0, 1)).magnitude / t;
        float vy = (direcToTarget.y - gravity * t * t * 0.5f) / t;
        Vector3 startVelocity = Vector3.Scale(direcToTarget, new Vector3(1, 0, 1)).normalized * vzx + Vector3.up * vy;
        rigidbody.velocity = startVelocity;
        rigidbody.angularVelocity = Vector3.zero;
        StartCoroutine(MergePosisionAndRotation(t));
    }
    // t : time to target
    IEnumerator MergePosisionAndRotation(float t)
    {
        CalTargetQuaternion();
        float totalT = 0;
        for (int i = 0; i < 1000; i++)
        {
            totalT += Time.deltaTime;
            // Lerp rotation effect another way use angleVelocity
            selfTransform.rotation = Quaternion.Lerp(startQuaternion, targetQuaternion, totalT / t);
            if (totalT > t)
            {
                break;
            }
            yield return null;
        }

        // lock posision, and posision in case physic fail
        selfTransform.position = target;
        rigidbody.velocity =
            Vector3.Lerp(rigidbody.velocity, Vector3.up * -5f, 0.7f);
    }

}
