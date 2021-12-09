using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.PS4;

public class PlayerMovement : MonoBehaviour  {

    public Rigidbody rb;

    public float maximalXVelocsity;

    public float sideForce;
    public float forwardForce;
    public float jumpForce = 600f;

    public float jumpReload;
    private float jumpReloadTimer = 0.0f;

    private bool toJump = false;

    public float shakeTime = .2f;
    public float shakeStrength = .2f;

    public float zMax, zMin;

    public Vector3 gyroCorrection;

    bool positiveZgyro = false;
    bool positiveXgyro = false;
    bool minZFix;

    //PS4 Dualshock variables
    int m_StickId;
    PS4Input.LoggedInUser m_LoggedInUser;
    public int playerId = -1;

    Vector3 oldControllerRotation = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        m_StickId = playerId + 1;
        m_LoggedInUser = PS4Input.RefreshUsersDetails(playerId);

        gyroCorrection = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (PS4Input.PadIsConnected(playerId))
        {
            PlayerMovementUpdate();
            PlayerJumpUpdate();

            /*
            Vector2 leftstick = new Vector2(Input.GetAxis("leftstick" + m_StickId + "horizontal"),
                                            Input.GetAxis("leftstick" + m_StickId + "vertical"));
            */


        }

    }

    private void PlayerMovementUpdate() {
        Vector3 gyro = new Vector3(-PS4Input.PadGetLastOrientation(playerId).x,
                                      -PS4Input.PadGetLastOrientation(playerId).y,
                                      PS4Input.PadGetLastOrientation(playerId).z);
        // gyro -= gyroCorrection;
        // right:
        if (rb.velocity.x < maximalXVelocsity && gyro.z < 0.0)
        {
            if (positiveZgyro == true)
            {
                Vector3 prevVelocity = rb.velocity;
                prevVelocity.x = 0;
                rb.velocity = prevVelocity;
            }

            rb.AddForce(sideForce * (float)Math.Sqrt(Math.Abs(gyro.z)) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            positiveZgyro = false;
        }
        // left:
        else if (-rb.velocity.x < maximalXVelocsity && gyro.z >= 0.0)
        {
            if (positiveZgyro == false)
            {
                Vector3 prevVelocity = rb.velocity;
                prevVelocity.x = 0;
                rb.velocity = prevVelocity;
            }

            rb.AddForce(-sideForce * (float)Math.Sqrt(Math.Abs(gyro.z)) * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            positiveZgyro = true;
        }

        // forward:
        if (rb.position.z < zMax && gyro.x > 0.0)
        {
            if (positiveXgyro == false)
            {
                Vector3 prevVelocity = rb.velocity;
                prevVelocity.z = 0;
                rb.velocity = prevVelocity;
            }

            rb.AddForce(0, 0, forwardForce * (float)Math.Sqrt(Math.Abs(gyro.x)) * Time.deltaTime, ForceMode.VelocityChange);
            positiveXgyro = true;
        }
        // back:
        else if (rb.position.z >= zMin && gyro.x <= 0.0)
        {
            minZFix = true;
            if (positiveXgyro == true)
            {
                Vector3 prevVelocity = rb.velocity;
                prevVelocity.z = 0;
                rb.velocity = prevVelocity;
            }

            rb.AddForce(0, 0, -forwardForce * (float)Math.Sqrt(Math.Abs(gyro.x)) * Time.deltaTime, ForceMode.VelocityChange);
            positiveXgyro = false;
        }
        else {
            if (minZFix == true)
            {
                minZFix = false;
                Vector3 prevVelocity = rb.velocity;
                prevVelocity.z = 0;
                rb.velocity = prevVelocity;
            }
        }

        /*
           // right:
           if (leftstick.x > 0.1)
           {
               if (rb.velocity.x < maximalXVelocsity)
               {
                   rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
               }
           }

           // left:
           if (leftstick.x < -0.1)
           {
               if (-rb.velocity.x < maximalXVelocsity)
               {
                   rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
               }
           }
           */

    }

    private void PlayerJumpUpdate() {
        bool jumpButtnonPressed = Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button0", true));

        if (jumpButtnonPressed)
        {
            if (toJump) { 
                rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
                toJump = false;

                transform.DORewind();
                transform.DOShakeScale(shakeTime, shakeStrength, 2, 10);
            }
        }

        jumpReloadTimer += Time.deltaTime;
        if (jumpReloadTimer >= jumpReload) {
            jumpReloadTimer = 0.0f;
            toJump = true;
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Surface") {
            toJump = true;
        }

        if (collision.gameObject.tag == "Obstacle") {
            toJump = true;
        }
    }
}
