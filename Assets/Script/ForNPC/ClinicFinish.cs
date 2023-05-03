using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClinicFinish : MonoBehaviour
{
    public GameObject Cat1;
    public GameObject Cat2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ClinicS") == 1)
        {
            Cat1.SetActive(false);
            Cat2.SetActive(true);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
