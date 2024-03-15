using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InStartManager : MonoBehaviour
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

        Time.timeScale = 0;
    }

    private void Update()
    {
        if (m_joyconR != null)
        {
            bool xButtonPressed = m_joyconR.GetButton(Joycon.Button.DPAD_UP);

            // Xボタンが押された瞬間を検出
            if (xButtonPressed && !xButtonWasPressed)
            {
                Time.timeScale = 1; 
                ToggleObjects();
            }

            xButtonWasPressed = xButtonPressed;
        }
    }

    private void ToggleObjects()
    {
        foreach (var obj in objectsToActiveFalse)
        {
            obj.SetActive(false);
        }
        
        foreach (var obj in objectsToActiveTrue)
        {
            obj.SetActive(true);
        }
    }

    
}
