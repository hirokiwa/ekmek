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
    private bool zrButtonWasPressed;
    
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
        // joycon接続時に呼ばれる処理
        if (m_joyconR != null)
        {
            bool ZRButtonPressed = m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_2);
            
            // ZRボタンが押された瞬間を検出
            if (ZRButtonPressed && !zrButtonWasPressed)
            {
                SceneManager.LoadScene("CountDown");
            }

            xButtonWasPressed = ZRButtonPressed;
            
            
            if (m_joyconR.GetButtonDown(m_buttons[3]))
            {
                SceneManager.LoadScene("Tutorial");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("CountDown");
        }
        
    }

}
