using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] public GameObject rankingCanvas;
    
    //1プレイのスコア表示用
    public Text SCORE;
    public Text TIME;
    public Text KCAL;
    
    // ランキング表示用のテキストUI
    public Text[] scoreTexts; // スコア表示用テキスト配列
    public Text[] timeTexts;  // クリア時間表示用テキスト配列
    public Text[] kcalTexts;  // 消費カロリー表示用テキスト配列
    
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
    public void AddScore(int score, int time, int kcal)
    {
        List<ScoreEntry> scores = GetScores();
        scores.Add(new ScoreEntry(score, time, kcal));
        
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
    
    /// <summary>
    /// 今回のスコア（スコア＋経過時間＋消費カロリー）を一番上に表示するための処理
    /// </summary>
    public void DisplayThisTimeScore(int score, int time, int kcal)
    {
        SCORE.text = score.ToString();
        // ここで整数値で計算してた経過時間をXX:XXに形式に計算直す
        TIME.text = (time / 60).ToString().PadLeft(2, '0') + ":" + (time % 60).ToString().PadLeft(2, '0');
        KCAL.text = kcal.ToString();

    }

    // ランキングをコンソールに表示
    public void DisplayRankings()
    {
        List<ScoreEntry> scores = GetScores();

        // 各テキストUIを更新
        for (int i = 0; i < MaxRankingEntries; i++)
        {
            if (i < scores.Count)
            {
                ScoreEntry entry = scores[i];
                scoreTexts[i].text = entry.Score.ToString();
                timeTexts[i].text = (entry.Time / 60).ToString().PadLeft(2, '0') + ":" + (entry.Time % 60).ToString().PadLeft(2, '0');
                kcalTexts[i].text = entry.NumberA.ToString(); // NumberAが消費カロリーに相当
            }
            else
            {
                // スコアが足りない場合は空にする
                scoreTexts[i].text = "";
                timeTexts[i].text = "";
                kcalTexts[i].text = "";
            }
        }
    }

    public void RankingCanvasOn()
    {
        rankingCanvas.SetActive(!rankingCanvas.activeSelf);
    }
    
    public void RankingCanvasOff()
    {
        rankingCanvas.SetActive(false);
    }
}
