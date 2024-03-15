using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconMove : MonoBehaviour
{
    float speed = 0.002f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Operation();
    }

    void Operation()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.JoystickButton1))  //上
        {
            position.y += speed;
        }
        else if (Input.GetKey(KeyCode.JoystickButton2)) //下
        {
            position.y -= speed;
        }
        else if (Input.GetKey(KeyCode.JoystickButton0)) //右
        {
            position.x += speed;
        }
        else if (Input.GetKey(KeyCode.JoystickButton3)) //左
        {
            position.x -= speed;
        }

        transform.position = position;
    }

}
