using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject playButton;
    public Text redTeamScore;
    public Text blueTeamScore;
    public Text gameLogo;


    // Start is called before the first frame update
    void Start()
    {
        playButton.SetActive(true);

        gameLogo.gameObject.SetActive(true);
        redTeamScore.gameObject.SetActive(false);
        blueTeamScore.gameObject.SetActive(false);
    }

    public void OnPlayButtonPressed() {
        playButton.SetActive(false);

        gameLogo.gameObject.SetActive(false);
        redTeamScore.gameObject.SetActive(true);
        blueTeamScore.gameObject.SetActive(true);


        FindObjectOfType<GameManager>().StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
