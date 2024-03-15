using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Management : MonoBehaviour
{
    public static Management instance;
    public bool power;
    public bool stop;
    public float powerTime = 0.0f;
    public float pub_timer;
    public float pub_ntimer;

    private float timer;
    private float ntimer;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        power = false;
        stop = false;
        timer = 0.0f;
        powerTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    { 
        //???????????
        if (power == true)
        {
            timer += Time.deltaTime;
            pub_timer = timer;

            if (timer >= powerTime)
            {
                timer = 0.0f;
                pub_timer = 0.0f;
                powerTime = 0.0f;

                Tracking_oikake.instance.power_o = false;
                Tracking_pinky.instance.power_p = false;
                Tracking_gusta.instance.power_g = false;
                power = false;

                Tracking_oikake.instance.flag_o = true;
                Tracking_pinky.instance.flag_p = true;
                Tracking_gusta.instance.flag_g = true;

            }
        }
        else
        {
            //??????????
            ntimer += Time.deltaTime;
            pub_ntimer = ntimer;
            if (ntimer > 40)
            {
                ntimer = 0.0f;
                pub_ntimer = 0.0f;
            }
        }

    }
    
    
    public IEnumerator Restart(NavMeshAgent o, NavMeshAgent p, NavMeshAgent g, GameObject gameObject)
    {

        Management.instance.stop = true;

        yield return new WaitForSeconds(2.0f);
        

        Vector3 pos_o = new Vector3(-3.0f, 0f, 0f);
        Vector3 pos_p = new Vector3(3.0f, 0f, 0f);
        Vector3 pos_g = new Vector3(0f, 0f, 0f);

        gameObject.transform.position = new Vector2(0f, -16.0f);
        o.Warp(pos_o);
        p.Warp(pos_p);
        g.Warp(pos_g);

        ntimer = 0.0f;
        pub_ntimer = 0.0f;

        yield return new WaitForSeconds(2.0f);

        Management.instance.stop = false;
    }

    public IEnumerator Pause(float n)
    {

        Management.instance.stop = true;

        yield return new WaitForSeconds(n);

        Management.instance.stop = false;
    }

    


}
