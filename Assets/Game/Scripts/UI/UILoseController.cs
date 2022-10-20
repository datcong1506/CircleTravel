using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILoseController : UICanvas,IInitAble
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI circleCountText;


    public override void Enter()
    {
        base.Enter();
        Init();
    }

    public void Init()
    {
        levelText.text = GameManager.Instance.GameDataController.GetCurrentLevel().name;
        circleCountText.text = GameManager.Instance.CircleCount.ToString()+"/"+GameManager.Instance.CurrentLevelCcl.CircleCount;
    }


    public void HomeButton()
    {
        GameManager.Instance.Home();
    }

    public void PlayAgainButton()
    {
        GameManager.Instance.PlayAgain();
    }
}
