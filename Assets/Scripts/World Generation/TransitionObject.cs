using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionObject : MonoBehaviour
{
    public GameObject firstTransitionIntoAreaInst, secondTransitionIntoAreaInst;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Hitter Object")
        {
            //INSERT LOADING SCREEN OR TRANSITION SCENE SOMETHING HERE

            firstTransitionIntoAreaInst.transform.position = collision.transform.gameObject.GetComponent<HitterObject>().firstAreaInstance.transform.position;
            secondTransitionIntoAreaInst.transform.position = collision.transform.gameObject.GetComponent<HitterObject>().secondAreaInstance.transform.position;

            collision.transform.gameObject.GetComponent<HitterObject>().firstAreaInstance.SetActive(false);
            collision.transform.gameObject.GetComponent<HitterObject>().secondAreaInstance.SetActive(false);

            firstTransitionIntoAreaInst.SetActive(true);
            secondTransitionIntoAreaInst.SetActive(true);

            collision.transform.gameObject.GetComponent<HitterObject>().firstAreaInstance = firstTransitionIntoAreaInst;
            collision.transform.gameObject.GetComponent<HitterObject>().secondAreaInstance = secondTransitionIntoAreaInst;
            PlayerParent.playerClimbing = true; //Activate player climbing method
        }
    }
}
