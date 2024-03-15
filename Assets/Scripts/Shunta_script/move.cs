using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed =  0.002f;
    private Vector2 pos;

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
        

        if (Input.GetKey(KeyCode.W))    //上
        {

            pos.y = Mathf.Sin(Mathf.Deg2Rad * 90.0f) * speed;
            pos.x = 0;
            
        }
        else if (Input.GetKey(KeyCode.S))   //下
        {

            pos.y = Mathf.Sin(Mathf.Deg2Rad * 270.0f) * speed;
            pos.x = 0;
            
        }
        else if (Input.GetKey(KeyCode.D))   //右
        {
            pos.y = 0;
            pos.x = Mathf.Cos(Mathf.Deg2Rad * 0.0f) * speed;
        }
        else if (Input.GetKey(KeyCode.A))   //左
        {
            pos.y = 0;
            pos.x = Mathf.Cos(Mathf.Deg2Rad * 180.0f) * speed;
        }

        Rigidbody2D rd = GetComponent<Rigidbody2D>();
        rd.velocity = pos;

    }

}
