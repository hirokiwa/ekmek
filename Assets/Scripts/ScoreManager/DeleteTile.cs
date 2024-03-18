using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeleteTile : MonoBehaviour
{
    public AudioClip PakuSound;
    // public AudioClip BakuSound;
    public AudioClip PiyoSound;
    AudioSource audioSource1;
    // [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;

    [SerializeField] private ScoreCountSystem scoreSystem;

    [SerializeField] public GameObject RunFever;
    [SerializeField] private Animator feverAnim;

    public static DeleteTile instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D ot)
    {
        // Check if the collided object has the tag "AAA"
        if (ot.gameObject.tag == "Esa") // Add this line
        {
            audioSource1.PlayOneShot(PakuSound);
            
            var hitPos = Vector3.zero;

            foreach (var point in ot.contacts)
            {
                hitPos = point.point;
            }

            var position = ot.gameObject.GetComponent<Tilemap>().cellBounds.allPositionsWithin;
            var minPosition = 0;
            var allPosition = new List<Vector3>();

            foreach (var variable in position)
            {
                if (ot.gameObject.GetComponent<Tilemap>().GetTile(variable) != null)
                {
                    allPosition.Add(variable);
                }
            }

            for (var i = 1; i < allPosition.Count; i++)
            {
                if ((hitPos - allPosition[i]).magnitude <
                    (hitPos - allPosition[minPosition]).magnitude)
                {
                    minPosition = i;
                }
            }

            var finalPosition = Vector3Int.RoundToInt(allPosition[minPosition]);

            var tiletmp = ot.gameObject.GetComponent<Tilemap>().GetTile(finalPosition);

            if (tiletmp != null)
            {
                var map = ot.gameObject.GetComponent<Tilemap>();
                var tileCol = ot.gameObject.GetComponent<TilemapCollider2D>();
                map.SetTile(finalPosition, null);
                tileCol.enabled = false;
                tileCol.enabled = true;

                // スコアを更新する処理を追加x`
                if(scoreSystem != null)
                {
                    scoreSystem.AddScore(100); 
                }

            }
            
            //エサのカウントを上げる
            InClearChecker.instance.eatenFoodCount++;
            InClearChecker.instance.CheckForWin();
        }
        else if (ot.gameObject.tag == "PowerEsa")
        {
            audioSource3.PlayOneShot(PiyoSound);
            
            RunFever.SetActive(true);
            isFeverAnimOn();

            Management.instance.power = true;
            Tracking_oikake.instance.power_o = true;
            Tracking_pinky.instance.power_p = true;
            Tracking_gusta.instance.power_g = true;

            Tracking_oikake.instance.ram_o = true;
            Tracking_pinky.instance.ram_p = true;
            Tracking_gusta.instance.ram_g = true;

            Tracking_oikake.instance.flag_o = true;
            Tracking_pinky.instance.flag_p = true;
            Tracking_gusta.instance.flag_g = true;

            Management.instance.powerTime += 10.0f;

            Destroy(ot.gameObject);

        }
    }

    public void isFeverAnimOn()
    {
        feverAnim.SetBool("isFever", true);
    }
    
    public void isFeverAnimOff()
    {
        feverAnim.SetBool("isFever", false);
    }
}