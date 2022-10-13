using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOnAirController : MonoBehaviour
{
    [SerializeField] private Transform selfTransform;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Vector3 offset;

    [SerializeField] private GameObject circleOnPostPrefab;
    public GameObject CircleOnPostPrefab => circleOnPostPrefab;
    public void Init(Transform circle,Color color)
    {
        renderer.material.color = color;
        selfTransform.position = circle.position;
        selfTransform.rotation = circle.rotation;
    }

    public void UpdatePosision(Vector3 target)
    {
        selfTransform.position = Vector3.Lerp(selfTransform.position, target, 0.2f)+offset;
    }
}
