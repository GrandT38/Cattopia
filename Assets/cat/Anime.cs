using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Anime : MonoBehaviour
{

    public GameObject SS;
    Animator SSAnimator;


    // Start is called before the first frame update
    void Start()
    {
        SSAnimator = SS.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.I))
        {

        }
    }



    public void isIdle()
    {
        SSAnimator.SetBool("isIdle", true);
        SSAnimator.SetBool("isRun", false);
        SSAnimator.SetBool("isWalk", false);
    }
    public void isRun()
    {
        SSAnimator.SetBool("isIdle", false);
        SSAnimator.SetBool("isRun", true);
        SSAnimator.SetBool("isWalk", false);
    }
    public void isWalk()
    {
        SSAnimator.SetBool("isIdle", false);
        SSAnimator.SetBool("isRun", true);
        SSAnimator.SetBool("isWalk", true);
    }

}
