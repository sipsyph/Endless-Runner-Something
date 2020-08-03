using UnityEngine;
using UnityEngine.UI;

namespace SpeedTutorBattleRoyaleUI
{
    public class PlayerRaycast : MonoBehaviour
    {
        [Header("Raycast Length/Layer")]
        [SerializeField] private int rayLength = 10;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private LayerMask layerToExclude;
        private ItemController raycasted_obj;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair;
        private bool isCrosshairActive;
        private bool doOnce;

        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int Mask = 1 << layerToExclude.value | layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, rayLength, Mask))
            {
                 if (hit.collider.CompareTag("Pickup"))
                {
                    if (!doOnce)
                    {
                        raycasted_obj = hit.collider.gameObject.GetComponent<ItemController>();
                        CrosshairChange(true);
                    }

                    isCrosshairActive = true;
                    doOnce = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        raycasted_obj.ObjectInteraction();
                    }
                }
            }

            else
            {
                if (isCrosshairActive)
                {
                    CrosshairChange(false);
                    doOnce = false;
                }
            }
        }

        void CrosshairChange(bool on)
        {
            if (on && !doOnce)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }
}
