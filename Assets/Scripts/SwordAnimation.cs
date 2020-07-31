using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
public static Animator swordAnimator;
public static string[] triggerNames;

public Animator pubSwordAnimator;

    void Start()
    {
        swordAnimator = pubSwordAnimator;
        triggerNames = new string[]{"DownwardSlashRightToLeftTrigger","DownwardSlashLeftToRightTrigger",
        "UpwardSlashLeftToRightTrigger", "UpwardSlashRightToLeftTrigger", "IdleTrigger"};
    }

    public static void PlayDownwardSlashRightToLeft()
    {
        ResetTriggerExcept("DownwardSlashRightToLeftTrigger");
        swordAnimator.SetTrigger("DownwardSlashRightToLeftTrigger");
    }

    public static void PlayDownwardSlashLeftToRight()
    {
        ResetTriggerExcept("DownwardSlashLeftToRightTrigger");
        swordAnimator.SetTrigger("DownwardSlashLeftToRightTrigger");
    }

    public static void PlayUpwardSlashLeftToRight()
    {
        ResetTriggerExcept("UpwardSlashLeftToRightTrigger");
        swordAnimator.SetTrigger("UpwardSlashLeftToRightTrigger");
    }

    public static void PlayUpwardSlashRightToLeft()
    {
        ResetTriggerExcept("UpwardSlashRightToLeftTrigger");
        swordAnimator.SetTrigger("UpwardSlashRightToLeftTrigger");
    }


    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger");
        swordAnimator.SetTrigger("IdleTrigger");
    }

    public static void ResetTriggerExcept(string triggerName)
    {

        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                swordAnimator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }
        swordAnimator.SetTrigger("IdleTrigger");
    }
}
