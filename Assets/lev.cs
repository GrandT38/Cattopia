using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lev : MonoBehaviour
{
    public Lightmanage LM;
    public float Time1;
    // Start is called before the first frame update
    void Start()
    {
        LM = FindObjectOfType<Lightmanage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (LM.TimeOfDay >= 6 && LM.TimeOfDay <= 18)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene(1);
        }


    }
}
