using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Canvas : MonoBehaviour
{

    public TextMeshProUGUI healthText, enemyHealthText, enemyNameText;
    public Button upperLeftWeaponBtn, upperRightWeaponBtn, bottomLeftWeaponBtn, bottomRightWeaponBtn;
    void Start()
    {
        healthText.text = UIResources.health + Player.playerHealth.ToString();
        SetupButtonEvents();
    }

    void Update()
    {
        UpdatePlayerHealthText();
        UpdateCurrentEnemyUI();
    }

    void UpdatePlayerHealthText()
    {
        Debug.Log("Health text being updated...");
        healthText.text = UIResources.health + Player.playerHealth.ToString();
    }

    void UpdateCurrentEnemyUI()
    {
        if(PlayerParent.enemyDetected)
        {
            if(PlayerParent.currentEnemy.gameObject.activeSelf)
            {
                enemyNameText.text = PlayerParent.currentEnemy.gameObject.name;
                enemyHealthText.text = PlayerParent.currentEnemyHealth.ToString();
            }
        }else{
                enemyNameText.text = "";
                enemyHealthText.text = "";
                return;
        }
    }

    void SetupButtonEvents()
    {
        upperLeftWeaponBtn.onClick.AddListener(() =>
        {
            ResetPlayerAttackingMode();
            SwordAnimation.PlayDownwardSlashLeftToRight();
        });
        upperRightWeaponBtn.onClick.AddListener(() =>
        {
            ResetPlayerAttackingMode();
            SwordAnimation.PlayDownwardSlashRightToLeft();
        });
        bottomLeftWeaponBtn.onClick.AddListener(() =>
        {
            ResetPlayerAttackingMode();
            SwordAnimation.PlayUpwardSlashLeftToRight();
        });
        bottomRightWeaponBtn.onClick.AddListener(() =>
        {
            ResetPlayerAttackingMode();
            SwordAnimation.PlayUpwardSlashRightToLeft();
        });
    }

    void ResetPlayerAttackingMode()
    {
        PlayerParent.attackingModeDurationCtr  = 0;
        PlayerParent.isAttacking = true;
    }
}
