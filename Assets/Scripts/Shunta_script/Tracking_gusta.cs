using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tracking_gusta : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform target;
    public Transform start_point;
    public static Tracking_gusta instance;
    public bool pause;
    public bool ram_g;
    public bool power_g;

    private Vector2 rand_pos;
    private float speed = 0.0f; //初期速度
    private float distance;
    private bool w;

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    SpriteRenderer sr;
    public bool flag_g = false;

    public Transform Point_ur;
    public Transform Point_ul;
    public Transform Point_dr;
    public Transform Point_dl;

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
        ram_g = true;
        power_g = false;
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

                if (power_g == false)
                {
                    if (flag_g)
                    {
                        sr.sprite = s1;
                        flag_g = false;
                    }

                    if (!w) agent.speed = speed;
                    else agent.speed = speed * 0.5f;

                    ram_g = true;
                    Vector2 pos = target.position;

                    if (Management.instance.pub_ntimer < 8)
                    {
                        Vector2 n_pos = nawabari.position;
                        agent.SetDestination(n_pos);
                    }
                    else
                    {
                        distance = Vector3.Distance(transform.position, target.transform.position);

                        if (distance < 15)
                        {
                            if (0 <= pos.x && 0 <= pos.y)
                            {
                                Vector2 new_pos = Point_ur.position;
                                agent.SetDestination(new_pos);
                            }
                            else if (pos.x < 0 && 0 < pos.y)
                            {
                                Vector2 new_pos = Point_ul.position;
                                agent.SetDestination(new_pos);
                            }
                            else if (pos.x < 0 && pos.y < 0)
                            {
                                Vector2 new_pos = Point_dr.position;
                                agent.SetDestination(new_pos);
                            }
                            else if (0 < pos.x && pos.y < 0)
                            {
                                Vector2 new_pos = Point_dl.position;
                                agent.SetDestination(new_pos);
                            }
                        }
                        else
                        {
                            agent.SetDestination(pos);
                        }
                    }

                }
                else
                {

                    if (flag_g)
                    {
                        sr.sprite = s2;
                        flag_g = false;
                    }

                    if (Management.instance.pub_timer > (Management.instance.powerTime - 3))
                    {
                        sr.sprite = s3;
                        flag_g = false;
                    }

                    if (ram_g)
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

                        ram_g = false;
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
            if (power_g)
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
