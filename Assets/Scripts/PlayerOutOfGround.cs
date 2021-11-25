using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerOutOfGround : MonoBehaviour {
    public GameManager gm;

    // Start is called before the first frame update
    void Start() {
        gm = FindObjectsOfType<GameManager>()[0];
    }

    // Update is called once per frame
    void Update() {

        if (transform.position.y < -1) {
            gm.Respawn(this.gameObject);
        }
    }
}
