using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPauseController : UICanvas, IInitAble
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI circleCountText;
    [SerializeField] private Transform settingRoot;

    public override void Enter()
    {
        base.Enter();
        Init();
    }

    public void ContinueButton()
    {
        GameManager.Instance.UnPauseGame();
        Exit();
    }

    public void SettingButton()
    {
        UIManager.Instance.LoadSubUI(UIID.SettingUI, settingRoot);
    }

    public void HomeButton()
    {
        GameManager.Instance.Home();
    }

    public void Init()
    {
        levelText.text = GameManager.Instance.GameDataController.GetCurrentLevel().name;
        circleCountText.text = GameManager.Instance.CircleCount.ToString();
    }
}
