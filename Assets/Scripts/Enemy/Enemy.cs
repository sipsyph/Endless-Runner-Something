using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // /public int firstAttackDamageDealt, secondAttackDamageDealt, thirdAttackDamageDealt;

    void Update()
    {


    }

    void CheckIfCurrentEnemyIsThis()
    {
        if(PlayerParent.currentEnemy == this.transform)
        {

        }
    }

}
