using UnityEngine;
using UnityEngine.UI;

public class CalorieManager : MonoBehaviour
{
  private const float ACCELERATION_THRESHOLD = 2;

  // 変更するテキスト
  public Text JoyConScoreNumber;

  // 生の加速度データ
  private float Acceleration;
  
  // 速度
  private float JoyConScore;

  public static CalorieManager instance;

  public void Awake(){
    if (instance == null)
    {
        instance = this;
    }
  }

  void Update()
  {
    setAcceleration(AccelerationFilter.instance?.last_accel_value ?? 0);
    if(Player.instance.isGameRunning){
      CountUpJoyConScore();
    }
    
    
  }

  /// <summary>
  /// 加速度が設定されたしきい値を超えている場合、その値を現在のスコアに加算
  /// </summary>
  void CountUpJoyConScore()
  {
    float additionalScore = Acceleration > ACCELERATION_THRESHOLD ? Acceleration : 0;
    updateScore(JoyConScore + additionalScore);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="input"></param>
  private void updateScore(float input)
  {
    setJoyConScore(input);
    setJoyConScoreNumber(input.ToString());
  }

  private void setAcceleration(float input)
  {
    Acceleration = input;
  }

  private void setJoyConScore(float input)
  {
    JoyConScore = input;
  }
  
  /// <summary>
  /// ここでアタッチしたテキストを引数のテキストに変更する
  /// </summary>
  /// <param name="input">表示するテキスト</param>
  private void setJoyConScoreNumber(string input)
  {
    JoyConScoreNumber.text = input;
  }

  public void resetCalorieScore() {
    updateScore(0);
  }

}
