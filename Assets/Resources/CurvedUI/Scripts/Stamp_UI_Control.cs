using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp_UI_Control : MonoBehaviour
{
    public GameObject ship_c;
    public GameObject droptower_c;
    public GameObject wheel_c;
    public GameObject circustent_c;
    public GameObject castle_c;
    public GameObject carousel_c;

    public GameObject ship_wb;
    public GameObject droptower_wb;
    public GameObject wheel_wb;
    public GameObject circustent_wb;
    public GameObject castle_wb;
    public GameObject carousel_wb;

    public GameObject minimap;
    public GameObject status;
    public GameObject diary;
    public GameObject end_sense;

    public OVRGrabbable ship_reward;
    public OVRGrabbable droptower_reward;
    public OVRGrabbable wheel_reward;
    public OVRGrabbable circustent_reward;
    public OVRGrabbable castle_reward;
    public OVRGrabbable carousel_reward;

    private int ship_active;
    private int droptower_active;
    private int wheel_active; 
    private int circustent_active;
    private int castle_active;
    private int carousel_active;
    // deter whether the diary is showing
    // (in game begin & end when player can't open UI)
    [HideInInspector]
    public bool Is_Reading_Diary;
    // Start is called before the first frame update
    void Start()
    {
        ship_active=0;
        droptower_active=0;
        wheel_active=0;
        circustent_active=0;
        castle_active=0;
        carousel_active=0;

        Is_Reading_Diary = true;
        Invoke("Diary_end", 15.0f);
}

    // Update is called once per frame
    void Update()
    {
        if (DetermineFacilityGame(0))
        {
            droptower_c.SetActive(true);
            droptower_wb.SetActive(false);
            droptower_active = 1;
        }
        if (DetermineFacilityGame(1))
        {
            ship_c.SetActive(true);
            ship_wb.SetActive(false);
            ship_active = 1;
        }
        if (DeterminePoseGamePass(0))
        {
            wheel_c.SetActive(true);
            wheel_wb.SetActive(false);
            wheel_active = 1;
        }
        if (true)
        {
            circustent_c.SetActive(true);
            circustent_wb.SetActive(false);
            circustent_active = 1;
        }
        if (DeterminePoseGamePass(1))
        {
            castle_c.SetActive(true);
            castle_wb.SetActive(false);
            castle_active = 1;
        }
        if (DeterminePoseGamePass(2))
        {
            carousel_c.SetActive(true);
            carousel_wb.SetActive(false);
            carousel_active = 1;
        }

        if (wheel_active ==1 && ship_active ==1 && droptower_active == 1 && circustent_active == 1 && castle_active == 1 && carousel_active == 1)
        {
            Invoke("EndGame", 3.0f);
        }
    }
    // (0 : droptower , 1 : gondora)
    bool DetermineFacilityGame(int ID)
    {
        if (ID == 0)
        {
            DropTowerControl game = FindObjectOfType<DropTowerControl>();
            if (game.end == 1)
                return true;
        }
        else if (ID == 1)
        {
            Gondora game = FindObjectOfType<Gondora>();
            if (game.end == 1)
                return true;
        }
        return false;
    }

    // input pose game ID & determine whether the game is pass
    // ( 0 : wheel , 1 : Castle , 2 : Carousel)
    bool DeterminePoseGamePass(int ID)
    {
        PoseGameManager[] GameList = FindObjectsOfType<PoseGameManager>();
        for(int i = 0; i < GameList.Length; i++)
        {
            if(GameList[i].PoseGameID == ID)
            {
                return GameList[i].is_complete;
            }
        }
        return false;
    }

    void EndGame()
    {
        minimap.SetActive(false);
        status.SetActive(false);
        end_sense.SetActive(true);
        Is_Reading_Diary = true;
    }

    void Diary_end()
    {
        diary.SetActive(false);
        Is_Reading_Diary = false;
    }
    
    public void OpenUI()
    {
        minimap.SetActive(true);
        status.SetActive(true);
    }

    public void CloseUI()
    {
        minimap.SetActive(false);
        status.SetActive(false);
    }
}
