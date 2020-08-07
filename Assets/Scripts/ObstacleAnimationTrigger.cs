using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimationTrigger : MonoBehaviour
{
    public string[] triggerNames;

    public Animator animator;
    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        triggerNames = new string[]{"FallingTrigger"};
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Player")
        {
            PlayFallingAnimation();
        }
    }

    public void PlayFallingAnimation()
    {
        //ResetTriggerExcept("FallingTrigger");
        animator.SetTrigger("FallingTrigger");
    }

    public void ResetTriggerExcept(string triggerName)
    {
        for(int i=0; i<triggerNames.Length;i++)
        {
            if(!triggerName.Equals(triggerNames[i]))
            {
                animator.ResetTrigger(triggerNames[i]);
            }
        }
    }
}
