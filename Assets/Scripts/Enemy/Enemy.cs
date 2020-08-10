using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public int firstAttackDamageDealt, secondAttackDamageDealt, thirdAttackDamageDealt;
    Vector3 startingPos;
    void Start()
    {
        //startingPos = transform.localPosition;
    }

    void Update()
    {
        //transform.position = new Vector3(transform.localPosition.x, startingPos.y, transform.localPosition.y);

    }

    void CheckIfCurrentEnemyIsThis()
    {
        if(PlayerParent.currentEnemy == this.transform)
        {

        }
    }

}
