using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOnAirController : MonoBehaviour
{
    [SerializeField] private Transform selfTransform;
    public Transform SelfTransform => selfTransform;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Vector3 offset;
    [SerializeField] private CharacterJoint characterJoint;
    [SerializeField] private GameObject circleOnPostPrefab;
    public GameObject CircleOnPostPrefab => circleOnPostPrefab;
    public void Init(Transform circle, Color color)
    {
        renderer.material.color = color;
        selfTransform.position = circle.position;
        selfTransform.rotation = circle.rotation;
    }

    public void Init(Transform circle, Color color, Vector3 posision)
    {
        Init(circle, color);
        characterJoint.connectedAnchor = posision + characterJoint.anchor;
    }

    public void UpdatePosision(Vector3 target)
    {
        var targetRealPosision = target + offset + characterJoint.anchor;
        if ((targetRealPosision - characterJoint.connectedAnchor).magnitude > 0.05f)
        {
            characterJoint.connectedAnchor = Vector3.Lerp(characterJoint.connectedAnchor, target + offset + characterJoint.anchor, 0.8f); ;
        }
    }
}
