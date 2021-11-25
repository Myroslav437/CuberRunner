using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject exitButton;
    public Canvas blueWinCanvaas;
    public Canvas redWinCanvas;
    public Text redTeamScore;
    public Text blueTeamScore;
    public Text gameLogo;


    // Start is called before the first frame update
    void Start()
    {
        playButton.SetActive(true);
        exitButton.SetActive(true);

        gameLogo.gameObject.SetActive(true);
        redTeamScore.gameObject.SetActive(false);
        blueTeamScore.gameObject.SetActive(false);

        blueWinCanvaas.gameObject.SetActive(false);
        redWinCanvas.gameObject.SetActive(false);
    }

    public void OnPlayButtonPressed() {
        playButton.SetActive(false);
        exitButton.SetActive(false);

        gameLogo.gameObject.SetActive(false);
        redTeamScore.gameObject.SetActive(true);
        blueTeamScore.gameObject.SetActive(true);


        blueWinCanvaas.gameObject.SetActive(false);
        redWinCanvas.gameObject.SetActive(false);

        FindObjectOfType<GameManager>().StartGame();
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OnReturnToMenuButtonPressed() {
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
