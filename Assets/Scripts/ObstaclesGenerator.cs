using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour {

    public static ObstaclesGenerator gen;

    public Vector3 spawnPoint = new Vector3(0, 1, 2000);
    public Vector2 distanceFreqencyBounds = new Vector2(11, 30);
    public Vector2 XBounds = new Vector2(6.5f, -6.5f);

    GameObject prevObj = null;
    float spawnDist;

    // Start is called before the first frame update
    void Start() {
        if (ObstaclesGenerator.gen == null) {
            ObstaclesGenerator.gen = this;
        }
        else {
            if (ObstaclesGenerator.gen != this) {
                Destroy(ObstaclesGenerator.gen.gameObject);
                ObstaclesGenerator.gen = this;
            }
        }

    }

    // Update is called once per frame
    void Update() {
        spawnDist = Random.Range(distanceFreqencyBounds.x, distanceFreqencyBounds.y);

        if (prevObj == null) {
            prevObj = Spawn();
        }
        else {
            float dist = spawnPoint.z - prevObj.transform.position.z;
            if (dist > spawnDist) {
                prevObj = Spawn();
            }
        }
    }

    public GameObject Spawn() {
        int type = Random.Range(0, 3);
        string ObstaclesPFName = "Obstacles/";
        Vector3 sp = spawnPoint;

        // Regular Obstacle;
        if (type == 0) {
            ObstaclesPFName += "Obstacle";
            sp.x = Random.Range(XBounds.x, XBounds.y);

        }
        // Five in row left:
        else if (type == 1) {
            ObstaclesPFName += "FiveSkewObstacleLeft";
            sp.x = 6.5f;
        }
        // Five in row right:
        else if (type == 2) {
            ObstaclesPFName += "FiveSkewObstacleRight";
            sp.x = -6.5f;
        }
        else {
            ObstaclesPFName += "Obstacle";
        }
        
        return InstantiateObstacle(ObstaclesPFName, sp, Quaternion.identity);
    }

    GameObject InstantiateObstacle(string pfb, Vector3 position, Quaternion rotation) {
        GameObject go = Instantiate(Resources.Load(pfb, typeof(GameObject)), position, rotation) as GameObject;
        return go;
    }

    public GameObject SpawnFinishTrigger() {
        GameObject go = InstantiateObstacle("FinishGameCube", spawnPoint, Quaternion.identity);
        return go;
    }

}
