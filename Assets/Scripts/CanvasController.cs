using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PS4;


public class CanvasController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject exitButton;
    public Canvas blueWinCanvaas;
    public Canvas redWinCanvas;
    public Text redTeamScore;
    public Text blueTeamScore;
    public Text gameLogo;

    int m_StickId1 = 1;
    int m_StickId2 = 2;

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
        if (xPressedByOneOfPlayers())
            OnPlayButtonPressed();

        if (oPressedByOneOfPlayers()||trigPressedByOneOfPlayers())
            OnExitButtonPressed();

        if (squarePressedByOneOfPlayers())
            OnReturnToMenuButtonPressed();
    }


    bool xPressedByOneOfPlayers()
    {
        return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId1 + "Button0", true)) || Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId2 + "Button0", true));
    }

    bool oPressedByOneOfPlayers()
    {
        //Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button1", true))
        return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId1 + "Button1", true)) || Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId2 + "Button1", true));
    }

    bool trigPressedByOneOfPlayers()
    {
        //Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button1", true))
        return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId1 + "Button3", true)) || Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId2 + "Button3", true));
    }
    bool squarePressedByOneOfPlayers()
    {
        return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId1 + "Button2", true)) || Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId2 + "Button2", true));
    }
}
