using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DatCong;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<ListToDictionNary<UIID, UICanvas>> uiList;

    [SerializeField] private Transform root;
    public Transform Root => root;
    private Dictionary<UIID, UICanvas> _uiMapsPrefab;

    protected Dictionary<UIID, UICanvas> uiMapsPrefab
    {
        get
        {
            if (_uiMapsPrefab == null)
            {
                ConvertListToDic();
            }

            return _uiMapsPrefab;
        }
    }

    protected Dictionary<UIID, UICanvas> uiMaps = new Dictionary<UIID, UICanvas>();

    protected override void Awake()
    {
        base.Awake();
        ConvertListToDic();
    }

    private void ConvertListToDic()
    {
        _uiMapsPrefab = uiList.ConvertListToDic();
    }
    /// <summary>
    ///  Disable all uicanvas before load a ui
    /// </summary>
    /// <param name="uiid"></param>
    public UICanvas LoadUI(UIID uiid)
    {
        UnLoadUIS();
        return JustLoadUI(uiid, root);
    }

    private UICanvas JustLoadUI(UIID uiid, Transform nroot)
    {
        bool isExsist = uiMaps.ContainsKey(uiid);
        if (isExsist)
        {
            isExsist = isExsist && uiMaps[uiid] != null;
        }
        if (!isExsist)
        {
            bool isExsistInPrefas = uiMapsPrefab.ContainsKey(uiid);
            if (!isExsistInPrefas) return null;
            var uiPrefabGO = uiMapsPrefab[uiid].gameObject;
            var uiGO = Instantiate(uiPrefabGO, nroot);
            var uiCanvas = uiGO.GetComponent<UICanvas>();
            uiMaps.Add(uiid, uiCanvas);
        }
        else
        {
            // set root

        }
        var targetCanvas = uiMaps[uiid];
        targetCanvas.SetRoot(nroot);
        targetCanvas.Enter();

        return targetCanvas;
    }

    public UICanvas LoadSubUI(UIID uiid, Transform nroot)
    {
        return JustLoadUI(uiid, nroot);
    }

    public UICanvas LoadUITop(UIID uiid)
    {
        UnLoadUIS();
        var targetCanvas = JustLoadUI(uiid, root);
        targetCanvas.transform.SetAsFirstSibling();
        return targetCanvas;
    }

    public UICanvas LoadSubUI(UIID uiid)
    {
        return LoadSubUI(uiid, root);
    }

    public void UnLoadUI(UIID uiid)
    {
        Debug.Log(uiid);
        bool isExsistUI = uiMaps.ContainsKey(uiid);
        if (isExsistUI)
        {
            bool isOpening = uiMaps[uiid].IsOpening();
            if (isOpening)
            {
                uiMaps[uiid].Exit();
            }
        }
    }
    public void UnLoadUIS()
    {
        for (int i = 0; i < uiMaps.Count; i++)
        {
            var uiCanvas = uiMaps.ElementAt(i);
            UnLoadUI(uiCanvas.Key);
        }
    }

}
