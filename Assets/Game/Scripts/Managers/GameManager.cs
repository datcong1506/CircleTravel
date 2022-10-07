using System;
using System.Collections;
using System.Collections.Generic;
using DatCong;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]private GameDataController gameDataController;
    public GameDataController GameDataController => gameDataController;

    //Game Input
    private GameInput gameInput;
    //

    [Header("Runtime Set")] [SerializeField]
    private LevelData levelData;
    [Header("Runtime Set")] [SerializeField]
    private LevelController currentLevelCCL;


    //Player
    [SerializeField] private PlayerController playerController;
    //
    
    
    protected override void Awake()
    {
        base.Awake();
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        Init();
    }

    private void OnDestroy()
    {
        OnDeSpawn();
    }


    private void Init()
    {
        SetGameInput();
        StartLevel();
    }

    private void SetGameInput()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
        }
        gameInput.Enable();
    }
    
    // NOTE:   
    // Be called By UI Event 
    //
    public void Play()
    {
        
    }
    
    //
    //
    //
    public void Pause()
    {
        
    }
    
    //
    //
    //
    public void ExitGame()
    {
        
    }


    private void OnDeSpawn()
    {
        GameDataController.OnDeSpawn();
    }
    
    
    private void StartLevel()
    {
        UnLoadLevel();
        LoadLevel();
        playerController.Init(gameInput.Player);
    }
    

    private void LoadLevel()
    {
        levelData = GameDataController.GetCurrentLevel();
        currentLevelCCL = Instantiate(levelData.LevelPrefab).GetComponent<LevelController>();
        currentLevelCCL.Init();
    }

    private void UnLoadLevel()
    {
        if (currentLevelCCL!=null)
        {
            //NOTE: for now just destroy GO
            Destroy(currentLevelCCL.gameObject);
        }
    }
    
}
