using UnityEngine;
using UnityEngine.UI;

public class CalorieManager : MonoBehaviour
{

  private const float DEFAULT_BODY_WAIGHT_KG = 60;

  public Text CalorieKCalNumber;

  private float CalorieKCal;

  public static CalorieManager instance;

  public void Awake(){
    if (instance == null)
    {
        instance = this;
    }
  }

  private float MToKm(float M)
  {
    return M / 1000;
  }


  private float calculateCalorieKCal(float DistanceKM, float BodyWeightKg)
  {
    return DistanceKM * BodyWeightKg;
  }

  private void updateCalorieKCal(float input)
  {
    setCalorieKCal(input);
    setCalorieKCalNumber(input.ToString());
  }

  private void setCalorieKCal(float input)
  {
    CalorieKCal = input;
  }

  private void setCalorieKCalNumber(string input)
  {
    CalorieKCalNumber.text = input;
  }

  public void resetCalorieKCalScore() {
    updateCalorieKCal(0);
  }

  public void CalorieKCalCalculateExecution() {
    float DistanceM = DistanceManager.instance.getDistanceM();
    float DistanceKm = MToKm(DistanceM);
    float calculatedCalorieKCal = calculateCalorieKCal(DistanceKm, DEFAULT_BODY_WAIGHT_KG);
    updateCalorieKCal(calculatedCalorieKCal);
  }

  public float getCalorieKCal() {
    return CalorieKCal;
  }

}
