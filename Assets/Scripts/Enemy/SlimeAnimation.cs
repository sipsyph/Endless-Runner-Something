using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
public static Animator slimeAnimator;
public static string[] triggerNames;

public Animator pubSlimeAnimator;

    void Start()
    {
        slimeAnimator = pubSlimeAnimator;
        triggerNames = new string[]{"IdleTrigger","LeftHitTrigger","RightHitTrigger"};
    }


    public static void PlayLeftAttackAnimation()
    {
        ResetTriggerExcept("LeftHitTrigger");
        slimeAnimator.SetTrigger("LeftHitTrigger");
    }

        public static void PlayRightAttackAnimation()
    {
        ResetTriggerExcept("RightHitTrigger");
        slimeAnimator.SetTrigger("RightHitTrigger");
    }

        public static void PlayIdleAnimation()
    {
        ResetTriggerExcept("IdleTrigger");
        slimeAnimator.SetTrigger("IdleTrigger");
    }

    public static void ResetTriggerExcept(string triggerName)
    {
        //Debug.Log("ERROR => "+triggerName+" Animator: "+enemyAnimator.name);
        for(int i=0; i<triggerNames.Length;i++)
        {
            //Debug.Log("Animator level => triggerName:"+triggerName+" "+i);
            if(!triggerName.Equals(triggerNames[i]))
            {
                slimeAnimator.ResetTrigger(triggerNames[i]);
                //Debug.Log("Resetting Trigger: "+triggerNames[i]);
            }
        }
        slimeAnimator.SetTrigger("IdleTrigger");
    }
}

