using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResiNPCFinish : MonoBehaviour
{
    public GameObject Cat1;
    public GameObject Cat2;
    public GameObject Cat3;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("ResidentialS", 1);
        if(PlayerPrefs.GetInt("ResidentialS") ==1 )
        {
            Cat1.SetActive(false);
            Cat2.SetActive(true);
            Cat3.SetActive(true);
        }
    }

}
