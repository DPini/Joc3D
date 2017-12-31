using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int highestZ;
    private int score;
    private Text scoreText;
    

    public void Init()
    {
        highestZ = 0;
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        Debug.Log("ScoreText " + scoreText);
    }

    public void updateScore(Position playerPosition)
    {
        int playerZ = Mathf.RoundToInt(playerPosition.z);
        if (playerZ > highestZ) highestZ = playerZ;

        score = highestZ * 5;

        scoreText.text = "Score: " + score;
    }
	


}
