using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string sceneToLoad;
    //public CheckpointController CkPoint;
    public GameObject player;
    [SerializeField] public int Point = 0;
    public Text PointTxt;
    //public Text StealthTxt;
    public int MissionNeed;
    [SerializeField]
    public bool playerExposed = false;

    public GameObject stealthSystem;
    public Sprite warning;
    public bool warning1 = false;
    public Sprite exposed;
    public Sprite stealth;

    public PlayerMovement playerMovement;
    //public int PlayerSchedule;
    private Lightmanage LM;

    public GameObject WarningPanel;
    public ExitCollider exitCollider;
    public PlayerLead playerLead;
    public Text Timetxt;

    public LoadLevel LoadLevel;
    private DataLoader dataLoader;

    public bool PlayerHide = false;

    public GameObject HideBoxWall;
    public WorldHints worldHint;

    public GameObject pausePanel;
    public GameObject roadblock;
    public AudioSource collected;

    void Start()
    {
        HideBoxWall = GameObject.Find("HideBoxWall");
        dataLoader = FindObjectOfType<DataLoader>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        stealthSystem = GameObject.Find("Stealth");
        player = GameObject.FindGameObjectWithTag("Player");
        LM = FindObjectOfType<Lightmanage>();
        //CkPoint = FindObjectOfType<CheckpointController>();
        //PointTxt.text = Point + "/" + MissionNeed;
        Time.timeScale = 1;
        //PlayerPrefs.SetInt("schedule", 0);
        if (HideBoxWall != null)
        {
            HideBoxWall.SetActive(false);
        }

        if (PointTxt != null)
        {
            PointTxt.text = Point + "/" + MissionNeed;
        }

        //ExitCollider
        if (PlayerPrefs.GetInt("parkS") == 1 && PlayerPrefs.GetInt("MarketS") >= 1 && PlayerPrefs.GetInt("ClinicS") == 1 && PlayerPrefs.GetInt("ResidentialS") == 1  )
        {
            if (playerLead != null)
                playerLead.gameProgress = 4;
            Debug.Log("Cattopia");
            roadblock.SetActive(false);
            //cattopia
        }
        else if (PlayerPrefs.GetInt("parkS") == 1 && PlayerPrefs.GetInt("ClinicS") == 1 && PlayerPrefs.GetInt("ResidentialS") == 1)
        {
            if (playerLead != null)
                playerLead.gameProgress = 3;
            //chaoshi 
        }
        else if (PlayerPrefs.GetInt("parkS") == 1 && PlayerPrefs.GetInt("ResidentialS") == 1)
        {
            if (playerLead != null)
                playerLead.gameProgress = 2;
            //zhensuo
        }
        else if (PlayerPrefs.GetInt("parkS") == 1 )
        {
            if (playerLead != null)
                playerLead.gameProgress = 1;
            //ju mingqu
        }
        else
        {
            if (playerLead != null)
                playerLead.gameProgress = 0;
            //park
            Debug.Log("Not now");
        }

    }


    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();
        /*
        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("PlayerX", -63);
        PlayerPrefs.SetInt("PlayerZ", 1);
        PlayerPrefs.SetInt("schedule", 0);
        PlayerPrefs.SetInt("Mission1", 0);
        */
    }

    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetPlayerData();
        }*/
        if(LM != null )
        Timetxt.text = (int)LM.TimeOfDay + ":00";
        //


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else if (!pausePanel.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Confined;
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;

            }
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        LoadLevel.loadScene("MainMenu");
    }

    public void PlayButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //level 0
    public void AddPoint(int PointToAdd)
    {
        if(collected != null)
        {
            collected.Play();
        }

        Point += PointToAdd;
        PointTxt.text = Point + "/" + MissionNeed;
        if (Point >= MissionNeed)
        {
            //SaveData();
            exitCollider.ExitShow();
            playerLead.ShowLeader();
            //SceneManager.LoadScene("SampleScene");
            //PlayerPrefs.SetInt("schedule", 1);
            //PlayerSchedule = PlayerPrefs.GetInt("schedule");

        }
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PlayerStealth()
    {
        //StealthTxt.text = "[Stealth]";
        //StealthTxt.color = Color.green;
        stealthSystem.GetComponent<UnityEngine.UI.Image>().sprite = stealth;
    }

    public void EnemyVigilant()
    {
        //StealthTxt.color = new Color(255 / 255f, 140 / 255f, 0 / 255f, 255 / 255f);
        //StealthTxt.text = "[Vigilant]";
        stealthSystem.GetComponent<UnityEngine.UI.Image>().sprite = warning;
        warning1 = true;

    }

    public void PlayerExposed()
    {
        //StealthTxt.text = "[Exposed]";
        //StealthTxt.color = Color.red;
        stealthSystem.GetComponent<UnityEngine.UI.Image>().sprite = exposed;
        playerExposed = true;
        if (HideBoxWall != null)
        {
            HideBoxWall.SetActive(true);
        }
    }

    public void LoadParkScene()
    {
        //PlayerPrefs.SetInt("PlayerX", (int)player.transform.position.x);
        //PlayerPrefs.SetInt("PlayerZ", (int)player.transform.position.z);
        dataLoader.TimerOfWorld = LM.TimeOfDay;
        PlayerPrefs.SetInt("PlayerX", -64 );
        PlayerPrefs.SetFloat("PlayerY", 0.19f);
        PlayerPrefs.SetInt("PlayerZ", 21);
        dataLoader.Saved = true;
        dataLoader.PlayerX = player.transform.position.x;
        dataLoader.PlayerY = player.transform.position.y;
        dataLoader.PlayerZ = player.transform.position.z;
        LoadLevel.loadScene("ParkScene");
    }

    public void LoadPark2()
    {
        //PlayerPrefs.SetInt("PlayerX", (int)player.transform.position.x);
        //PlayerPrefs.SetInt("PlayerZ", (int)player.transform.position.z);
        dataLoader.TimerOfWorld = LM.TimeOfDay;
        PlayerPrefs.SetInt("PlayerX", -64);
        PlayerPrefs.SetFloat("PlayerY", 0.19f);
        PlayerPrefs.SetInt("PlayerZ", 21);
        dataLoader.Saved = true;
        dataLoader.PlayerX = player.transform.position.x;
        dataLoader.PlayerY = player.transform.position.y;
        dataLoader.PlayerZ = player.transform.position.z;
        LoadLevel.loadScene("ParkScene2");
    }

    public void LoadMarketScene()
    {
        dataLoader.TimerOfWorld = LM.TimeOfDay;
        PlayerPrefs.SetInt("PlayerX", 32);
        PlayerPrefs.SetFloat("PlayerY", 0.09f);
        PlayerPrefs.SetInt("PlayerZ", 78);
        dataLoader.Saved = true;
        dataLoader.PlayerX = player.transform.position.x;
        dataLoader.PlayerY = player.transform.position.y;
        dataLoader.PlayerZ = player.transform.position.z;

        if (LM.TimeOfDay >= 6 && LM.TimeOfDay <= 18)
        {
            LoadLevel.loadScene("MarketScene");
            //SceneManager.LoadScene("MarketScene");
        }
        else if (LM.TimeOfDay <= 6 || LM.TimeOfDay >= 18 )
        {
            if (PlayerPrefs.GetInt("Mission1") != 1) // if have not the mission1
            {
                LoadLevel.loadScene("MarketSceneNight2");
            }
            else
            {
                LoadLevel.loadScene("MarketSceneNight");
            }

        }

    }

    public void LoadClinicScene()
    {
        dataLoader.TimerOfWorld = LM.TimeOfDay;
        PlayerPrefs.SetInt("PlayerX", -56);
        PlayerPrefs.SetFloat("PlayerY", 0.09f);
        PlayerPrefs.SetInt("PlayerZ", 118);
        dataLoader.Saved = true;
        dataLoader.PlayerX = player.transform.position.x;
        dataLoader.PlayerY = player.transform.position.y;
        dataLoader.PlayerZ = player.transform.position.z;


        if (LM.TimeOfDay >= 6 && LM.TimeOfDay <= 18)
        {

            //SceneManager.LoadScene("MarketScene");
            //if (PlayerPrefs.GetInt("Mission2") != 1) // if have not the mission1;  [DialogSystem.cs]
            //{
                worldHint.warningPlayer();
                //WarningPanel.SetActive(true);
                //playerMovement.StartTouch = true;       //can make player dont move
                //StartCoroutine(WarningPlayer());
            //}



        }
        else if (LM.TimeOfDay <= 6 || LM.TimeOfDay >= 18)
        {
            if (PlayerPrefs.GetInt("Mission2") != 1)
            {
                worldHint.warningPlayer();
            }
            else
            {
                LoadLevel.loadScene("ClinicNight");
            }
        }
    }

    public void LoadResidentialScene()
    {
        dataLoader.TimerOfWorld = LM.TimeOfDay;
        PlayerPrefs.SetInt("PlayerX", -38);
        PlayerPrefs.SetFloat("PlayerY", 0f);
        PlayerPrefs.SetInt("PlayerZ", -128);
        dataLoader.Saved = true;
        dataLoader.PlayerX = player.transform.position.x;
        dataLoader.PlayerY = player.transform.position.y;
        dataLoader.PlayerZ = player.transform.position.z;

        LoadLevel.loadScene("Residential Scene Night");
    }

    public void LoadCattopia()
    {
        if (playerLead.gameProgress == 4)
        {
            ResetPlayerData();
            dataLoader.Saved = false;
            dataLoader.PlayerX = 0;
            dataLoader.PlayerY = 0;
            dataLoader.PlayerZ = 0;
            LoadLevel.loadScene("EndScene");
        }
    }


    public bool IsSaved()
    {
        /*
        int coinCount = PlayerPrefs.GetInt("CoinCount");
        int playerX = PlayerPrefs.GetInt("PlayerX");
        int playerZ = PlayerPrefs.GetInt("PlayerZ");
        int schedule = PlayerPrefs.GetInt("schedule");
        int mission1 = PlayerPrefs.GetInt("Mission1");
        */
        bool isSaved = PlayerPrefs.GetInt("CoinCount") != 0 || PlayerPrefs.GetInt("PlayerX") != -63 || 
            PlayerPrefs.GetInt("PlayerZ") != 1 || PlayerPrefs.GetInt("schedule") != 0 || PlayerPrefs.GetInt("Mission1") != 0;

        return isSaved;
    }


    public void LoadToWorld()
    {
        LoadLevel.loadScene("SampleScene");
    }

    IEnumerator WarningPlayer()
    {
        yield return new WaitForSeconds(3f);
        playerMovement.StartTouch = false;
        WarningPanel.SetActive(false);
    }
}
