using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas:MonoBehaviour
{
    public virtual void Enter()
    {
        gameObject.SetActive(true);
    }

    public virtual void Exit()
    {
        transform.SetParent(UIManager.Instance.Root);
        gameObject.SetActive(false);
    }

    public virtual bool IsOpening()
    {
        return gameObject.activeSelf;}

    public virtual void SetRoot(Transform root)
    {
        transform.SetParent(root);
    }
}
