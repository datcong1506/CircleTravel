using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIWaveWarming : UICanvas, IInitAble
{
    [SerializeField] private TextMeshProUGUI alertTMP;
    [SerializeField] private Animator animator;
    private string mainAnimParam = "Alert";
    public void Init()
    {
        StartCoroutine(AlertMessage());
    }
    public void SetMessage(int waveCount)
    {
        alertTMP.text = "Wave " + waveCount.ToString();
        Init();
    }
    IEnumerator AlertMessage()
    {
        animator.SetTrigger(mainAnimParam);
        yield return new WaitForSeconds(1.28f);
        Exit();
    }
}
