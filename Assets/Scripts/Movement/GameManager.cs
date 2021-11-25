using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Vector3 spawnPoint = new Vector3(0, 0, 0);

    private void Start()  {
        FindObjectOfType<ObstaclesGenerator>().GetComponent<ObstaclesGenerator>().enabled = true;
    }

    public void Respawn(GameObject who) {
        who.GetComponent<Transform>().position = spawnPoint;
    }
}
