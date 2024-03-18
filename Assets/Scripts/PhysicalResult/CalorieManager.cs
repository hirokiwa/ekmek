using UnityEngine;
using UnityEngine.UI;

public class CalorieManager : MonoBehaviour
{

  private const float DEFAULT_BODY_WAIGHT = 60;

  public Text CalorieNumber;

  private float Calorie;

  public static CalorieManager instance;

  public void Awake(){
    if (instance == null)
    {
        instance = this;
    }
  }


  private float calculateCalorie(float Distance, float BodyWeight)
  {
    return Distance * BodyWeight;
  }

  private void updateCalorie(float input)
  {
    setCalorie(input);
    setCalorieNumber(input.ToString());
  }

  private void setCalorie(float input)
  {
    Calorie = input;
  }

  private void setCalorieNumber(string input)
  {
    CalorieNumber.text = input;
  }

  public void resetCalorieScore() {
    updateCalorie(0);
  }

  public void CalorieCalculateExecution() {
    float distance = DistanceManager.instance.getDistance();
    float calculatedCalorie = calculateCalorie(distance, DEFAULT_BODY_WAIGHT);
    updateCalorie(calculatedCalorie);
  }

}
