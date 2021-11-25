using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour {
    public static float moveSpeed = 20f;
    public Vector2 moveSpeedBounds = new Vector2(10f, 100f);
    public float deltaSpeed = 10f;
    public Rigidbody rd;

    // Start is called before the first frame update
    void Start() {
    }

    void Update() {
        if (Input.GetKey("s"))
        {
            if (moveSpeed > moveSpeedBounds.x)
            {
                moveSpeed -= deltaSpeed;
            }
        }
        if (Input.GetKey("w"))
        {
            if (moveSpeed < moveSpeedBounds.y)
            {
                moveSpeed += deltaSpeed;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        //rd.velocity += new Vector3(0, 0, -moveSpeed);
        rd.AddForce(0, 0, -moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
