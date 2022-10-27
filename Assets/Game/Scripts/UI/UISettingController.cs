using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingController : UICanvas
{
    [SerializeField] private Animator animator;



    public void SoundButton()
    {
        animator.SetBool("UseSound", !animator.GetBool("UseSound"));
    }


    public void VibButton()
    {
        animator.SetBool("UseVib", !animator.GetBool("UseVib"));
    }

    public void ExitButton()
    {
        Exit();
    }

}
