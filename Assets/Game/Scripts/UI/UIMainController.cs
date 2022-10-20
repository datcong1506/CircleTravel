using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainController : UICanvas
{
    [SerializeField] private Transform settingRoot;
  

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
}
