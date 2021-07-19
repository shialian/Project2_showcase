using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public enum Direction{
    N,
    NE,
    E,
    SE,
    S,
    SW,
    W,
    NW,
    NONE
};

public class PoseGameManager : NetworkBehaviour
{
    // if it is single player game just need to assign "one" PoseGame to P1 or P2
    public PoseGame P1;
    public PoseGame P2;
    // ID let UI identify ( 0 : wheel , 1 : Castle , 2 : Carousel)
    public int PoseGameID;
    public AudioSource source;
    public AudioClip clip;
    public bool played;
    List<PoseGame> GameList = new List<PoseGame>();
    // below value set public for debug 
    // (init in start no matter what value is setted)
    /************************************************/
    // this flag means whether the players finish whole pose game
    [SyncVar]
    public bool is_complete = false;
    // trigger game start. prevent palyer complete game immediately after enter game scene
    [SyncVar]
    public bool is_start = false;
    // Start is called before the first frame update
    void Start()
    {
        if(P1 != null){
            GameList.Add(P1);
        }
        if(P2 != null){
            GameList.Add(P2);
        }
        is_complete = false;
        is_start = false;
    }

    // Update is called once per frame
    void Update()
    {
        JudgePlayerPose();
        CompleteGame();
    }

    public bool CompleteGame(){
        int i;
        //Debug.Log("Game num : " + GameList.Count);
        if(!is_complete){
            for(i = 0 ; i < GameList.Count ; i++){
                if(!GameList[i].is_pass)
                    break;
            }
            if(i == GameList.Count){
                Debug.Log("Whole Game Pass!");
                is_complete = true;
                if (played == false)
                {
                    source.PlayOneShot(clip);
                    played = true;
                }
            }
        }
        return is_complete;
    }
    void JudgePlayerPose(){
        if(!is_start || is_complete)
            return ;
        Player[] PlayerList = FindObjectsOfType<Player>();
        for(int i = 0 ; i < PlayerList.Length ; i++ ){
            int ID = PlayerList[i].PlayerID;
            PoseGame game = GameList[ID % GameList.Count];
            if(PlayerList[i].enabled){
                // Debug.Log("Test Game NO." + ID % GameList.Count);
                PlayerList[i].DeterminePose(game);
            }
        }
    }
    // Starting the game which can be call by any player
    [Command(requiresAuthority = false)]
    public void StartGame(){
        is_start = true;
    }
}
