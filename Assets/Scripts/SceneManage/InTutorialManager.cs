using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InTutorialManager : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    m_joycons;
    private Joycon          m_joyconR;
    private Joycon.Button?  m_pressedButtonR;
    private JoyconManager joycon;
    private bool xButtonWasPressed;
    private bool zruttonWasPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        var m_joycons = JoyconManager.Instance.j;
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        m_joyconR = m_joycons.Find( c => !c.isLeft );
        xButtonWasPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // joycon接続時に呼ばれる処理
        if (m_joyconR != null)
        {
            if (m_joyconR.GetButtonDown(m_buttons[3]))
            {
                SceneManager.LoadScene("Start");
            }
        }
    }
}
