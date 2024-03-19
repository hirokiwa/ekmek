using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject rankingCanvas;

    public static RankingManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RankingCanvasOn()
    {
        rankingCanvas.SetActive(true);
    }
    
    public void RankingCanvasOff()
    {
        rankingCanvas.SetActive(false);
    }
}
