using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedBoost : MonoBehaviour
{
    public Slider speedBoostSlider;
    private float maxSpeedBoost = 1.5f;
    private float minSpeedBoost = 1.0f;
    public float speedBoost = 1.0f; // 外部からアクセス可能にすることで、他のスクリプトがこの値を使用できるように

    void Update()
    {
        float acceleration = AccelerationFilter.instance.last_accel_value; // 仮の関数。実際の加速度取得メソッドに置き換える
        if (acceleration > 3) // someThresholdは加速度が十分に大きいと見なす閾値
        {
            speedBoostSlider.value += acceleration * Time.deltaTime;
        }

        // スライダーの値に基づいて速度増加を計算
        speedBoost = Mathf.Lerp(minSpeedBoost, maxSpeedBoost, speedBoostSlider.value);
    }
}