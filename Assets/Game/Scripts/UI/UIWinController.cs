using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWinController : UICanvas, IInitAble
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeEclapseText;

    public override void Enter()
    {
        base.Enter();
        Init();
    }

    public void Init()
    {
        levelText.text = GameManager.Instance.GameDataController.GetCurrentLevel().name;
        timeEclapseText.text = ConvertSecondToMin(GameManager.Instance.EclapseTime);
    }

    private string ConvertSecondToMin(float second)
    {
        int min = (int)second / 60;

        if (min > 0)
        {
            return min.ToString() + "p" + (((int)second) % 60).ToString() + "s";
        }
        else
        {
            return second.ToString("F2") + "s";
        }
    }

    public void ContinuePlayButton()
    {
        GameManager.Instance.Play();
    }

    public void HomeButton()
    {
        GameManager.Instance.Home();
    }
}
