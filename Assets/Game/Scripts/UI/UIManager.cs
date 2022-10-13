using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIID, UICanvas> uiMaps;



    [SerializeField] private GameObject mainUIPrefab;
    [SerializeField] private GameObject settingUIPrefab;
    [SerializeField] private GameObject pauseUIPrefab;
    
    private void Init()
    {
        uiMaps = new Dictionary<UIID, UICanvas>();
    }
}
