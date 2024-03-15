using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTimerValue : MonoBehaviour
{
    public TMP_Text textTimer;
    private GameManager gm;
    private bool isGMHere;

    private void Awake()
    {
        gm = GameManager.Instance;
        if (gm != null)
        {
            isGMHere = true;
        }
    }

    private void Update()
    {
        if (isGMHere)
        {
            textTimer.text = gm.actualValue.ToString();
        }
        
    }
}
