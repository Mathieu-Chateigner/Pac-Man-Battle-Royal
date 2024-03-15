using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public int timerExploration;

    public int timerBattle;

    private TMP_Text timerText;
    
    private bool m_boolExploration = false;

    public int actualValue;

    public List<Transform> listSpawnPoint;

    //private static GameManager instance;

    //public static GameManager Instance;
    
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static GameManager instance;
    #endregion

    public bool boolExploration
    {
        get {return m_boolExploration;}
        set {
            if (m_boolExploration == value) return;
            m_boolExploration = value;
            if (OnVariableChange != null)
                OnVariableChange(m_boolExploration);
        }
    
    }
    private bool m_boolBattle = false;

    public bool boolBattle
    {
        get {return m_boolBattle;}
        set {
            if (m_boolBattle == value) return;
            m_boolBattle = value;
            if (OnVariableChange != null)
                OnVariableChange(m_boolBattle);
        }
    
    }

    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariableChange;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(timerText.gameObject); 
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("instance");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //OnVariableChange += ExplorationEnded;
        //OnVariableChange += BattleEnded;
        StartCoroutine(SecondIteration());
       
    }

    private void ExplorationEnded(bool newVal)
    {
        Debug.Log("prout");
    }

    private void BattleEnded(bool newVal)
    {
        Debug.Log("end");
    }

    IEnumerator SecondIteration()
    {
        yield return CountDown(timerExploration);
        boolExploration = true;
        
        ChangeScene("Game");
        yield return CountDown(timerBattle);
        boolBattle = true;
    }

    IEnumerator CountDown(int timer)
    {
        for (int i = timer; i > 0; i--)
        {
            Debug.Log(i);
            //timerText.text = i.ToString();
            actualValue = i;
            yield return new WaitForSeconds(1);
        }

        
    }
    [PunRPC]
    public void ChangeSceneWithName(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    private void ChangeScene(string sceneName)
    {
        photonView.RPC("ChangeSceneWithName", RpcTarget.All, sceneName);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
