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
        enemyAnimator = pubEnemyAnimator;
        triggerNames = new string[]{"GettingHitTrigger","IdleTrigger"};
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

        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                enemyAnimator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }

    }
}
