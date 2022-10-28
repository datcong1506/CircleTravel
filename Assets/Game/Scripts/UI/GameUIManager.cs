using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : UIManager, IInitAble
{
    [SerializeField] private Transform topLayer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        LoadUI(UIID.MainUI);
    }

    public UICanvas LoadSubUIInTopLayer(UIID uiid)
    {
        return LoadSubUI(uiid, topLayer);
    }
}
