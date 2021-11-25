using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text redScoreText;
    public Text blueScoreText;

    public int redScore;
    public int blueScore;

    public bool autoUpdateScore = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetRedScore(int score) {
        redScore = score;
    }

    public void SetBlueScore(int score) {
        blueScore = score;
    }

    // Update is called once per frame
    void Update()
    {
        if (autoUpdateScore) {
            UpdateScores();
        }

        redScoreText.text = "Red Score: " + redScore.ToString();
        blueScoreText.text = "Blue Score: " + blueScore.ToString();
    }

    public void UpdateScores()
    {
        foreach (PlayerScript ps in FindObjectsOfType<PlayerScript>()) {
            if (ps.teamName == "Red") {
                redScore = (int)ps.score;
            }
            else if (ps.teamName == "Blue") {
                blueScore = (int)ps.score;
            }
        }
    }
}
