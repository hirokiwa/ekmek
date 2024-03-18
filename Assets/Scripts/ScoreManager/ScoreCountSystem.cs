using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ScoreCountSystem : MonoBehaviour
{
    public Text scoreText; // 現在のスコアを表示するテキスト
    public Text highScoreText; // ハイスコアを表示するテキスト
    private int score; // 現在のスコア
    private int highScore; // ハイスコア

    public static ScoreCountSystem instance;
    private int consecutiveEatenEnemies = 0; // 連続して食べた敵の数

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
        score = 0; // スコアを0に初期化
        highScore = PlayerPrefs.GetInt("HighScore", 0); // 保存されたハイスコアをロード、なければ0
        UpdateScoreText(); // スコアテキストを更新
        UpdateHighScoreText(); // ハイスコアテキストを更新
    }

    // スコアを増加させるメソッド
    public void AddScore(int amount)
    {
        score += amount; // スコアを増加
        UpdateScoreText(); // スコアテキストを更新

        // 現在のスコアがハイスコアを超えた場合はハイスコアを更新
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // 新しいハイスコアを保存
            UpdateHighScoreText(); // ハイスコアテキストを更新
        }
    }

    public void AddScorePowerEsa()
    {
        int[] scores = new int[] { 400, 800, 1600 }; // 加算するスコアの配列
        if (consecutiveEatenEnemies >= scores.Length)
        {
            consecutiveEatenEnemies = scores.Length - 1; // 配列の最大インデックスを超えないようにする
        }

        int scoreToAdd = scores[consecutiveEatenEnemies]; // 加算するスコアを決定
        AddScore(scoreToAdd); // スコアを加算
        consecutiveEatenEnemies++; // 連続して食べた敵の数を増加
    }
    
    public void ResetConsecutiveEatenEnemies()
    {
        consecutiveEatenEnemies = 0; // 連続して食べた敵の数をリセット
    }

    // スコアテキストを更新するメソッド
    private void UpdateScoreText()
    {
        if(scoreText != null) 
        {
            scoreText.text = score.ToString();
        }
    }

    // ハイスコアテキストを更新するメソッド
    private void UpdateHighScoreText()
    {
        if(highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

    public void ScoreReset()
    {
        score = 0;
    }
}