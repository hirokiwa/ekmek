using UnityEngine;
using UnityEngine.UI;

public class CalorieManager : MonoBehaviour
{
  private const float ACCELERATION_THRESHOLD = 2;

  public Text JoyConScoreNumber;

  private float Acceleration;
  private float JconScore;

  void Update()
  {
    Debug.Log(AccelerationFilter.instance.last_accel_value);

    setAcceleration(AccelerationFilter.instance?.last_accel_value ?? 0);
    CountUpJoyConScore();
  }

  void CountUpJoyConScore()
  {
    float additionalScore = Acceleration > ACCELERATION_THRESHOLD ? Acceleration : 0;
    updateScore(JconScore + additionalScore);
  }

  private void updateScore(float input)
  {
    setJsonScore(input);
    setJoyConScoreNumber(input.ToString());
  }

  private void setAcceleration(float input)
  {
    Acceleration = input;
  }

  private void setJsonScore(float input)
  {
    JconScore = input;
  }

  private void setJoyConScoreNumber(string input)
  {
    JoyConScoreNumber.text = input;
  }

}