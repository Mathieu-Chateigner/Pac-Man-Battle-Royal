using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {

        #region Photon Callbacks
        
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }
        
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.Log($"GameManager::OnPlayerEnteredRoom() {other.NickName}");

            if (!PhotonNetwork.IsMasterClient) return;

            LoadArena();
        }
        
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log($"GameManager::OnPlayerEnteredRoom() {other.NickName}");
            
            if (!PhotonNetwork.IsMasterClient) return;

            LoadArena();
        }
        

        #endregion

        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
        
        #region Private Methods

        private void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.LoadLevel("WFCScene");
            }
        }

        #endregion
    }
}