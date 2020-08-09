using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    Transform thisNode;

    Transform[] allChildren;
    List<Transform> allObstacles, allEnemies;
    private int randNum, ctr;

    private bool isEnemyNode, canReset;
    void Start()
    {
        canReset = false;
        thisNode = transform.parent.transform;
        if(thisNode.name.Contains("Enemy"))
        {
            isEnemyNode = true;
        }else{
            isEnemyNode = false;
        }
        allObstacles = new List<Transform>();
        allEnemies = new List<Transform>();
        RandomizeObstacleObjectsInThisNode();
    }

    // Update is called once per frame
    void Update()
    {
        if(canReset)
        {
            if(isEnemyNode)
            {
                if(PlayerParent.currentEnemyIsDead == false)
                {
                    PlayerParent.currentEnemyIsDead = true;
                }
            }
            
            WaitBeforeReset();
        }
    }

    void WaitBeforeReset()
    {
        ctr++;
        if(ctr>=60)
        {
            ctr = 0;
            canReset = false;
            ResetNode();
        }
    }

    void RandomizeObstacleObjectsInThisNode()
    {
        allChildren = thisNode.GetComponentsInChildren<Transform>(true);

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


    void RandomizeEnemySpawnInThisNode()
    {
        allChildren = thisNode.GetComponentsInChildren<Transform>(true);

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Enemy")
            {
                Debug.Log("Enemy in Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allEnemies.Add(child);
            }
        }
        Debug.Log("ENEMY COUNT: "+allEnemies.Count);
        if(allEnemies.Count<=1)
        {
            randNum = 0;
        }else{
            randNum = Random.Range(0,allEnemies.Count-1);
        }
        
        allEnemies[randNum].gameObject.SetActive(true);
        Debug.Log("Chosen ENEMY: "+allEnemies[randNum].name);
    }
    void ResetNode()
    {
        if(isEnemyNode)
        {
            PlayerParent.currentEnemyIsDead = false;
            RandomizeEnemySpawnInThisNode();
        }else{
            RandomizeObstacleObjectsInThisNode();
        }
        
        this.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            canReset = true;
        }
    }
}
