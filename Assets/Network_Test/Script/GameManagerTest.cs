using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManagerTest : NetworkBehaviour
{
    //temp variable for test
    SyncList<int> num = new SyncList<int>();
    public static GameManagerTest GM;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GM = this;
        for(int i = 0 ; i < 2 ; i++){
            num.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Command(requiresAuthority = false)]
    public void add(int count){
        for(int i = 0 ; i < num.Count ; i++){
            num[i] += count;
            Debug.Log("GameManager " + i + " : " + num[i]);
        }
    }
}
