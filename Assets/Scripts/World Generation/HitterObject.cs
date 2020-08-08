using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitterObject : MonoBehaviour
{
    public GameObject firstAreaInstance, secondAreaInstance;

    public Transform areaBackgroundOfFirstAreaInst, areaBackgroundOfSecondAreaInst;
    
    public Transform[] firstAreaInstanceNodes, secondAreaInstanceNodes;
    public float distanceOfNewAreaInstance;

    private bool switchInstance;

    private int randNum;

    void Start()
    {
        switchInstance = true;
        RandomizeBackgroundObjectsOfThisAreaInst(areaBackgroundOfFirstAreaInst);
        RandomizeBackgroundObjectsOfThisAreaInst(areaBackgroundOfSecondAreaInst);
    }

    void RandomizeBackgroundObjectsOfThisAreaInst(Transform areaBackground)
    {
        
        Transform[] allChildren = areaBackground.GetComponentsInChildren<Transform>(true); //GetComponentsInChildren's boolean parameter is U S E L E S S
        //Debug.Log("ALL CHILDREN COUNT: "+allChildren.Length);
        foreach (Transform child in allChildren)
        {
            randNum  = Random.Range(1,100);
            child.gameObject.SetActive(true);
            if(!child.name.Equals("Area Background"))
            {
                if(randNum>=50)
                {   
                    child.gameObject.SetActive(false);
                }else{
                    child.gameObject.SetActive(true);
                }
            }

            
        }
    }

    void ActivateNodes(Transform[] nodes)
    {
        foreach(Transform node in nodes)
        {
            node.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Object To Hit Before Generation")
        {
            if(switchInstance)
            {
                RandomizeBackgroundObjectsOfThisAreaInst(areaBackgroundOfFirstAreaInst);
                firstAreaInstance.transform.position = new Vector3(collision.transform.parent.position.x, 0, collision.transform.parent.position.z + distanceOfNewAreaInstance);
                ActivateNodes(firstAreaInstanceNodes);
                switchInstance = false;
            
            }else{
                RandomizeBackgroundObjectsOfThisAreaInst(areaBackgroundOfSecondAreaInst);
                secondAreaInstance.transform.position = new Vector3(collision.transform.parent.position.x, 0, collision.transform.parent.position.z + distanceOfNewAreaInstance);
                ActivateNodes(secondAreaInstanceNodes);
                switchInstance = true;
            }
            

        }
    }
}
