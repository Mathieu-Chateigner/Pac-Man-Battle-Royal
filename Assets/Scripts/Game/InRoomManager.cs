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
        private readonly Dictionary<int, bool> _spawnUsed = new();

        #endregion
        
        private void Awake()
        {
            _spawnPoints.Add(1, new Vector3(-15, 20, 0));
            _spawnPoints.Add(2, new Vector3(15, -20, 0));
            _spawnPoints.Add(3, new Vector3(15, 20, 0));
            _spawnPoints.Add(4, new Vector3(-15, -20, 0));
            
            _spawnUsed.Add(1, false);
            _spawnUsed.Add(2, false);
            _spawnUsed.Add(3, false);
            _spawnUsed.Add(4, false);
        }

        private void Start()
        {
            Debug.Log("Start InRoomManager");
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
                var selectedSpawnPoint = SelectSpawnPoint();
                PhotonNetwork.Instantiate(playerPrefab.name, selectedSpawnPoint, Quaternion.identity);
            }
        }

        private Vector3 SelectSpawnPoint()
        {
            while (true)
            {
                var id = Random.Range(1, 5);

                if (_spawnUsed[id]) continue;
                CallSendSpawnPointUsed(id);
                return _spawnPoints[id];
            }
        }

        [PunRPC]
        private void SendSpawnPointUsed(int id)
        {
            _spawnUsed[id] = true;
        }

        private void CallSendSpawnPointUsed(int id)
        {
            photonView.RPC("SendSpawnPointUsed", RpcTarget.All, id);
        }
    }
}