using UnityEngine;
using Photon.Pun;
using TMPro;

namespace UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField playerNameInputField;
        
        #region Private Constants

        private const string PlayerNamePrefKey = "PlayerName";

        #endregion

        #region MonoBehaviour CallBacks
        
        private void Start () {

            var defaultName = string.Empty;
            if (playerNameInputField != null)
            {
                if (PlayerPrefs.HasKey(PlayerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                    playerNameInputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName =  defaultName;
        }

        #endregion

        #region Public Methods
        
        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogWarning("Player Name is null or empty");
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(PlayerNamePrefKey,value);
        }

        #endregion
    }
}