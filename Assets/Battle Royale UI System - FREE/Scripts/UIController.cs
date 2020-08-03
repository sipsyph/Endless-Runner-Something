using UnityEngine;
using UnityEngine.UI;

namespace SpeedTutorBattleRoyaleUI
{
    public class UIController : MonoBehaviour
    {
        [Header("Generics Parameters")]
        [SerializeField] private string playerName;
        [SerializeField] private Color nameColour;
        public float playerCash;

        [Header("Health Parameters")]
        [Range(0, 100)]public float currentHealthValue;
        [SerializeField] private float maximumHealthBars = 100f;
        private float currentHealthTimer;
        [SerializeField] private float maxHealthTimer;
        public bool regenHealth;
        public bool inDamageArea;

        [Header("Armour Parameters")]
        [Range(0, 300)] public float currentArmourValue;
        public float playerArmourAmount;
        [SerializeField] private float maxArmourTimer;
        private float currentArmourTimer;

        [Header("Armour Equip Key")]
        [SerializeField] private KeyCode equipArmourKey;

        [Header("References")]
        [SerializeField] private Text nameUI;
        [SerializeField] private Text cashUI;
        [SerializeField] private Text armourAmount;
        [SerializeField] private Image normalHealthUI;
        [SerializeField] private Image armourHealthUI1;
        [SerializeField] private Image armourHealthUI2;
        [SerializeField] private Image armourHealthUI3;

        public static UIController instance;

        void Awake()
        {
            if (instance != null)
            { /*Destroy(gameObject);*/ }
            else
            { instance = this; /*DontDestroyOnLoad(gameObject);*/ }

            currentHealthTimer = maxHealthTimer;
            currentArmourTimer = maxArmourTimer;
            UpdateUI();
        }

        public void UpdateArmourAmount(float armourValue)
        {
            playerArmourAmount += armourValue;
            armourAmount.text = playerArmourAmount.ToString("0");
        }

        public void UpdateCashUI(float cashValue)
        {
            playerCash += cashValue;
            cashUI.text = playerCash.ToString("0");
        }

        private void Update()
        {
            if (regenHealth)
            {
                if (currentHealthValue <= maximumHealthBars)
                {
                    currentHealthTimer -= Time.deltaTime;

                    if (currentHealthTimer <= 0)
                    {
                        currentHealthValue += Time.deltaTime * 10;
                        UpdateUI();
                        currentHealthTimer = 0;

                        if (currentHealthValue >= maximumHealthBars)
                        {
                            currentHealthTimer = maxHealthTimer;
                            regenHealth = false;
                        }
                    }
                }
                else
                {
                    regenHealth = false;
                    currentHealthTimer = maxHealthTimer;
                }
            }

            if (Input.GetKey(equipArmourKey))
            {
                if (playerArmourAmount >= 1)
                {
                    currentArmourTimer -= Time.deltaTime;

                    if (currentArmourTimer <= 0)
                    {
                        if (currentArmourValue < 300)
                        {
                            playerArmourAmount--;

                            if (currentArmourValue <= 99)
                            {
                                currentArmourValue = 100;
                            }

                            else if (currentArmourValue >= 100 && currentArmourValue <= 199)
                            {
                                currentArmourValue = 200;
                            }

                            else if (currentArmourValue >= 200 && currentArmourValue <= 299)
                            {
                                currentArmourValue = 300;
                            }

                            UpdateUI();
                            currentArmourTimer = maxArmourTimer;
                        }
                    }
                }
            }
            if (Input.GetKeyUp(equipArmourKey))
            {
                currentArmourTimer = maxArmourTimer;
            }
        }

        public void UpdateUI()
        {
            nameUI.text = playerName;
            nameUI.color = nameColour;
            cashUI.text = playerCash.ToString("0");
            armourAmount.text = playerArmourAmount.ToString("0");

            if (currentHealthValue >= 1)
            {
                normalHealthUI.fillAmount = currentHealthValue / maximumHealthBars;
                if (!inDamageArea)
                {
                    regenHealth = true;
                }

                if (currentHealthValue >= maximumHealthBars)
                {
                    normalHealthUI.fillAmount = maximumHealthBars / maximumHealthBars;
                    regenHealth = false;
                    currentHealthTimer = maxHealthTimer;
                }
            }
            else
            {
                normalHealthUI.fillAmount = 0;
            }

            if (currentArmourValue >= 1)
            {
                armourHealthUI1.fillAmount = currentArmourValue / maximumHealthBars;

                if (currentArmourValue >= maximumHealthBars)
                {
                    armourHealthUI1.fillAmount = maximumHealthBars / maximumHealthBars;
                }
            }
            else
            {
                armourHealthUI1.fillAmount = 0;
            }

            if (currentArmourValue >= 101)
            {
                armourHealthUI2.fillAmount = (currentArmourValue - maximumHealthBars) / maximumHealthBars;

                if (currentArmourValue >= 200)
                {
                    armourHealthUI2.fillAmount = maximumHealthBars / maximumHealthBars;
                }
            }
            else
            {
                armourHealthUI2.fillAmount = 0;
            }

            if (currentArmourValue >= 201)
            {
                armourHealthUI3.fillAmount = (currentArmourValue - 200) / maximumHealthBars;

                if (currentArmourValue >= 300)
                {
                    armourHealthUI3.fillAmount = maximumHealthBars / maximumHealthBars;
                }
            }
            else
            {
                armourHealthUI3.fillAmount = 0;
            }
        }
    }
}
