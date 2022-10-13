using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;

    private void Update()
    {
        pathCreator.bezierPath.SetPoint(0,Vector3.left);
    }
}
