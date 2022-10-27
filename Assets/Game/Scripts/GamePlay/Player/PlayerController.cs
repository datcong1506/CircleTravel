using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[Serializable]
public enum PlayerState
{
    Init,
    Relax,
    HoldCircle,
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Vector3 offset;
    private Camera mainCamera;
    private Transform cameraTransform;
    [SerializeField] private GameInput.PlayerActions playerInput;
    [SerializeField] private LayerMask circleLayer;
    [SerializeField] private LayerMask postLayer;
    // Input
    [SerializeField] private bool isClicked;
    //

    private CircleController currentCircleController;



    private CircleOnAirController circleOnAirController;
    [SerializeField] private Transform handTF;

    private void Update()
    {
        SetInput();
        PlayerActions();
    }

    public void Init(GameInput.PlayerActions playerInput)
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.GetComponent<Transform>();

        this.playerInput = playerInput;
        this.playerInput.Enable();
        this.playerInput.Move.Enable();
        this.playerInput.Fire.Enable();
    }

    public void Play()
    {
        playerState = PlayerState.Relax;
    }

    private void OnDeSpawn()
    {
        playerInput.Disable();
    }



    private void SetInput()
    {
        isClicked = playerInput.Fire.IsPressed();
    }

    private void PlayerActions()
    {

        switch (playerState)
        {
            case PlayerState.Init:
                break;
            case PlayerState.Relax:
                if (isClicked)
                {
                    TryTakeCircle();
                }

                break;
            case PlayerState.HoldCircle:
                TakeCircle();
                DropCircle();
                break;
        }

    }

    private void TryTakeCircle()
    {
        RaycastHit hit;
        var touchPosision = playerInput.Move.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(touchPosision);
        if (Physics.Raycast(ray, out hit, 10000, circleLayer))
        {
            if (CacheComponentManager.Instance.CCCache.TryGet(hit.collider.gameObject, out var circleController))
            {
                if (circleController.CanBeTaken())
                {
                    currentCircleController = circleController;
                    var color = currentCircleController.SkinColor;
                    var circleOnAirGO = currentCircleController.OnBeTake();
                    circleOnAirController = circleOnAirGO.GetComponent<CircleOnAirController>();
                    circleOnAirController.Init(currentCircleController.transform, color, currentCircleController.SelfTransform.position);
                    playerState = PlayerState.HoldCircle;
                }
            }
        }
    }
    //just update posision
    private void TakeCircle()
    {
        var touchPosision = playerInput.Move.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(touchPosision);


        var worldPoint = ray.GetPoint(10f);
        // Debug.Log(touchPosision+" "+ worldPoint);
        circleOnAirController.UpdatePosision(worldPoint);
    }

    private void DropCircle()
    {


        if (!isClicked)
        {
            var touchPosision = playerInput.Move.ReadValue<Vector2>();
            Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(circleOnAirController.SelfTransform.position));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000, postLayer))
            {
                if (CacheComponentManager.Instance.PostCache.TryGet(hit.collider.gameObject, out var postController))
                {
                    if (postController.CanDrop(currentCircleController))
                    {
                        var dropCircleGO =
                            PollManager.Instance.CircleOnPostPoll.Instantiate(circleOnAirController.CircleOnPostPrefab);
                        var dropCircleController = dropCircleGO.GetComponent<CircleDropController>();
                        dropCircleController.SetPosision(circleOnAirController.SelfTransform.position);
                        postController.DropToPost(dropCircleController);
                        circleOnAirController.gameObject.SetActive(false);
                        playerState = PlayerState.Relax;
                        currentCircleController.DropToPost();
                        GameManager.Instance.CircleCount++;
                        return;
                    }
                }
            }
        }
        if (!isClicked)
        {
            currentCircleController.BackToRoad();
            circleOnAirController.gameObject.SetActive(false);
            playerState = PlayerState.Relax;
        }
    }
}
