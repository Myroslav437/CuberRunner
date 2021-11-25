using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour  {

    public Rigidbody rb;

    public float maximalXVelocsity;

    public float sideForce = 500f;
    public float jumpForce = 500f;
    public float jumpReload = 0.0f;
    private bool toJump = false;

    public float shakeTime = .2f;
    public float shakeStrength = .2f;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetKey("d")) {
            if (rb.velocity.x < maximalXVelocsity) { 
                rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKey("a")) {
            if (-rb.velocity.x < maximalXVelocsity) {
                rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKey(KeyCode.Space)) {
            if (toJump) {
                rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.Impulse);
                toJump = false;

                transform.DORewind();
                transform.DOShakeScale(shakeTime, shakeStrength, 2, 10);

            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Surface") {
            if (!toJump) { 
                Invoke("EnableJump", jumpReload);
            }
        }

        if (collision.gameObject.tag == "Obstacle") {
            if (!toJump) { 
                Invoke("EnableJump", jumpReload);
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
