using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempUI : MonoBehaviour
{
    public static Vector3 LeftPos;
    public static Vector3 RightPos;
    public static Vector3 CenterPos;
    public static Direction l_dir = Direction.NONE;
    public static Direction r_dir = Direction.NONE;
    public static float theta;
    GameObject left_text;
    GameObject right_text;
    GameObject center_text;
    GameObject result;
    GameObject angle;
    GameObject state;
    Dictionary<Direction,string> dict = new Dictionary<Direction, string>();
    // Start is called before the first frame update
    PoseGame[] GameList;
    void Start()
    {
        left_text = GameObject.Find("LeftDisplayer/Num");
        right_text = GameObject.Find("RightDisplayer/Num");
        center_text = GameObject.Find("CenterDisplayer/Num");
        result = GameObject.Find("Result/Num");
        angle = GameObject.Find("Angle/Num");
        state = GameObject.Find("StageState/Num");
        dict.Add(Direction.N,"N");
        dict.Add(Direction.NE,"NE");
        dict.Add(Direction.NW,"NW");
        dict.Add(Direction.S,"S");
        dict.Add(Direction.SE,"SE");
        dict.Add(Direction.SW,"SW");
        dict.Add(Direction.NONE,"NONE");
        dict.Add(Direction.W,"W");
        dict.Add(Direction.E,"E");
        Cursor.visible = true;
        // Debug.Log(left_text + " / " + right_text);
    }

    // Update is called once per frame
    void Update()
    {
        DisHandPos();
    }

    void DisHandPos(){
        // Debug.Log(result.GetComponent<Text>().text);
        left_text.GetComponent<Text>().text = string.Format("({0:F3},{1:F3},{2:F3})",LeftPos[0],LeftPos[1],LeftPos[2]);
        right_text.GetComponent<Text>().text = string.Format("({0:F3},{1:F3},{2:F3})",RightPos[0],RightPos[1],RightPos[2]);
        center_text.GetComponent<Text>().text = string.Format("({0:F3},{1:F3},{2:F3})",CenterPos[0],CenterPos[1],CenterPos[2]);
        result.GetComponent<Text>().text = "L : " + dict[l_dir] + ",R : " + dict[r_dir];
        angle.GetComponent<Text>().text = theta.ToString();
        GameList = FindObjectsOfType<PoseGame>();
        string str = "";
        for(int i = 0 ; i < GameList.Length ; i++){
            str +=  GameList[i].is_pass;
            if(i == 0)
                str += " / ";
        }
        state.GetComponent<Text>().text = str;
    }
}
