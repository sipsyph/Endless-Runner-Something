using UnityEngine;

namespace SpeedTutorBattleRoyaleUI
{
    public class DamageArea : MonoBehaviour
    {
        [SerializeField] [Range(0, 50)] private float damageMult;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                UIController.instance.inDamageArea = true;
                UIController.instance.regenHealth = false;

                if (UIController.instance.currentArmourValue >= 0)
                {
                    UIController.instance.currentArmourValue -= Time.deltaTime * damageMult;
                    UIController.instance.UpdateUI();
                }

                if (UIController.instance.currentArmourValue <= 0 && UIController.instance.currentHealthValue >= 0)
                {
                    UIController.instance.currentHealthValue -= Time.deltaTime * damageMult;
                    UIController.instance.UpdateUI();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                UIController.instance.inDamageArea = false;
                UIController.instance.regenHealth = true;
            }
        }
    }
}
