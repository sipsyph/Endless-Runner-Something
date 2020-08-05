using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
public static Animator enemyAnimator;
public static string[] triggerNames;

public Animator pubEnemyAnimator;

    void Start()
    {
        //enemyAnimator = pubEnemyAnimator;
        triggerNames = new string[]{"GettingHitTrigger","IdleTrigger","UpperLeftSwingTrigger",
        "BottomLeftSwingTrigger","UpperRightSwingTrigger","BottomRightSwingTrigger"};
    }


    public static void PlayUpperLeftSwingAnimation()
    {
        ResetTriggerExcept("UpperLeftSwingTrigger");
        enemyAnimator.SetTrigger("UpperLeftSwingTrigger");
    }

        public static void PlayUpperRightSwingAnimation()
    {
        ResetTriggerExcept("UpperRightSwingTrigger");
        enemyAnimator.SetTrigger("UpperRightSwingTrigger");
    }

        public static void PlayBottomLeftSwingAnimation()
    {
        ResetTriggerExcept("BottomLeftSwingTrigger");
        enemyAnimator.SetTrigger("BottomLeftSwingTrigger");
    }

        public static void PlayBottomRightSwingAnimation()
    {
        ResetTriggerExcept("BottomRightSwingTrigger");
        enemyAnimator.SetTrigger("BottomRightSwingTrigger");
    }

    public static void PlayGettingHitAnimation()
    {
        ResetTriggerExcept("GettingHitTrigger");
        enemyAnimator.SetTrigger("GettingHitTrigger");
    }

    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger");
        enemyAnimator.SetTrigger("IdleTrigger");
    }

    public static void ResetTriggerExcept(string triggerName)
    {
        //Debug.Log("ERROR => "+triggerName+" Animator: "+enemyAnimator.name);
        for(int i=0; i<triggerNames.Length;i++)
        {
            //Debug.Log("Animator level => triggerName:"+triggerName+" "+i);
            if(!triggerName.Equals(triggerNames[i]))
            {
                enemyAnimator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }
        enemyAnimator.SetTrigger("IdleTrigger");
    }
}
