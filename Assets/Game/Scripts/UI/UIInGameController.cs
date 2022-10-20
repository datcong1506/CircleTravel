using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameController : UICanvas
{
    [SerializeField] private Transform pauseRoot;
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override bool IsOpening()
    {
        return gameObject.activeSelf;
    }

    public void PauseButton()
    {
        UIManager.Instance.LoadSubUI(UIID.PauseUI,pauseRoot);
        GameManager.Instance.PauseGame();
    }
    
}
