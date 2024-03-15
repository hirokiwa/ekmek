using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tracking_pinky : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform target;
    public Transform start_point;
    public static Tracking_pinky instance;
    public bool pause;
    public bool ram_p;
    public bool power_p;

    private Vector2 rand_pos;
    private float speed = 0.0f; //初期速度
    private float distance;
    private bool w;

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    SpriteRenderer sr;
    public bool flag_p = false;

    public Transform nawabari;

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

        pause = false;
        ram_p = true;
        power_p = false;
        speed = agent.speed;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Management.instance.stop)
        {
            if (pause == true)
            {

                agent.speed = 30.0f;
                agent.velocity = (agent.steeringTarget - transform.position).normalized * agent.speed;

                Vector2 opos = start_point.position;
                agent.SetDestination(opos);

            }
            else
            {

                if (power_p == false)
                {
                    if (flag_p)
                    {
                        sr.sprite = s1;
                        flag_p = false;
                    }

                    if (!w) agent.speed = speed;
                    else agent.speed = speed * 0.5f;

                    ram_p = true;
                    Vector2 pos = target.position;

                    if (Management.instance.pub_ntimer < 8)
                    {
                        Vector2 n_pos = nawabari.position;
                        agent.SetDestination(n_pos);
                    }
                    else
                    {
                        distance = Vector3.Distance(transform.position, target.transform.position);

                        if (7 < distance)
                        {
                            if (Player.instance.v == 0)
                            {
                                pos.x -= 5;
                            }
                            else if (Player.instance.v == 1)
                            {
                                pos.x += 5;
                            }
                            else if (Player.instance.v == 2)
                            {
                                pos.y -= 5;
                            }
                            else if (Player.instance.v == 3)
                            {
                                pos.y += 5;
                            }
                        }
                    
                        agent.SetDestination(pos);
                    }
                    
                }
                else
                {

                    if (flag_p)
                    {
                        sr.sprite = s2;
                        flag_p = false;
                    }

                    if (Management.instance.pub_timer > (Management.instance.powerTime - 3))
                    {
                        sr.sprite = s3;
                        flag_p = false;
                    }

                    if (ram_p)
                    {

                        Vector2 pos = target.position;

                        if (0 <= pos.x && 0 <= pos.y)
                        {
                            float x1 = Random.Range(-35f, 15f);
                            float y1 = Random.Range(-25f, -5f);
                            float x2 = Random.Range(-35f, -25f);
                            float y2 = Random.Range(-25f, 25f);
                            float x3 = Random.Range(-35f, 35f);
                            float y3 = Random.Range(-25f, -5f);

                            Vector2 v1 = new Vector2(x1, y1);
                            Vector2 v2 = new Vector2(x2, y2);
                            Vector2 v3 = new Vector2(x3, y3);

                            Vector2[] V = new Vector2[3] { v1, v2, v3 };

                            rand_pos = V[Random.Range(0, 2)];
                        }
                        else if (pos.x < 0 && 0 < pos.y)
                        {
                            float x1 = Random.Range(15f, 35f);
                            float y1 = Random.Range(-25f, -5f);
                            float x2 = Random.Range(25f, 35f);
                            float y2 = Random.Range(-25f, 25f);
                            float x3 = Random.Range(-35f, 35f);
                            float y3 = Random.Range(-25f, -5f);

                            Vector2 v1 = new Vector2(x1, y1);
                            Vector2 v2 = new Vector2(x2, y2);
                            Vector2 v3 = new Vector2(x3, y3);

                            Vector2[] V = new Vector2[3] { v1, v2, v3 };

                            rand_pos = V[Random.Range(0, 2)];

                        }
                        else if (pos.x < 0 && pos.y < 0)
                        {
                            float x1 = Random.Range(15f, 35f);
                            float y1 = Random.Range(5f, 25f);
                            float x2 = Random.Range(25f, 35f);
                            float y2 = Random.Range(-25f, 25f);
                            float x3 = Random.Range(-35f, 35f);
                            float y3 = Random.Range(5f, 25f);

                            Vector2 v1 = new Vector2(x1, y1);
                            Vector2 v2 = new Vector2(x2, y2);
                            Vector2 v3 = new Vector2(x3, y3);

                            Vector2[] V = new Vector2[3] { v1, v2, v3 };

                            rand_pos = V[Random.Range(0, 2)];
                        }
                        else if (0 < pos.x && pos.y < 0)
                        {
                            float x1 = Random.Range(-35f, -15f);
                            float y1 = Random.Range(5f, 25f);
                            float x2 = Random.Range(-35f, -25f);
                            float y2 = Random.Range(-25f, 25f);
                            float x3 = Random.Range(-35f, 35f);
                            float y3 = Random.Range(5f, 25f);

                            Vector2 v1 = new Vector2(x1, y1);
                            Vector2 v2 = new Vector2(x2, y2);
                            Vector2 v3 = new Vector2(x3, y3);

                            Vector2[] V = new Vector2[3] { v1, v2, v3 };

                            rand_pos = V[Random.Range(0, 2)];
                        }

                        ram_p = false;
                    }

                    agent.speed = speed * 0.6f;
                    agent.SetDestination(rand_pos);
                }
                
            }
        }
        else
        {
            agent.velocity = Vector2.zero;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (power_p)
            {
                StartCoroutine(Management.instance.Pause(1.0f));

                gameObject.layer = LayerMask.NameToLayer("Return");
                pause = true;
            }
            

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "UpperRoad" || collision.gameObject.name == "LowerRoad")
        {
            w = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "UpperRoad" || collision.gameObject.name == "LowerRoad")
        {
            w = false;
        }
    }




}
