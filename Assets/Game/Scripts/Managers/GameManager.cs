using System;
using System.Collections;
using System.Collections.Generic;
using DatCong;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : Singleton<GameManager>,IInitAble
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
    public LevelController CurrentLevelCcl => currentLevelCCL;

    //Player
    [SerializeField] private PlayerController playerController;
    //
    
    
    //
    [SerializeField] private Transform selfTransform;
    public Transform SelfTransform => selfTransform;
    //
    private float startPlayTime;

    public float EclapseTime
    {
        get
        {
            return Time.time - startPlayTime;
        }
    }
    

    [SerializeField]private int circleCount = 0;

    public int CircleCount
    {
        get
        {
            return circleCount;
        }
        set
        {
            circleCount = value;
            if (currentLevelCCL !=null)
            {
                if (circleCount == currentLevelCCL.CircleCount)
                {
                    //win
                    StartCoroutine(DelaySometimeWin(2f));
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Init();
        // Play();
    }

    private void OnDestroy()
    {
        OnDeSpawn();
    }


    public void Init()
    {
        UnLoadResource();
        UIManager.Instance.LoadUI(UIID.MainUI);
        SetGameInput();
        playerController.Init(gameInput.Player);
        circleCount = 0;
        UnPauseGame();
    }

    private void UnLoadResource()
    {
        UnLoadLevel();
        PollManager.Instance.ResetPoll();
        CacheComponentManager.Instance.ResetCache();
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
        UnLoadResource();
        circleCount = 0;
        startPlayTime = Time.time;
        StartLevel();
        playerController.Play();
        GameUIManager.Instance.LoadUI(UIID.InGameUI);
    }


    public void Home()
    {
        Init();
    }

    public void PlayAgain()
    {
        Home();
        Play();
    }
    
    
    public void Lose()
    {
        UnLoadResource();
        UIManager.Instance.LoadUI(UIID.LoseUI);
    }



    IEnumerator DelaySometimeWin(float t)
    {
        GameDataController.NextLevel();
        yield return 
            new WaitForSeconds(t);
        Win();
    }
    public void Win()
    {
        UIManager.Instance.LoadUI(UIID.WinUI);
    }
    
    

    private void OnDeSpawn()
    {
        GameDataController.OnDeSpawn();
    }
    
    
    private void StartLevel()
    {
      
        UnLoadLevel();
        LoadLevel();
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
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
