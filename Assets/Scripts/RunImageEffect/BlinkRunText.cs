using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlinkRunText : MonoBehaviour
{
    public Text blinkRunText;
    public float dotweenInterval;

    public static BlinkRunText instance;
    private Tween loopBlinkText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        loopBlinkText.SetRecyclable(true);
    }

    // Start is called before the first frame update
    public void RunTextBlink()
    {
        blinkRunText.DOFade(1.0f, 0); // テキストの不透明度を即座に1.0（完全に不透明）にリセット
        loopBlinkText = blinkRunText.DOFade(0.0f, dotweenInterval)   // アルファ値を0にしていく
                .SetLoops(-1, LoopType.Yoyo)
                .SetAutoKill(false);    
    }

    public void KillBlink()
    {
        if(loopBlinkText != null)
        {
            loopBlinkText.Kill(); // 現在の点滅TweenをKill
        }
    }
}
