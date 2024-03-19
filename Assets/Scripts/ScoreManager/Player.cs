using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

    private List<Joycon>    m_joycons;
    private Joycon          m_joyconL;
    private Joycon          m_joyconR;
    private Joycon.Button?  m_pressedButtonL;
    private Joycon.Button?  m_pressedButtonR;

    private JoyconManager joycon;
    private Vector3 accel;
    public float speed = 0.1f;
    [SerializeField] Vector2 magnificationVec;
    public static Player instance;
    public int v;

    public NavMeshAgent enemy1;
    public NavMeshAgent enemy2;
    public NavMeshAgent enemy3;
    public GameObject me;

    private Vector2 w_pos;
    [SerializeField] private Animator anim;

    private int remainCount;
    [SerializeField] private Text RemainingNumber;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject UICanvas;
    
    Rigidbody2D rigidbody2D;
    private Vector2 directionVector;
    
    public Tilemap food_tilemap;

    [SerializeField] public bool inputOptionIsSwitchController_CheckBox;
    public static bool inputOptionIsSwitchController = true;

    public static float speedBoost;
    // 30秒で0.5を減少させたいので、1秒あたりの減少量は0.5 / 30
    float decreaseAmountPerSecond = 0.5f / 30f;
    
    // public AudioClip howasound;
    // AudioSource audioSource;

    // Whether the game stage is in play
    public bool isGameRunning;

    private int RotateDigree;

    public void Awake()
    {
        inputOptionIsSwitchController = inputOptionIsSwitchController_CheckBox;
        if (instance == null)
        {
            instance = this;
        }
        
        remainCount = 2;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        var m_joycons = JoyconManager.Instance.j;
        if ( m_joycons == null || m_joycons.Count <= 0 ) return;

        m_joyconL = m_joycons.Find( c =>  c.isLeft );
        m_joyconR = m_joycons.Find( c => !c.isLeft );

    }

    void Update()
    {
        if (!Management.instance.stop)
        {
            if (inputOptionIsSwitchController)
            {
                //Joy-Con用
                if (m_joyconR.GetButtonDown(m_buttons[2]))
                {
                    // Yボタン（x軸負の方向）
                    directionVector = new Vector2(-1f, 0);
            
                    v = 0;
                    handleGoLeft();
                }
                else if (m_joyconR.GetButtonDown(m_buttons[1]))
                {
                    // Aボタン（x軸正の方向）
                    directionVector = new Vector2(1f, 0);
            
                    v = 1;
                    handleGoRight();
                }
                else if (m_joyconR.GetButtonDown(m_buttons[3]))
                {
                    // Xボタン（y軸正の方向）
                    directionVector = new Vector2(0, 1f);
            
                    v = 2;
                    handleGoUp();
                }
                else if (m_joyconR.GetButtonDown(m_buttons[0]))
                {
                    // Bボタン（y軸負の方向）
                    directionVector = new Vector2(0, -1f);
            
                    v = 3;
                    handleGoDown();
                }else
                {
                    anim.SetBool("isRun", true);
                }
            
                rigidbody2D.velocity = new Vector2(AccelerationFilter.instance.last_accel_value, AccelerationFilter.instance.last_accel_value)
                                       * directionVector * magnificationVec * speedBoost;
                
                if (speedBoost > 1.0f)
                {
                    // 30秒での減少量を計算
                    float decreaseAmountPerSecond = (speedBoost - 1.0f) / 30f;
        
                    // フレームごとの減少量を計算
                    speedBoost -= decreaseAmountPerSecond * Time.deltaTime;
        
                    // speedBoostが1.0未満にならないようにする
                    if (speedBoost < 1.0f)
                    {
                        speedBoost = 1.0f;
                    }
                }
            }
            else
            {
                Vector2 position = transform.position;
                
                if (Input.GetKey("left"))
                {
                    position.x -= speed;
                    anim.SetBool("isRun", true);
                
                    v = 0;
                    handleGoLeft();
                }
                else if (Input.GetKey("right"))
                {
                    position.x += speed;
                    anim.SetBool("isRun", true);
                
                    v = 1;
                    handleGoRight();
                }
                else if (Input.GetKey("up"))
                {
                    position.y += speed;
                    anim.SetBool("isRun", true);
                
                    v = 2;
                    handleGoUp();
                }
                else if (Input.GetKey("down"))
                {
                    position.y -= speed;
                    anim.SetBool("isRun", true);
                
                    v = 3;
                    handleGoDown();
                }
                else
                {
                    anim.SetBool("isRun", false);
                }
                
                transform.position = position;
            }

        } 

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!Management.instance.power)
            {
                // ここに残り残機数を減らす処理を発火させる
                remainCount -= 1; 
                RemainingNumber.text = "✖️ " + remainCount.ToString();
                
                StartCoroutine(Management.instance.Restart(enemy1, enemy2, enemy3, me));

                // GameOverUIの表示もここで行う
                if (remainCount < 0)
                {
                    GameOver();
                }
            }
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        
        ScoreCountSystem.instance.ScoreReset();
        remainCount = 2;
        RemainingNumber.text = "✖️ " + remainCount.ToString();
        
        setIsGameRunning(false);
        
        SceneManager.LoadScene("Main");
    }

    public void setIsGameRunning(bool input) {
        if(!isGameRunning & input){
            DistanceManager.instance.resetDistanceM();
        }
        isGameRunning = input;
    }

    public void handleGoRight(){
        changeRotateDigree(0);
    }
    public void handleGoLeft(){
        changeRotateDigree(180);
    }
    public void handleGoUp(){
        changeRotateDigree(90);
    }
    public void handleGoDown(){
        changeRotateDigree(270);
    }

    private void changeRotateDigree(int NewDigree){
        int CurrentRotateDigree = getRotateDigree();
        int DifferenceZ = NewDigree - CurrentRotateDigree;
        bool turnPacman = turnLeftFromUpOrDown(CurrentRotateDigree, NewDigree);
        int DifferenceX = getDifferenceX(CurrentRotateDigree, NewDigree);
        transform.Rotate(DifferenceX, 0, turnPacman ? DifferenceZ + 180 : DifferenceZ);
        setRotateDigree(NewDigree);
    }

    private bool turnLeftFromUpOrDown(int CurrentDigreeZ, int NewDigreeZ)
    {
        bool IsCurrentUpOrDown = CurrentDigreeZ == 90 | CurrentDigreeZ == 270;
        bool IsNextLeft = NewDigreeZ == 180;
        return IsCurrentUpOrDown & IsNextLeft;
    }

    private int getDifferenceX(int CurrentDigreeZ, int NewDigreeZ)
    {
        bool IsCurrentLeft = CurrentDigreeZ == 180;
        bool IsNextLeft = NewDigreeZ == 180;
        bool TurnX = IsCurrentLeft ^ IsNextLeft;
        return  TurnX ? 180 : 0;
    }

    public void resetDirection() {
        changeRotateDigree(0);
    }

    private int getRotateDigree(){
        return RotateDigree;
    }
    private void setRotateDigree(int input){
        RotateDigree = input;
    }

}
