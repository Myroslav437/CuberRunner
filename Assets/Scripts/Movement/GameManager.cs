using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Vector3 spawnPointRed = new Vector3(0, 0, 0);
    public Vector3 spawnPointBlue = new Vector3(0, 0, 0);

    public Canvas blueWinCanvas;
    public Canvas redWinCanvas;

    float timeFinish = 0;
    public float mathchTime = 60;
    bool gameStarted = false;

    private void Start()  {
        gameStarted = false;

        FindObjectOfType<ObstaclesGenerator>().GetComponent<ObstaclesGenerator>().enabled = false;

        foreach (PlayerScript ps in FindObjectsOfType<PlayerScript>()) {
            Respawn(ps);
        }
    }

    public void Respawn(PlayerScript player) {
        Vector3 newVelocity = player.GetComponent<Rigidbody>().velocity;
        newVelocity.z = 0;
        player.GetComponent<Rigidbody>().velocity = newVelocity;

        player.PelalizeScoreOnRespawn();

        if (player.teamName == "Blue") {
            player.transform.SetPositionAndRotation(spawnPointBlue, Quaternion.identity);
        }
        else if (player.teamName == "Red") {
            player.transform.SetPositionAndRotation(spawnPointRed, Quaternion.identity);
        }
        else {
            player.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void StartGame() {
        FindObjectOfType<ObstaclesGenerator>().GetComponent<ObstaclesGenerator>().enabled = true;

        foreach (PlayerScript ps in FindObjectsOfType<PlayerScript>()) {
            Respawn(ps);
            ps.StartScoreCount();
        }

        gameStarted = true;
    }

    public void EndGame() {
        gameStarted = false;

        float redPoints = 0, bluePoints = 0;
        foreach (PlayerScript ps in FindObjectsOfType<PlayerScript>()) {
            if (ps.teamName == "Red")
            {
                redPoints = ps.score;
                ps.score = 0;
                ps.StopScoreCount();
            }
            else if (ps.teamName == "Blue") {
                bluePoints = ps.score;
                ps.score = 0;
                ps.StopScoreCount();
            }
        }

        FindObjectOfType<ObstaclesGenerator>().GetComponent<ObstaclesGenerator>().enabled = false;

        if (redPoints < bluePoints) {
            blueWinCanvas.gameObject.SetActive(true);
        }
        else {
            redWinCanvas.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (gameStarted) {
            timeFinish += Time.deltaTime;

            if (timeFinish >= mathchTime) {
                FindObjectOfType<ObstaclesGenerator>().SpawnFinishTrigger();
            }
        }
        else {
            timeFinish = 0;
        }
    }
}
