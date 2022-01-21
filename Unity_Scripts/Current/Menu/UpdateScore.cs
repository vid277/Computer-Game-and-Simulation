using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    public Text scorevalue;

    void Awake(){
        if (PlayerPrefs.HasKey("Score")){
            scorevalue.text = PlayerPrefs.GetString("Score");
        }
        else{
            scorevalue.text = "0";
        }
    }

    void Update(){
        scorevalue.text = (Enemy.score).ToString();
        PlayerPrefs.SetString("Score", (Enemy.score).ToString());
    }
}