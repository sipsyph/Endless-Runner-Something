using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
public static Animator playerAnimator;
public static string[] triggerNames;

public Animator pubPlayerAnimator;

    void Start()
    {
        playerAnimator = pubPlayerAnimator;
        triggerNames = new string[]{"LeftMoveTrigger","RightMoveTrigger","IdleTrigger"};
    }

    public static void PlayLeftMoveAnimation()
    {
        ResetTriggerExcept("LeftMoveTrigger");
        playerAnimator.SetTrigger("LeftMoveTrigger");
    }

    public static void PlayRightMoveAnimation()
    {
        ResetTriggerExcept("RightMoveTrigger");
        playerAnimator.SetTrigger("RightMoveTrigger");
    }


    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger");
        playerAnimator.SetTrigger("IdleTrigger");
    }

    public static void ResetTriggerExcept(string triggerName)
    {

        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                playerAnimator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }

    }

}
