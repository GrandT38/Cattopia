using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string sceneToLoad;
    public CheckpointController CkPoint;
    public GameObject player;
    [SerializeField] private int Point = 0;
    public Text PointTxt;
    public int MissionNeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CkPoint = FindObjectOfType<CheckpointController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint(int PointToAdd)
    {
        Point += PointToAdd;
        PointTxt.text = "Point : " + Point;
        if(Point >= MissionNeed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void RespawnPlayer()
    {
        //CkPoint.transform.position = CkPoint.respawnPosition;
        //player.transform.position = CkPoint.respawnPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
