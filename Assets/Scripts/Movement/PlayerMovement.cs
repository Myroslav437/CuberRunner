using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.PS4;

public class PlayerMovement : MonoBehaviour  {

    public Rigidbody rb;

    public float maximalXVelocsity;

    public float sideForce = 500f;
    public float jumpForce = 0.4f;
    public float jumpReload = 0.1f;
    private bool toJump = false;

    public float shakeTime = .2f;
    public float shakeStrength = .2f;

    int m_StickId;
    PS4Input.LoggedInUser m_LoggedInUser;
    public int playerId = -1;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        m_StickId = playerId + 1;
        m_LoggedInUser = PS4Input.RefreshUsersDetails(playerId);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (PS4Input.PadIsConnected(playerId))
        {

            Vector3 movementStick = new Vector3(Input.GetAxis("leftstick" + m_StickId + "horizontal"), 0);

            Vector3 movementDpad = new Vector3(Input.GetAxis("dpad" + m_StickId + "_horizontal"), 0);

            bool jumpButtnonPressed = Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button0", true));

            if (movementDpad.x > 0.1 || movementStick.x > 0.1)
            {
                if (rb.velocity.x < maximalXVelocsity)
                {
                    rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
            }
            if (movementDpad.x < -0.1 || movementStick.x < -0.1)
            {
                if (-rb.velocity.x < maximalXVelocsity)
                {
                    rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                }
            }

           

            if (jumpButtnonPressed&&toJump)
            {

                    Debug.Log("Jump triggered by p" + playerId);
                    
                    //rb.AddForce(Vector3.Normalize(transform.TransformPoint(Vector3.up)), ForceMode.Impulse) ;
                    rb.AddForce((new Vector3(0,1,0))* sideForce, ForceMode.Impulse);
                    toJump = false;

                   // transform.DORewind();
                    //transform.DOShakeScale(shakeTime, shakeStrength, 2, 10
            }


        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Surface") {
            Debug.Log("Jump reloaded" + playerId);
            if (!toJump) {
                // Invoke("EnableJump", jumpReload);
                toJump = true;
            }
        }

        if (collision.gameObject.tag == "Obstacle") {
            if (!toJump) {
                //Invoke("EnableJump", jumpReload);
                toJump = true;
            }
            // respawn;
        }
    }

    public void EnableJump() {
        Debug.Log("Jump reloaded" + playerId);
        toJump = true;
    }

    public void DisableJump() {
        toJump = true;
    }

}
