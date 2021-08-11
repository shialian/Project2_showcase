using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Gondora : NetworkBehaviour
{
    public GameObject m_anchor;        //圆点
    public float g = 9.8f;            //重力加速度
    private Vector3 m_rotateAxis;    //旋转轴
    private float w = 0;                //角速度


    public GameObject ship;
    private int goleft = 0;
    public OVRGrabbable activedevice;
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
    public AudioSource audio;
    public AudioClip clip;
    private bool played;
    public OVRGrabbable reward;

    //public bool goldenFinger;

    [SyncVar]
    public int end = 0;
    // Use this for initialization
    void Start()
    {
        //求出旋转轴
        m_rotateAxis = Vector3.Cross(ship.transform.position - m_anchor.transform.position, Vector3.left);
        active = 0;
        end = 0;
    }

    void DoPhysics()
    {
        float r = Vector3.Distance(m_anchor.transform.position, ship.transform.position);
        float l = Vector3.Distance(new Vector3(m_anchor.transform.position.x, ship.transform.position.y, m_anchor.transform.position.z), ship.transform.position);
        //当钟摆摆动到另外一侧时，l为负，则角加速度alpha为负。
        Vector3 axis = Vector3.Cross(ship.transform.position - m_anchor.transform.position, Vector3.down);
        if (Vector3.Dot(axis, m_rotateAxis) < 0)
        {
            l = -l;
        }
        float cosalpha = l / r;
        //求角加速度
        float alpha = (cosalpha * g) / r;
        //累计角速度
        w += alpha * Time.deltaTime;
        //求角位移(乘以180/PI 是为了将弧度转换为角度)
        float thelta = w * Time.deltaTime * 180.0f / Mathf.PI;
        //绕圆点m_ahchor的旋转轴m_rotateAxis旋转thelta角度
        ship.transform.RotateAround(m_anchor.transform.position, m_rotateAxis, thelta);

    }
    
    void SwingControl()
    {
        if (activedevice.isGrabbed == true)
        {
            int TriggerPlayerID = activedevice.grabbedBy.transform.root.gameObject.GetComponent<Player>().PlayerID;
            GameManager.singleton.SendAnotherPlayer(TriggerPlayerID,Facility,playerAnchor.localPosition);
            active = 1;
        }

        if (reward.isGrabbed == true)
        {
            int TriggerPlayerID = reward.grabbedBy.transform.root.gameObject.GetComponentInChildren<Player>().PlayerID;
            GameManager.singleton.SendPlayerBack(TriggerPlayerID, BackPosition, is_origin);
            PassGame();
            GameEnd();
            //goldenFinger = false;
        }

        if (active == 1)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.2f)
            {
                ship.transform.RotateAround(m_anchor.transform.position, new Vector3(1.0f, 0.0f, 0.0f), -1 * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * Time.deltaTime * 20);
                if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 1.0f)
                {
                    goleft = 1;
                }
            }

            if (goleft == 1 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0.0f)
            {
                ship.transform.RotateAround(m_anchor.transform.position, new Vector3(1.0f, 0.0f, 0.0f), Time.deltaTime * 20);

            }
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        ///DoPhysics();
        SwingControl();
    }

    void GameEnd()
    {
        ship.transform.rotation = Quaternion.identity;
        active = 0;
    }
    public void GoldenFinger()
    {
        Player[] PlayerList = GetComponents<Player>();
        int TriggerPlayerID = -1;
        for (int i = 0; i < PlayerList.Length; i++)
        {
            if (PlayerList[i].gameObject.transform.parent != null)
            {
                TriggerPlayerID = PlayerList[i].PlayerID;
            }
        }
        GameManager.singleton.SendPlayerBack(TriggerPlayerID, BackPosition, is_origin);
        PassGame();
        GameEnd();
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
