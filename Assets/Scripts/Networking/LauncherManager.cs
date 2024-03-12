using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Networking
{
    public class LauncherManager : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [SerializeField]
        private int maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields
        
        private string _gameVersion;

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        private void Start()
        {
            _gameVersion = Application.version;
        }

        #endregion
        
        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning($"OnDisconnected() was called by PUN with reason {cause}.");
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Player joined.");
        }

        #endregion

        #region Public Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = _gameVersion;
            }
        }

        #endregion
    }
}