using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerFunction : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        summon();
    }
    public void summon(){
        if(this.isLocalPlayer && Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Activate from player , Auth : " + hasAuthority);
            create_item();
            // this.create_item();
        }
    }

    [Command]
    void create_item(){
        GameObject item;
        Debug.Log("Summon item!");
        item = MyNetworkManager.NM.spawnPrefabs.Find(x => x.name == "item");
        item = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(item);
    }
}
