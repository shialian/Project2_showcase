using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UI_test : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject x;
    public static UI_test UI;
    void Start()
    {
        UI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void summon(){
        Debug.Log("Activate create item command from button , Auth : " + hasAuthority);
        this.create_item();
    }

    public void create_item(){
        Debug.Log("Create item Sucess");
        GameObject item;
        item = MyNetworkManager.NM.spawnPrefabs.Find(x => x.name == "item");
        item = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(item);
    }

    public void create_item2(){
        GameObject item;
        item = Instantiate(x, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(item);
    }

    public void change_scene(){
        MyNetworkManager.NM.ServerChangeScene("Scene1");
    }
    public void change_to_main(){
        MyNetworkManager.NM.ServerChangeScene("Main");
    }
    public void add(int i){
        GameManagerTest.GM.add(i);
    }
}
