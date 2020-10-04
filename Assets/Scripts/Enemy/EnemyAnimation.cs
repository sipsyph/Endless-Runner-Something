using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
public static Animator enemyAnimator;
public static string[] koboldSwordTriggerNames, koboldBowTriggerNames, slimeTriggerNames;

public static string[] currentSetOfTriggers;

public Animator pubEnemyAnimator;

    void Start()
    {
        koboldSwordTriggerNames = new string[]{"GettingHitTrigger","IdleTrigger","UpperLeftSwingTrigger",
        "BottomLeftSwingTrigger","UpperRightSwingTrigger","BottomRightSwingTrigger","BlockedTrigger","WalkingTrigger"};

        koboldBowTriggerNames = new string[]{"IdleTrigger","ShootArrowTrigger"};

        slimeTriggerNames = new string[]{"IdleTrigger","LeftHitTrigger","RightHitTrigger"};
    }

    #region Kobold with Sword
    public static void PlayUpperLeftSwingAnimation()
    {
        ResetTriggerExcept("UpperLeftSwingTrigger", koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("UpperLeftSwingTrigger");
    }

    public static void PlayUpperRightSwingAnimation()
    {
        ResetTriggerExcept("UpperRightSwingTrigger", koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("UpperRightSwingTrigger");
    }

    public static void PlayBottomLeftSwingAnimation()
    {
        ResetTriggerExcept("BottomLeftSwingTrigger", koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("BottomLeftSwingTrigger");
    }

    public static void PlayBottomRightSwingAnimation()
    {
        ResetTriggerExcept("BottomRightSwingTrigger", koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("BottomRightSwingTrigger");
    }

    public static void PlayGettingHitAnimation()
    {
        ResetTriggerExcept("GettingHitTrigger",koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("GettingHitTrigger");
    }

    public static void PlayBlockedAnimation()
    {
        ResetTriggerExcept("BlockedTrigger",koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("BlockedTrigger");
    }

    public static void PlayWalkingAnimation()
    {
        ResetTriggerExcept("WalkingTrigger",koboldSwordTriggerNames);
        enemyAnimator.SetTrigger("WalkingTrigger");
    }

    public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger",koboldSwordTriggerNames);
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

    public static void PlayKoboldBowIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger",koboldBowTriggerNames);
        enemyAnimator.SetTrigger("IdleTrigger");
    }

    public static void PlayKoboldBowShootAnimation()
    {
        ResetTriggerExcept("ShootArrowTrigger",koboldBowTriggerNames);
        enemyAnimator.SetTrigger("ShootArrowTrigger");
    }

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

    public static void ResetAllInCurrentTriggerSetExcept(string triggerName)
    {
        Debug.Log("ERROR => "+triggerName+" Animator: "+enemyAnimator.name);
        for(int i=0; i<currentSetOfTriggers.Length;i++)
        {
            Debug.Log("Animator level => triggerName:"+triggerName+" "+i);
            if(!triggerName.Equals(currentSetOfTriggers[i]))
            {
                enemyAnimator.ResetTrigger(currentSetOfTriggers[i]);
                Debug.Log("Resetting Trigger: "+currentSetOfTriggers[i]);
            }
        }
        enemyAnimator.SetTrigger("IdleTrigger");
    }

}
