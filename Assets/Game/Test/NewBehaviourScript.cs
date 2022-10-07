using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float speed;


    private float distancTravel;
    private void Update()
    {
        distancTravel += Time.deltaTime * speed;
        if (distancTravel >= pathCreator.path.length)
        {
            distancTravel = pathCreator.path.length-0.001f;
        }
        // pathCreator.path.GetPointAtDistance()
        transform.position = pathCreator.path.GetPointAtDistance(distancTravel);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distancTravel);
    }
}
