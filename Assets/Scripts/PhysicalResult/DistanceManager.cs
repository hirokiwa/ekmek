using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
  private const float ACCELERATION_THRESHOLD = 2;
  private const float ADJUST_VARIABLE = 100;

  public Text DistanceMNumber;

  private float Acceleration;
  private float DistanceM;

  public static DistanceManager instance;

  public void Awake(){
    if (instance == null)
    {
      instance = this;
    }
  }

  void Update()
  {
    Debug.Log(AccelerationFilter.instance.last_accel_value);

    setAcceleration(AccelerationFilter.instance?.last_accel_value ?? 0);
    if(Player.instance.isGameRunning){
      CountUpDistanceM();
    }
  }

  void CountUpDistanceM()
  {
    float AdditionalDistanceMBase = Acceleration > ACCELERATION_THRESHOLD ? Acceleration : 0;
    float AdditionalDistanceM = AdditionalDistanceMBase / ADJUST_VARIABLE;

    updateDistanceM(DistanceM + AdditionalDistanceM);
  }

  private void updateDistanceM(float input)
  {
    setDistanceM(input);
    string inputText = input.ToString();
    setDistanceMNumber(input.ToString());
  }

  private void setAcceleration(float input)
  {
    Acceleration = input;
  }

  private void setDistanceM(float input)
  {
    DistanceM = input;
  }

  private void setDistanceMNumber(string input)
  {
    DistanceMNumber.text = input;
  }

  public void resetDistanceM() {
    updateDistanceM(0);
  }

  public float getDistanceM() {
    return DistanceM;
  }

}
