using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetValueFromGameManager : MonoBehaviour
{
    public TMP_Text text;

    private GameManager gm; 
    //private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
        //Debug.Log(gm.actualValue);
        
    }
    

    private void Update()
    { 
        text.text = gm.actualValue.ToString();
    }
}
