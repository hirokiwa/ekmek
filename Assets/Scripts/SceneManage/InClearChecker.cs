using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using DG.Tweening;

// エサのカウントとクリア条件処理を含む
public class InClearChecker : MonoBehaviour
{
    
    public Tilemap food_tilemap; // エディタからエサのタイルマップをアサイン
    private int totalFoodCount;
    
    
    [HideInInspector] public int eatenFoodCount = 0; // 食べたエサの数
    [SerializeField] private GameObject ClearCanvas;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject GameOverCanvas;
    
    public static InClearChecker instance;

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
        CountFoodTiles();
    }

    // Update is called once per frame
    void CountFoodTiles()
    {
        totalFoodCount = 0;
        foreach (var pos in food_tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (food_tilemap.HasTile(localPlace))
            {
                totalFoodCount++;
            }
        }
    }

    public void CheckForWin()
    {
        if (eatenFoodCount >= totalFoodCount)
        {
            // クリア時間の計測を止める
            ClearTimeChecker.instance.isTimer = false;
            
            GameOverCanvas.SetActive(false);

            // ランキングのリーダーボードにスコア・クリア時間・カロリーを登録
            float Calorie = CalorieManager.instance.getCalorieKCal();
            float Calorie_int = Calorie;
            RankingManager.instance.AddScore(ScoreCountSystem.instance.score, ClearTimeChecker.instance.clearTime, Calorie_int);
            RankingManager.instance.DisplayRankings();
            RankingManager.instance.DisplayThisTimeScore(ScoreCountSystem.instance.score, ClearTimeChecker.instance.clearTime, Calorie_int);
            Debug.Log("Calorie_int" + Calorie);

            
            ClearCanvas.SetActive(true);
            UICanvas.SetActive(false);
            Player.instance.setIsGameRunning(false);
            CalorieManager.instance.CalorieKCalCalculateExecution();
            eatenFoodCount = 0;
            ScoreCountSystem.instance.ScoreReset();
            
        }
    }
}
