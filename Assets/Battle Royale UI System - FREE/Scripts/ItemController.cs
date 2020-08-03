using UnityEngine;

namespace SpeedTutorBattleRoyaleUI
{
    public class ItemController : MonoBehaviour
    {
        [Header("Item Type")]
        [SerializeField] private bool armour;
        [SerializeField] private bool cash;

        [Header("Item Parameters")]
        [SerializeField] private float itemValue;

        public void ObjectInteraction()
        {
            if (armour)
            {
                UIController.instance.UpdateArmourAmount(itemValue);
                this.gameObject.SetActive(false);
            }

            if (cash)
            {
                UIController.instance.UpdateCashUI(itemValue);
                this.gameObject.SetActive(false);
            }
        }
	}
}
