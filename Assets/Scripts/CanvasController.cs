using System;
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

    int m_StickId = 1;

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

        Time.timeScale = 0;
    }

    public void OnPlayButtonPressed() {
        Time.timeScale = 1;

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
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button0", true))||Input.GetKeyDown(KeyCode.P))
            OnPlayButtonPressed();

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button1", true)) || Input.GetKeyDown(KeyCode.E))
            OnExitButtonPressed();

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button2", true)) || Input.GetKeyDown(KeyCode.R))
            OnReturnToMenuButtonPressed();
    }
}
