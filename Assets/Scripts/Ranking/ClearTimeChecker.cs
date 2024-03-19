using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearTimeChecker : MonoBehaviour
{
    [HideInInspector] public int clearTime;
    private float countTime;
    [HideInInspector] public bool isTimer;

    public static ClearTimeChecker instance;
    
    void Awake()
    {
        clearTime = 0;
        isTimer = true;

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // Debug.Log((clearTime / 60).ToString().PadLeft(2, '0') + ":" + (clearTime % 60).ToString().PadLeft(2, '0'));
    }

    private void FixedUpdate()
    {
        if (isTimer)
        {
            countTime  += Time.deltaTime;
            clearTime = (int)countTime;
        }
    }
}
