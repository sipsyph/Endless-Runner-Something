using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    Transform thisNode;

    Transform[] allChildren;
    List<Transform> allObstacles;
    private int randNum;
    void Start()
    {
        thisNode = transform.parent.transform;
        allObstacles = new List<Transform>();
        RandomizeObstacleObjectsInThisNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizeObstacleObjectsInThisNode()
    {
        
        allChildren = thisNode.GetComponentsInChildren<Transform>(true); //GetComponentsInChildren's boolean parameter is U S E L E S S
        //Debug.Log("ALL CHILDREN COUNT: "+allChildren.Length);
        

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Obstacle")
            {
                Debug.Log("Obstacle of Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allObstacles.Add(child);
            }
        }

        Debug.Log("OBSTACLE COUNT: "+allObstacles.Count);
        randNum = Random.Range(0,allObstacles.Count);
        allObstacles[randNum].gameObject.SetActive(true);
        Debug.Log("Chosen OBSTACLE: "+allObstacles[randNum].name);
    }

    void ResetNode()
    {
        RandomizeObstacleObjectsInThisNode();
        this.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            ResetNode();
        }
    }
}
