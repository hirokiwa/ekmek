using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject rankingCanvas;

    public static RankingManager instance;
    
    // ランキングに表示する最大エントリ数
    private const int MaxRankingEntries = 5;

    // スコアを保存する際のキー
    private const string ScoresKey = "Scores";
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // スコア、時間、数字Aの組を表すクラス
    private class ScoreEntry
    {
        public int Score;
        public int Time;
        public int NumberA;

        public ScoreEntry(int score, int time, int numberA)
        {
            Score = score;
            Time = time;
            NumberA = numberA;
        }
    }

    // スコアを追加する
    public void AddScore(int score, int time, int numberA)
    {
        List<ScoreEntry> scores = GetScores();
        scores.Add(new ScoreEntry(score, time, numberA));
        
        // スコアで降順にソート
        scores.Sort((x, y) => y.Score.CompareTo(x.Score));

        // 最大エントリ数を超えたら削除
        if (scores.Count > MaxRankingEntries)
        {
            scores.RemoveRange(MaxRankingEntries, scores.Count - MaxRankingEntries);
        }

        SaveScores(scores);
    }

    // スコアをPlayerPrefsに保存
    private void SaveScores(List<ScoreEntry> scores)
    {
        string scoreString = "";
        foreach (var score in scores)
        {
            if (scoreString != "") scoreString += "|";
            scoreString += $"{score.Score},{score.Time},{score.NumberA}";
        }

        PlayerPrefs.SetString(ScoresKey, scoreString);
        PlayerPrefs.Save();
    }

    // 保存されたスコアを取得
    private List<ScoreEntry> GetScores()
    {
        List<ScoreEntry> scores = new List<ScoreEntry>();
        string savedScores = PlayerPrefs.GetString(ScoresKey, "");

        if (!string.IsNullOrEmpty(savedScores))
        {
            string[] scoreEntries = savedScores.Split('|');
            foreach (var entry in scoreEntries)
            {
                string[] details = entry.Split(',');
                if (details.Length == 3)
                {
                    scores.Add(new ScoreEntry(int.Parse(details[0]), int.Parse(details[1]), int.Parse(details[2])));
                }
            }
        }

        return scores;
    }

    // ランキングをコンソールに表示
    public void DisplayRankings()
    {
        List<ScoreEntry> scores = GetScores();

        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log($"Rank {i + 1}: Score = {scores[i].Score}, Time = {scores[i].Time}, Number A = {scores[i].NumberA}");
        }
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
