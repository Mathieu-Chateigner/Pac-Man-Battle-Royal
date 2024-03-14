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

        [SerializeField]
        private GameObject gamePanel;
        
        [SerializeField]
        private GameObject loadingPanel;

        #endregion

        #region Private Fields
        
        private string _gameVersion;
        private bool _isConnecting;

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
            if (!_isConnecting) return;
            
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
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
            if (PhotonNetwork.CurrentRoom.PlayerCount != 1) return;
            
            PhotonNetwork.LoadLevel("Lobby");
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
                _isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = _gameVersion;
            }
        }

        #endregion
    }
}