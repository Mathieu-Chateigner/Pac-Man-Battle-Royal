using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class InRoomManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        public GameObject playerPrefab;

        #endregion
        
        #region Private Fields

        private readonly Dictionary<int, Vector3> _spawnPoints = new();

        #endregion
        
        private void Awake()
        {
            _spawnPoints.Add(1, new Vector3(-15, 20, 0));
            _spawnPoints.Add(2, new Vector3(15, -20, 0));
            _spawnPoints.Add(3, new Vector3(15, 20, 0));
            _spawnPoints.Add(4, new Vector3(-15, -20, 0));
        }

        private void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
                PhotonNetwork.Instantiate(playerPrefab.name, _spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount], Quaternion.identity);
            }
        }
    }
}