using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTouch : MonoBehaviour
{
    private PlayerMovement PM;
    public  Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        PM = FindObjectOfType<PlayerMovement>();
        ani = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && PM.StartTouch)
        {
            ani.SetBool("isIdle", false );
            ani.SetBool("CatTouch", true);
            StartCoroutine(Idle());
        }
    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(4);
        PM.StartTouch = false;
        ani.SetBool("CatTouch", false);
        ani.SetBool("isIdle", true);
    }
}
