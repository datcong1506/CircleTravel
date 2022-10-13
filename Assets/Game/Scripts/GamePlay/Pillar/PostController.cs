using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostController : MonoBehaviour
{
    [SerializeField] private Transform selfTransform;
    public Transform SelfTransform => selfTransform;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Transform topTF;

    private Color skinColor;
    public Color SkinColor
    {
        get
        {
            return skinColor;
        }
    }
    

    public void Init(Color color)
    {
        skinColor = color;
        meshRenderer.material.color = skinColor;
    }




    public bool CanDrop(CircleController circleController)
    {
        return circleController.SkinColor == SkinColor;
    }


    public void DropToPost(CircleDropController circleDropController)
    {
        circleDropController.Init(SkinColor,topTF.position);
    }
    

}
