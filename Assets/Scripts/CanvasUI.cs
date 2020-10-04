using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasUI : MonoBehaviour
{

    public TextMeshProUGUI healthText, enemyHealthText, enemyNameText;

    public Button upperLeftWeaponBtn, upperRightWeaponBtn, bottomLeftWeaponBtn, bottomRightWeaponBtn, 
    inventoryBtn, weaponBtn;
    public GameObject inventoryPanel, shieldJoystick;
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
            enemyNameText.text = PlayerParent.currentEnemy.gameObject.name;
            enemyHealthText.text = PlayerParent.currentEnemyHealth.ToString();
        }else{
                enemyNameText.text = "";
                enemyHealthText.text = "";
                return;
        }
    }



    void SetupButtonEvents()
    {
        inventoryBtn.onClick.AddListener(() =>
        {
            if(inventoryPanel.activeSelf)
            {
                PlayerParent.playerLookingInBag = false;
                PlayerAnimation.PlayIdleAnimation();
                weaponBtn.gameObject.SetActive(true);
                shieldJoystick.SetActive(true);
                inventoryPanel.SetActive(false);
            }else{
                PlayerParent.playerLookingInBag = true;
                //PlayerAnimation.PlayLookingInBagAnimation();
                weaponBtn.gameObject.SetActive(false);
                shieldJoystick.SetActive(false);
                inventoryPanel.SetActive(true);
            }
            
        });
    }

    void ResetPlayerAttackingMode()
    {
        PlayerParent.attackingModeDurationCtr  = 0;
        PlayerParent.isAttacking = true;
    }
}
