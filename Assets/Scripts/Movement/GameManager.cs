using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Vector3 spawnPointRed = new Vector3(0, 0, 0);
    public Vector3 spawnPointBlue = new Vector3(0, 0, 0);

    private void Start()  {
        FindObjectOfType<ObstaclesGenerator>().GetComponent<ObstaclesGenerator>().enabled = true;
    }

    public void Respawn(PlayerScript player) {
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
}
