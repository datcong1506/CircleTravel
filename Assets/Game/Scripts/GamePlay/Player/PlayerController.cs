using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]private GameInput.PlayerActions playerInput;
    
    public void Init(GameInput.PlayerActions playerInput)
    {
       
        mainCamera=Camera.main;
        this.playerInput = playerInput;
        this.playerInput.Enable();
        this.playerInput.Touching.Enable();
        this.playerInput.Fire.Enable();
        this.playerInput.Move.Enable();
    }

    private void OnDeSpawn()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        TakeCircle();
    }

    private void TakeCircle()
    {
        var IsClicked = playerInput.Move.ReadValue<Vector2>();
        Debug.Log(IsClicked);
    }
}
