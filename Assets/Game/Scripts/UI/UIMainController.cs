using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainController : UICanvas,IInitAble
{
    [SerializeField] private Transform settingRoot;

    [SerializeField] private TextMeshProUGUI levelText;

    public override bool IsOpening()
    {
        return gameObject.activeSelf;
    }


    public void PlayButton()
    {
        GameManager.Instance.Play();
    }

    public void SettingButton()
    {
        UIManager.Instance.LoadSubUI(UIID.SettingUI,settingRoot);
    }

    public override void Enter()
    {
        base.Enter();
        Init();
    }

    public void Init()
    {
        levelText.text = GameManager.Instance.GameDataController.GetCurrentLevel().name;
    }
}
