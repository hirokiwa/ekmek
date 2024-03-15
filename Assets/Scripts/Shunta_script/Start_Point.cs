using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Start_Point : MonoBehaviour
{

    public NavMeshAgent oikake;
    public NavMeshAgent pinky;
    public NavMeshAgent gusta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Return"))
            {

                other.gameObject.layer = LayerMask.NameToLayer("Enemy");

                if(other.gameObject.name == "oikake") {

                    oikake.velocity = Vector2.zero;


                    Tracking_oikake.instance.power_o = false;
                    Tracking_oikake.instance.pause = false;
                    Tracking_oikake.instance.flag_o = true;

                }

                if (other.gameObject.name == "pinky"){

                    pinky.velocity = Vector2.zero;


                    Tracking_pinky.instance.power_p = false;
                    Tracking_pinky.instance.pause = false;
                    Tracking_pinky.instance.flag_p = true;
                }

                if (other.gameObject.name == "gusta")
                {

                    gusta.velocity = Vector2.zero;


                    Tracking_gusta.instance.power_g = false;
                    Tracking_gusta.instance.pause = false;
                    Tracking_gusta.instance.flag_g = true;
                }



            }
            


        }
    }
}
