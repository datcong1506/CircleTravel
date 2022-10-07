using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour,IPointerClickHandler
{
    
        

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Okia"); 
    }
}
