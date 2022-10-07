using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{
    [SerializeField] private Transform selfTransform;
    public Transform SelfTransform => selfTransform;
    [SerializeField] private MeshRenderer meshRenderer;


    public Color SkinColor
    {
        get
        {
            return meshRenderer.material.color;
        }
    }
    

    public void Init(Color color)
    {
        meshRenderer.material.color = color;
    }




    private bool CanDrop(CircleController circleController)
    {
        return circleController.SkinColor == SkinColor;
    }

}
