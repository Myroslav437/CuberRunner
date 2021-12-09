using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;

public class testRespawnCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            int playerId = other.gameObject.GetComponent<PlayerMovement>().playerId;
            other.gameObject.GetComponent<PlayerMovement>().gyroCorrection.Set(
                              -PS4Input.PadGetLastOrientation(playerId).x,
                              -PS4Input.PadGetLastOrientation(playerId).y,
                              PS4Input.PadGetLastOrientation(playerId).z
                        );

            GameObject player = other.gameObject;
            Rigidbody playerRB = player.GetComponent<Rigidbody>();

            if (player.GetComponent<PlayerScript>().teamName.Equals("Red"))
                player.transform.position = new Vector3(-2, 2, 0);


            if (player.GetComponent<PlayerScript>().teamName.Equals("Blue"))
                player.transform.position = new Vector3(2, 2, 0);

            player.transform.rotation = Quaternion.identity;
            player.GetComponent<PlayerScript>().PelalizeScoreOnRespawn();

            playerRB.angularVelocity = new Vector3(0, 0, 0);
            playerRB.velocity = new Vector3(0, 0, 0);
        }
    }

}
