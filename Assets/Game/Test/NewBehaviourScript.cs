using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{

    public bool IsGlobal;

    public Vector3 Value;

    private void Update()
    {
        if (IsGlobal)
        {
            transform.position = Value;
        }
        else
        {
            transform.localPosition = Value;
        }
    }
}
