using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
public static Animator enemyAnimator;
public static string[] swordTriggerNames, slimeTriggerNames;

public Animator pubEnemyAnimator;

    void Start()
    {
        //enemyAnimator = pubEnemyAnimator;
        swordTriggerNames = new string[]{"GettingHitTrigger","IdleTrigger","UpperLeftSwingTrigger",
        "BottomLeftSwingTrigger","UpperRightSwingTrigger","BottomRightSwingTrigger"};

        slimeTriggerNames = new string[]{"IdleTrigger","LeftHitTrigger","RightHitTrigger"};

    }

    #region Enemy with Sword
        public static void PlayUpperLeftSwingAnimation()
    {
        ResetTriggerExcept("UpperLeftSwingTrigger", swordTriggerNames);
        enemyAnimator.SetTrigger("UpperLeftSwingTrigger");
    }

        public static void PlayUpperRightSwingAnimation()
    {
        ResetTriggerExcept("UpperRightSwingTrigger", swordTriggerNames);
        enemyAnimator.SetTrigger("UpperRightSwingTrigger");
    }

        public static void PlayBottomLeftSwingAnimation()
    {
        ResetTriggerExcept("BottomLeftSwingTrigger", swordTriggerNames);
        enemyAnimator.SetTrigger("BottomLeftSwingTrigger");
    }

        public static void PlayBottomRightSwingAnimation()
    {
        ResetTriggerExcept("BottomRightSwingTrigger", swordTriggerNames);
        enemyAnimator.SetTrigger("BottomRightSwingTrigger");
    }

    public static void PlayGettingHitAnimation()
    {
        ResetTriggerExcept("GettingHitTrigger",swordTriggerNames);
        enemyAnimator.SetTrigger("GettingHitTrigger");
    }

    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger",swordTriggerNames);
        enemyAnimator.SetTrigger("IdleTrigger");
    }
    #endregion
    
    #region Slime
    public static void PlayLeftAttackAnimation()
    {
        ResetTriggerExcept("LeftHitTrigger",slimeTriggerNames);
        enemyAnimator.SetTrigger("LeftHitTrigger");
    }

        public static void PlayRightAttackAnimation()
    {
        ResetTriggerExcept("RightHitTrigger",slimeTriggerNames);
        enemyAnimator.SetTrigger("RightHitTrigger");
    }

        public static void PlaySlimeIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger",slimeTriggerNames);
        enemyAnimator.SetTrigger("IdleTrigger");
    }
    #endregion

    public static void ResetTriggerExcept(string triggerName, string[] triggerNames)
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
