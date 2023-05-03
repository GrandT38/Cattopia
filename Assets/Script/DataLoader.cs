using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public float PlayerX;
    public float PlayerY;
    public float PlayerZ;
    public bool Saved = false;
    public float TimerOfWorld = 12;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
