using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
 
public class Move : NetworkBehaviour 
{
    [SerializeField]
    private float speed;
    public Camera cma;
    private AudioListener ad;
    Rigidbody rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        ad = this.transform.Find("Camera").GetComponent<AudioListener>();
        Debug.Log("Is local player? : " + this.isLocalPlayer);
        if(!this.isLocalPlayer)
        {
            Debug.Log("Not Your camera");
            cma.enabled = false;
            ad.enabled = false;
        }
    }
    void FixedUpdate () 
    {
        // Debug.Log("Move flag : " + this.isLocalPlayer);
        if(this.isLocalPlayer) 
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 moveBy = transform.right * x + transform.forward * z;
            rb.MovePosition(transform.position + moveBy.normalized * speed * Time.deltaTime);
            if(Input.GetKeyDown(KeyCode.Q)){
                GameObject target = GameObject.Find("T");
                Debug.Log("Touch Q");
                if(target != null)
                    GetComponent<Player>().CmdAttach(target,new Vector3(3,0,0));
            }
            if(Input.GetKeyDown(KeyCode.Z)){
                GetComponent<Player>().CmdDetach(new Vector3(0,0,0),true);
            }
        }
    }
}