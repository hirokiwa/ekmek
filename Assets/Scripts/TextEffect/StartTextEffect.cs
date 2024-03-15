using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // 追加
using DG.Tweening;      // 追加

public class StartTextEffect : MonoBehaviour
{
    public Text dotweenText;
    public float dotweenInterval;

    void Start()
    {
        dotweenText.DOFade(0.0f, dotweenInterval)   // アルファ値を0にしていく
            .SetLoops(-1, LoopType.Yoyo);    // 行き来を無限に繰り返す
    }
}