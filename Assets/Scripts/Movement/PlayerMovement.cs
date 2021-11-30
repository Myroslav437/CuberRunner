using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.PS4;

public class PlayerMovement : MonoBehaviour  {

    public Rigidbody rb;

    public float maximalXVelocsity;

    public float sideForce = 500f;
    public float jumpForce = 600f;
    public float jumpReload = 0.1f;
    private bool toJump = false;
    private bool jumpButtonPreviouslyPressed = false;

    public float shakeTime = .2f;
    public float shakeStrength = .2f;

    //PS4 Dualshock variables
    int m_StickId;
    PS4Input.LoggedInUser m_LoggedInUser;
    public int playerId = -1;

    Vector3 oldControllerRotation = new Vector3(0,0,0);

    // Touchpad variables
    int m_TouchNum, m_Touch0X, m_Touch0Y, m_Touch0Id, m_Touch1X, m_Touch1Y, m_Touch1Id;
    int m_TouchResolutionX, m_TouchResolutionY, m_AnalogDeadZoneLeft, m_AnalogDeadZoneRight;
    float m_TouchPixelDensity;

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
            Vector3 newControllerRotation = new Vector3(-PS4Input.PadGetLastOrientation(playerId).x, -PS4Input.PadGetLastOrientation(playerId).y, PS4Input.PadGetLastOrientation(playerId).z) * 100;
           

            PS4Input.GetLastTouchData(playerId, out m_TouchNum, out m_Touch0X, out m_Touch0Y, out m_Touch0Id, out m_Touch1X, out m_Touch1Y, out m_Touch1Id);

            Vector3 movementStick = new Vector3(Input.GetAxis("leftstick" + m_StickId + "horizontal"), 0);

            Vector3 movementDpad = new Vector3(Input.GetAxis("dpad" + m_StickId + "_horizontal"), 0);

            bool jumpButtnonPressed = Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button0", true));

            //movement control

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

            //jump control

            if (jumpButtnonPressed && toJump && !jumpButtonPreviouslyPressed)
            {
                jumpButtonPreviouslyPressed = true;
            }

            if (!jumpButtnonPressed && toJump && jumpButtonPreviouslyPressed)
            {

                    Debug.Log("Jump triggered by p" + playerId);
                    
                    rb.AddForce(Vector3.up * jumpForce*Time.deltaTime, ForceMode.Impulse);
                    toJump = false;
                    jumpButtonPreviouslyPressed = false;
                   
                    transform.DORewind();
                    transform.DOShakeScale(shakeTime, shakeStrength, 2, 10);
            }

        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Surface") {
           // Debug.Log("Jump reloaded" + playerId);
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
       
        toJump = true;
    }

    public void DisableJump() {
        toJump = true;
    }

}
