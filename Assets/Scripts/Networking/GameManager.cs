using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Networking
{
    public class GameManager : MonoBehaviourPunCallbacks
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
            Debug.Log("LoadArena");
            
            if (!PhotonNetwork.IsMasterClient) return;
            
            Debug.Log("LoadArena2");
            
            PhotonNetwork.LoadLevel("Game");
        }

        #endregion
    }
}