using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedBoost : MonoBehaviour
{
    public Slider speedBoostSlider;
    private float maxSpeedBoost = 1.5f;
    private float minSpeedBoost = 1.0f;
    private float totalAcceleration;
    private float targetAcceleration = 150f; // 3秒間で目標とする加速度の合計値
    [HideInInspector] public float speedBoost;

    
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    m_joycons;
    private Joycon          m_joyconR;
    private Joycon.Button?  m_pressedButtonR;
    private JoyconManager joycon;
    private bool xButtonWasPressed;

    public static PlayerSpeedBoost instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        var m_joycons = JoyconManager.Instance.j;
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        m_joyconR = m_joycons.Find( c => !c.isLeft );
    }

    void Update()
    {
        float acceleration = AccelerationFilter.instance.last_accel_value * 13; // 仮の関数。実際の加速度取得メソッドに置き換える
        totalAcceleration += acceleration * Time.deltaTime; // 加速度の合計を更新

        // 目標加速度に対する現在の加速度の割合に基づいてSliderの値を更新
        float progress = Mathf.Clamp(totalAcceleration / targetAcceleration, 0f, 1f);
        speedBoostSlider.value = progress;

        // スライダーの値に基づいて速度増加を計算
        speedBoost = Mathf.Lerp(minSpeedBoost, maxSpeedBoost, speedBoostSlider.value);
    }
}