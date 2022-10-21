using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlertController : UICanvas
{
    private CircleController belongCircle;

    public void SetOwn(CircleController circleController)
    {
        belongCircle = circleController;
    }
    
    
    private void Update()
    {
        UpdateAlert();
    }

    private void UpdateAlert()
    {
        if (belongCircle != null)
        {
            if (!belongCircle.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
