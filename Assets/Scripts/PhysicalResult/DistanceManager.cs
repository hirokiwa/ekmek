using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
  private const float ACCELERATION_THRESHOLD = 2;

  public Text DistanceNumber;

  private float Acceleration;
  private float Distance;

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
      CountUpDistance();
    }
  }

  void CountUpDistance()
  {
    float additionalScore = Acceleration > ACCELERATION_THRESHOLD ? Acceleration : 0;
    updateDistance(Distance + additionalScore);
  }

  private void updateDistance(float input)
  {
    setDistance(input);
    setDistanceNumber(input.ToString());
  }

  private void setAcceleration(float input)
  {
    Acceleration = input;
  }

  private void setDistance(float input)
  {
    Distance = input;
  }

  private void setDistanceNumber(string input)
  {
    DistanceNumber.text = input;
  }

  public void resetDistance() {
    updateDistance(0);
  }

  public float getDistance() {
    return Distance;
  }

}
