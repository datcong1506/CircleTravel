using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameController : UICanvas
{
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
    public override bool IsOpening()
    {
        return gameObject.activeSelf;
    }
}
