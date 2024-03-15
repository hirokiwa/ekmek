using System;
using Unity.VisualScripting;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform upperPosition;
    public Transform lowerPosition;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "UpperPosition")
        {
            transform.position = lowerPosition.position + new Vector3(0,3f,0);
        }
        
        if(other.gameObject.name == "LowerPosition")
        {
            transform.position = upperPosition.position - new Vector3(0,3f,0);
        }
    }
}