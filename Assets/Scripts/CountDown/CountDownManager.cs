using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownManager : MonoBehaviour
{
    public GameObject countdownCanvas;
    public Text countdownText;
    public int countdownDuration = 3;
    [HideInInspector] public float countdownTimer;

    public static CountDownManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        countdownTimer = countdownDuration;
        UpdateCountdownDisplay();
    }

    void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            UpdateCountdownDisplay();
            
            if (countdownTimer <= 0)
            {
                Player.speedBoost = PlayerSpeedBoost.instance.speedBoost;
                Time.timeScale = 1; 
                // countdownCanvas.SetActive(false);
                SceneManager.LoadScene("Main");
            }
        }
    }

    void UpdateCountdownDisplay()
    {
        // Mathf.CeilToIntを使用して、countdownTimerを整数に変換し、その値をテキストとして設定
        countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
    }
}