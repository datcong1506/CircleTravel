using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostController : MonoBehaviour, IInitAble, IDeSpawn
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

    private int circleCount = 0;
    public int CircleCount => circleCount;

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        DeSpawn();
    }


    public void Init(Color color)
    {
        skinColor = color;
        meshRenderer.material.color = skinColor;

    }
    public void Init()
    {
        CacheComponentManager.Instance.PostCache.Add(gameObject);
        circleCount = 0;
    }

    public void DeSpawn()
    {
        if (CacheComponentManager.Instance != null)
        {
            CacheComponentManager.Instance.PostCache.Remove(gameObject);
        }
    }

    public bool CanDrop(CircleController circleController)
    {
        return circleController.SkinColor == SkinColor;
    }


    public void DropToPost(CircleDropController circleDropController)
    {
        circleDropController.Init(SkinColor, topTF.position);
        circleCount++;
    }

}
