﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    Transform thisNode;

    Transform[] allChildren;
    List<Transform> allObstacles, allEnemies, allItemPositions, allItems, allTransObjs;
    private int randNum, ctr;

    private bool isEnemyNode, canReset, transObjSpawned;

    public bool thisNodeHasItemSpawns;
    void Start()
    {
        allObstacles = new List<Transform>();
        allEnemies = new List<Transform>();
        allItemPositions = new List<Transform>();
        allItems = new List<Transform>();
        allTransObjs = new List<Transform>();
        transObjSpawned = false;
        canReset = false;
        thisNode = transform.parent.transform;
        if(thisNode.name.Contains("Last")) //also means that this node is the last node in the instance
        {
            RandomizeAreaTransitionObjectsInThisNode(); //Determines if trans obj should spawn or not
        }

        if(thisNode.name.Contains("Enemy"))
        {
            isEnemyNode = true;
        }else{
            isEnemyNode = false;
            RandomizeObstacleObjectsInThisNode();
        }
        

        if(thisNodeHasItemSpawns)
        {
            RandomizeItemObjectsInThisNode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canReset)
        {
            WaitBeforeReset();

        }
    }

    void WaitBeforeReset()
    {
        if(isEnemyNode)
        {
            if(PlayerParent.currentEnemyIsDead == false)
            {
                Debug.Log("Killing Enemy in this Node: "+thisNode.name);
                PlayerParent.currentEnemyIsDead = true;
            }
        }
        ctr++;
        if(ctr>=10)
        {
            PlayerParent.currentEnemyIsDead = false;
            ctr = 0;
            canReset = false;
            ResetNode();
        }
    }

    void RandomizeAreaTransitionObjectsInThisNode()
    {
        Debug.Log("Entered Area Transition code");
        allChildren = thisNode.GetComponentsInChildren<Transform>(true); //Globalize this indafuture

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Trans Obj")
            {
                //Debug.Log("Obstacle of Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allTransObjs.Add(child);
            }
        }

        if(allTransObjs.Count <= 0)
        {
            Debug.Log("This Last Node does not have a trans obj");
            return;
        }

        //Debug.Log("OBSTACLE COUNT: "+allObstacles.Count);
        randNum = Random.Range(0,100);
        if(randNum>=0 && randNum<=50)
        {
            transObjSpawned = true;
            randNum = Random.Range(0,allTransObjs.Count);
            allTransObjs[randNum].gameObject.SetActive(true);
        }else{
            transObjSpawned = false;
            Debug.Log("Decided that trans obj should not spawn.");
        }

        //Debug.Log("Chosen OBSTACLE: "+allObstacles[randNum].name);
    }
    void RandomizeObstacleObjectsInThisNode()
    {
        allChildren = thisNode.GetComponentsInChildren<Transform>(true);

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Obstacle")
            {
                //Debug.Log("Obstacle of Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allObstacles.Add(child);
            }
        }
        //Debug.Log("OBSTACLE COUNT: "+allObstacles.Count);
        randNum = Random.Range(0,allObstacles.Count);
        allObstacles[randNum].gameObject.SetActive(true);
        if(allObstacles[randNum].gameObject.GetComponent<Animator>()!=null)
        {
            allObstacles[randNum].gameObject.GetComponent<Animator>().ResetTrigger("FallingTrigger");
            //allObstacles[randNum].gameObject.GetComponent<Animator>().SetTrigger("IdleTrigger");
            //Debug.Log("Playing Idle anim for this obstacle: "+allObstacles[randNum].name);
        }
        //Debug.Log("Chosen OBSTACLE: "+allObstacles[randNum].name);
    }

    void RandomizeItemObjectsInThisNode()
    {
        allChildren = thisNode.GetComponentsInChildren<Transform>(true);
        Debug.Log("Randomized items in "+thisNode.name);

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Item Position")
            {
                Debug.Log("Item of Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allItemPositions.Add(child);
            }
        }
        Debug.Log("ITEM COUNT: "+allItemPositions.Count);
        randNum = Random.Range(0,allItemPositions.Count-1);
        Transform chosenItemPos = allItemPositions[randNum];
        chosenItemPos.gameObject.SetActive(true);
        Debug.Log("Chosen ITEM: "+allItemPositions[randNum].name);

        randNum = Random.Range(0,ItemManager.itemPool.Count);
        //ItemManager.itemPool[randNum].gameObject.SetActive(true);
        //ItemManager.itemPool[randNum].position = new Vector3(chosenItemPos.position.x,ItemManager.itemPool[randNum].position.y,chosenItemPos.position.z);
        GameObject item = Instantiate( ItemManager.itemPool[randNum]
        , new Vector3(chosenItemPos.position.x,ItemManager.itemPool[randNum].transform.position.y,chosenItemPos.position.z)
        , Quaternion.identity);

        item.transform.parent = chosenItemPos;
        return;
    }


    void RandomizeEnemySpawnInThisNode()
    {
        allChildren = thisNode.GetComponentsInChildren<Transform>(true);

        foreach(Transform child in allChildren)
        {
            if(child.tag == "Enemy")
            {
                //Debug.Log("Enemy in Node "+thisNode.name+":"+child.name);
                child.gameObject.SetActive(false);
                allEnemies.Add(child);
            }
        }
        //Debug.Log("ENEMY COUNT: "+allEnemies.Count);
        if(allEnemies.Count<=1)
        {
            randNum = 0;
        }else{
            randNum = Random.Range(0,allEnemies.Count-1);
        }
        
        allEnemies[randNum].gameObject.SetActive(true);
        //Debug.Log("Chosen ENEMY: "+allEnemies[randNum].name);
    }
    void ResetNode()
    {

        if(thisNode.name.Contains("Last")) //also means that this node is the last node in the instance
        {
            RandomizeAreaTransitionObjectsInThisNode(); //Determines if trans obj should spawn or not
        }

        if(isEnemyNode && !transObjSpawned)
        {
            RandomizeEnemySpawnInThisNode();
        }else{
            RandomizeObstacleObjectsInThisNode();
        }

        if(thisNodeHasItemSpawns)
        {
            RandomizeItemObjectsInThisNode();
        }
        
        this.transform.parent.gameObject.SetActive(false);
        return;
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag=="Player")
        {
            canReset = true;
        }
    }
}
