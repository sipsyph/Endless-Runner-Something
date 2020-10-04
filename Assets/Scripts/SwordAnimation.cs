using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
public static Animator swordAnimator, swordBtnAnimator;
public static string[] swordTriggerNames, swordBtnTriggerNames;

public Animator pubSwordAnimator, pubSwordBtnAnimator;

    void Start()
    {
        swordAnimator = pubSwordAnimator;
        swordBtnAnimator = pubSwordBtnAnimator;
        swordTriggerNames = new string[]{"PreSlashTrigger", "SlashTrigger","IdleTrigger"};
        swordBtnTriggerNames = new string[]{"FadeInTrigger","IdleTrigger"};
    }

    public static void PlayPreSlash()
    {
        ResetTriggerExcept("PreSlashTrigger", swordTriggerNames, swordAnimator);
        swordAnimator.SetTrigger("PreSlashTrigger");
    }

    public static void PlaySlash()
    {
        ResetTriggerExcept("SlashTrigger", swordTriggerNames, swordAnimator);
        swordAnimator.SetTrigger("SlashTrigger");
    }

    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger", swordTriggerNames, swordAnimator);
        swordAnimator.SetTrigger("IdleTrigger");
    }
    public static void PlaySwordBtnFadeInAnimation()
    {
        ResetTriggerExcept("FadeInTrigger", swordBtnTriggerNames, swordBtnAnimator);
        swordBtnAnimator.SetTrigger("FadeInTrigger");
    }

    public static void PlaySwordBtnIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger", swordBtnTriggerNames, swordBtnAnimator);
        swordBtnAnimator.SetTrigger("IdleTrigger");
    }

    public static void ResetTriggerExcept(string triggerName, string[] triggerNames, Animator animator)
    {

        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                animator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }
        swordAnimator.SetTrigger("IdleTrigger");
    }
}
