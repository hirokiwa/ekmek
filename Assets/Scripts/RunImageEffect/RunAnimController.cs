using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimController : MonoBehaviour
{
    [SerializeField] private Animator feverAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        feverAnim.SetBool("isFever", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
