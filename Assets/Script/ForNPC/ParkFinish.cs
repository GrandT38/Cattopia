using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkFinish : MonoBehaviour
{
    public GameObject pp1;
    public GameObject pp2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("parkS") == 1)
        {
            pp1.SetActive(false);
            pp2.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
