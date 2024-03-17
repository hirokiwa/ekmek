using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccelerationFilter : MonoBehaviour
{
    public static AccelerationFilter instance;
    
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    m_joycons;
    private Joycon          m_joyconL;
    private Joycon          m_joyconR;
    private Joycon.Button?  m_pressedButtonL;
    private Joycon.Button?  m_pressedButtonR;
    
    const float LowPassFilterFactor = 0.2f;

    private JoyconManager joycon;
    private Vector3 accel;
    private Vector3 lowPassValue;

    private bool lastStatus;
    private bool bDirectionUp;
    private int continueUpCount;
    private int continueUpFormerCount;
    private float minValue;
    private float maxValue;
    private float peakOfWave;
    private float valleyOfWave;

    public float last_accel_value;
    private float timeOfLastPeak;
    private float timeOfThisPeak;
    private float timeOfNow;
    private float threadThreshold;
    private float aveSpeed;
    
    const int valueNum = 4;
    private int tempCount;
    private float[] tempValue;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        var m_joycons = JoyconManager.Instance.j;
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        m_joyconL = m_joycons.Find( c =>  c.isLeft );
        m_joyconR = m_joycons.Find( c => !c.isLeft );

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.inputOptionIsSwitchController)
        {
            DetectNewStep(JoyConUpdate()); // ここで関数を呼び出す
        }
        
       
        //Joyconの動作チェック
        //1→A
        //0→B
        //2→Y
        //3→X
    }
    
    float JoyConUpdate() 
    {
        // Accel values:  x, y, z axis values (in Gs)
        accel = m_joyconL.GetAccel();
        Vector3 filteredAccelValue = FilterAccelValue(true, accel);
        var current_accel_value = filteredAccelValue.magnitude;
        return current_accel_value;
        
    }
    
    Vector3 FilterAccelValue(bool smooth, Vector3 accVal)
    {
        if (smooth)
            lowPassValue = Vector3.Lerp(lowPassValue, accVal, LowPassFilterFactor);
        else
            lowPassValue = accVal;

        return lowPassValue;
    }
    
    
    
    void DetectNewStep(float values)
    {
        if (last_accel_value <= 0f)
        {
            last_accel_value = values;
        }
        else
        {
            if (DetectorPeak(values, last_accel_value))
            {
                timeOfLastPeak = timeOfThisPeak;
                timeOfNow = Time.time;

                if (timeOfNow - timeOfLastPeak >= 0.1f
                    && (peakOfWave - valleyOfWave >= threadThreshold))
                {
                    timeOfThisPeak = timeOfNow;
                    aveSpeed = 1f / Peak_Valley_Thread(timeOfNow - timeOfLastPeak);
                }
            }
        }
        last_accel_value = values;
    }
    
    bool DetectorPeak(float newValue, float oldValue)
    {
        lastStatus = bDirectionUp;

        // wave up
        if (newValue >= oldValue)                 
        {
            bDirectionUp = true;
            continueUpCount++;
        }
        // wave down
        else {                                                           
            continueUpFormerCount = continueUpCount;
            continueUpCount = 0;
            bDirectionUp = false;
        }

        // 山
        if (!bDirectionUp && lastStatus
                          && (continueUpFormerCount >= 2 && (oldValue >= minValue && oldValue < maxValue)))
        {
            peakOfWave = oldValue;
            return true;
        }
        // 谷
        else if (!lastStatus && bDirectionUp)
        {
            valleyOfWave = oldValue;
            return false;
        }

        return false;
    }
    
    
    public float Peak_Valley_Thread(float value)
    {
        float tempThread = 1f;
        if (tempCount < valueNum)
        { 
            tempValue[tempCount] = value;
            tempCount++;
        }
        else
        { 
            tempThread = averageValue(tempValue, valueNum);
            for (int i = 1; i < valueNum; i++)
            {
                tempValue[i - 1] = tempValue[i];
            }
            tempValue[valueNum - 1] = value;
        }
        return tempThread;
    }

    public float averageValue(float[] value, int n)
    {
        float ave = 0;
        for (int i = 0; i < n; i++)
        {
            ave += value[i];
        }
        ave = ave / valueNum;
        return ave;
    }
    
}
