using Networking;
using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        [Tooltip("The current Health of our player")]
        public float health = 10f;

        private void Update()
        {
            if (!photonView.IsMine) return;
            
            if (health <= 0f)
            {
                //GameManager.Instance.LeaveRoom();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.CompareTag("Player"))
            {
                return;
            }
            
            health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.CompareTag("Player"))
            {
                return;
            }

            health -= 0.1f * Time.deltaTime;
        }
    }
}
