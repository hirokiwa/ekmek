using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class InClearManager : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    m_joycons;
    private Joycon          m_joyconR;
    private Joycon.Button?  m_pressedButtonR;
    private JoyconManager joycon;
    private bool xButtonWasPressed;
    
    [SerializeField] private GameObject[] objectsToActiveFalse;
    [SerializeField] private GameObject[] objectsToActiveTrue;
    
    // Start is called before the first frame update
    void Start()
    {
        var m_joycons = JoyconManager.Instance.j;
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        m_joyconR = m_joycons.Find( c => !c.isLeft );
        xButtonWasPressed = false;
        
    }

    private void Update()
    {
        if (m_joyconR != null)
        {
            bool ZRButtonPressed = m_joyconR.GetButton(Joycon.Button.SHOULDER_2);

            // Xボタンが押された瞬間を検出
            if (ZRButtonPressed && !xButtonWasPressed)
            {
                // ToggleObjects();
                SceneManager.LoadScene("Main");
            }

            xButtonWasPressed = ZRButtonPressed;
            
            if (m_joyconR.GetButtonUp(m_buttons[3]))
            {
                RankingManager.instance.RankingCanvasOn();
            }
        }
        
        if (!Player.instance.inputOptionIsSwitchController)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ToggleObjects();
                SceneManager.LoadScene("Main");
            }
            
            if (Input.GetKeyDown(KeyCode.X))    
            {
                RankingManager.instance.RankingCanvasOn();
            }
        }
    }

    private void ToggleObjects()
    {
        foreach (var obj in objectsToActiveFalse)
        {
            obj.SetActive(false);
            Player.instance.setIsGameRunning(true);
        }
        
        foreach (var obj in objectsToActiveTrue)
        {
            obj.SetActive(true);
            Player.instance.setIsGameRunning(false);
            CalorieManager.instance.CalorieKCalCalculateExecution();
        }
    }

}
