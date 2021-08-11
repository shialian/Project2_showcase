using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DropTowerControl : NetworkBehaviour
{
    public GameObject chair;
    public GameObject energybar;
    public GameObject emptybar;
    public GameObject color_DropTower_UI;
    public GameObject BW_DropTower_UI;

    public OVRGrabbable activedevice;
    public OVRGrabbable reward;

    public AudioSource audio;
    public AudioClip clip;
    private bool played = false;

    private Vector3 start_position;
    private Vector3 energybarlength;
    private int release;
    private int toomuchpower;
    public int active;
    // Facility gameobject player will be sent to
    public GameObject Facility;
    // local position base on facility 
    //public Vector3 LocalPosition;
    public Transform playerAnchor;
    // the position send back to when is_origin flag is false
    public Vector3 BackPosition;
    // wheather send the palyer back to origin position
    public bool is_origin;
    private int visible;
    [SyncVar]
    public int end = 0;

    //public bool goldenFinger = false;

    // Start is called before the first frame update
    void Start()
    {
        start_position = new Vector3(chair.transform.position.x, chair.transform.position.y, chair.transform.position.z);
        energybarlength = new Vector3(energybar.transform.localScale.x, energybar.transform.localScale.y, energybar.transform.localScale.z);
        release = 0;
        active = 0;
        end = 0;
        toomuchpower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activedevice.isGrabbed == true)
        {
            int TriggerPlayerID = activedevice.grabbedBy.transform.root.gameObject.GetComponent<Player>().PlayerID;
            GameManager.singleton.SendAnotherPlayer(TriggerPlayerID, Facility, playerAnchor.localPosition);
            active = 1;
            chair.transform.position = start_position;
            energybar.transform.localScale = energybarlength;
        }

        if (reward.isGrabbed == true)
        {
            int TriggerPlayerID = reward.grabbedBy.transform.root.gameObject.GetComponentInChildren<Player>().PlayerID;
            GameManager.singleton.SendPlayerBack(TriggerPlayerID, BackPosition, is_origin);
            PassGame();
            if (color_DropTower_UI != null)
                color_DropTower_UI.SetActive(true);
            if (BW_DropTower_UI != null)
                BW_DropTower_UI.SetActive(false);
            GameEnd();
            //goldenFinger = false;
        }

        if (active == 1)
        {
            if (energybar.transform.localScale.y > 2 * emptybar.transform.localScale.y)
            {
                energybar.transform.localScale = energybarlength;
                chair.transform.Translate(Vector3.down * Time.deltaTime * 10.0f, Space.World);
                toomuchpower = 1;
            }
            if (OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.2f && chair.transform.position.y < 15.0f && toomuchpower == 0)
            {
                chair.transform.Translate(2 * Vector3.up * Time.deltaTime, Space.World);
                energybar.transform.localScale += new Vector3(0, 0.009f, 0);
                release = 0;

                energybar.SetActive(true);
                emptybar.SetActive(true);
            }

            else if (OVRInput.GetUp(OVRInput.Button.Two))
            {
                release = 1;
                toomuchpower = 0;
            }

            if (release == 1 && energybar.transform.localScale.y > 0.0f)
            {
                energybar.transform.localScale -= new Vector3(0, 0.003f, 0);
                if (energybar.transform.localScale.y <= 0)
                {
                    energybar.transform.localScale = new Vector3(energybar.transform.localScale.x, 0, energybar.transform.localScale.z);
                }
            }
            if ((release == 1 || toomuchpower == 1) && chair.transform.position.y > start_position.y)
            {
                chair.transform.Translate(Vector3.down * Time.deltaTime * 4.0f, Space.World);
            }

            void OnTriggerEnter(Collider aaa)
            {
                if (aaa.gameObject.tag == "reward")
                {
                    Destroy(aaa.gameObject);
                }
            }
        }

    }

    public void GoldenFinger()
    {
        Player[] PlayerList = GetComponents<Player>();
        int TriggerPlayerID = -1;
        for (int i = 0 ; i < PlayerList.Length ; i++)
        {
            if(PlayerList[i].gameObject.transform.parent != null)
            {
                TriggerPlayerID = PlayerList[i].PlayerID;
            }
        }
        GameManager.singleton.SendPlayerBack(TriggerPlayerID, BackPosition, is_origin);
        PassGame();
        if (color_DropTower_UI != null)
            color_DropTower_UI.SetActive(true);
        if (BW_DropTower_UI != null)
            BW_DropTower_UI.SetActive(false);
        GameEnd();
    }

    void GameEnd()
    {
        active = 0;
    }
    [Command(requiresAuthority = false)]
    public void PassGame()
    {
        
        end = 1;
        PlaySound();
        return;
    }
    [ClientRpc]
    public void PlaySound()
    {
        if (played == false)
        {
            audio.PlayOneShot(clip);
            played = true;
        }
    }
}
