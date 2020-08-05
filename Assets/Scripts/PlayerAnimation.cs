using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
public static Animator playerAnimator, playerParentAnimator;
public static string[] triggerNames, triggerNamesForPlayerParent;

public Animator pubPlayerAnimator, pubPlayerParentAnimator;

    void Start()
    {
        playerAnimator = pubPlayerAnimator;
        playerParentAnimator = pubPlayerParentAnimator;
        triggerNames = new string[]{"LeftMoveTrigger","RightMoveTrigger","IdleTrigger"};
        triggerNamesForPlayerParent = new string[]{"JumpTrigger","SlideTrigger","WalkTrigger"};
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

    //==============================================//
    public static void PlaySlideAnimation()
    {
        ResetTriggerForParentExcept("SlideTrigger");
        playerParentAnimator.SetTrigger("SlideTrigger");
    }

    public static void PlayJumpAnimation()
    {
        ResetTriggerForParentExcept("JumpTrigger");
        playerParentAnimator.SetTrigger("JumpTrigger");
    }

    public static void PlayWalkAnimation()
    {
        ResetTriggerForParentExcept("WalkTrigger");
        playerParentAnimator.SetTrigger("WalkTrigger");
    }


    public static void ResetTriggerExcept(string triggerName)
    {

        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                playerAnimator.ResetTrigger(triggerNames[i]);
            }
        }

    }

    public static void ResetTriggerForParentExcept(string triggerName)
    {

        for(int i=0; i<triggerNamesForPlayerParent.Length;i++)
        {
            if(!triggerNamesForPlayerParent.Equals(triggerNamesForPlayerParent[i]))
            {
                playerParentAnimator.ResetTrigger(triggerNamesForPlayerParent[i]);
            }
        }

    }

}
