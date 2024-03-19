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
            
            // ランキングのリーダーボードにスコア・クリア時間・カロリーを登録
            float Calorie = CalorieManager.instance.getCalorieKCal();
            int Calorie_int = (int)Calorie;
            RankingManager.instance.AddScore(ScoreCountSystem.instance.score, ClearTimeChecker.instance.clearTime, Calorie_int);
            RankingManager.instance.DisplayRankings();

            
            ClearCanvas.SetActive(true);
            Player.instance.setIsGameRunning(false);
            CalorieManager.instance.CalorieKCalCalculateExecution();
            eatenFoodCount = 0;
            ScoreCountSystem.instance.ScoreReset();
            // var sequence = DOTween.Sequence(); //Sequence生成

            DOVirtual.DelayedCall(3, () => SceneManager.LoadScene("Main"));
            //
            // sequence.Append(DOVirtual.DelayedCall(2, () => TilemapReseter.instance.ResetTiles()))
            //     .Join(DOVirtual.DelayedCall(3, () => Time.timeScale = 0))
            //     .Join(DOVirtual.DelayedCall(2, () => Player.transform.position = new Vector2(0f, -16.0f)))
            //     .Join(DOVirtual.DelayedCall(2, () => food_tilemap.gameObject.SetActive(false)))
            //     .Join(DOVirtual.DelayedCall(2, () => UICanvas.gameObject.SetActive(false)))
            //     .Join(DOVirtual.DelayedCall(3, () => food_tilemap.gameObject.SetActive(true)));
        }
    }
}
