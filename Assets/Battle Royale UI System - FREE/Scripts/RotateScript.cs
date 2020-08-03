using UnityEngine;

namespace SpeedTutorBattleRoyaleUI
{
    public class RotateScript : MonoBehaviour
    {
        [SerializeField] private int rotateSpeedX;
        [SerializeField] private int rotateSpeedY;
        [SerializeField] private int rotateSpeedZ;

        private void Update()
        {
            this.gameObject.transform.Rotate(rotateSpeedX, rotateSpeedY, rotateSpeedZ);
            Physics.SyncTransforms();
        }
    }
}
