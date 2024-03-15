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