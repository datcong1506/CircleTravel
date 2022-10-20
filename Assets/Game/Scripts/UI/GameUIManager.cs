using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : UIManager,InitAble
{
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        LoadUI(UIID.MainUI);
    }
}
