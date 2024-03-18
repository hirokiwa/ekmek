using UnityEngine;
using UnityEngine.UI;

public class CalorieManager : MonoBehaviour
{
  public Text JoyConScoreNumber;

  private int JconScore;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      HandleDebug();
    }
  }

  void HandleDebug()
  {
    Debug.Log("Space key pressed down!");
    setJsonScore(JconScore + 1);
    setJoyConScoreNumber(JconScore.ToString());
  }

  private void setJsonScore(int input)
  {
    JconScore = input;
  }

  private void setJoyConScoreNumber(string input)
  {
    JoyConScoreNumber.text = input;
  }

}
