using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Visible in the unity UI
    [SerializeField] public Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed = false;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    // ints default to 0 anyway
    private int superJumpsRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame (dependent on the frame rate if vsync is off)
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
            Debug.Log("Space key was pressed.");
        }

        horizontalInput = Input.GetAxis("Horizontal");


    }

    // FixedUpdate is called once per physics frame (default is 100 times per second)
    private void FixedUpdate()
    {

        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);
        /*
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) {
            // We're in the air
            return;
        }
        */

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            // We're in the air
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0) {
                jumpPower*=2;
                superJumpsRemaining--;
            }
            jumpKeyWasPressed = false;
            // Physics updates should be done in FixedUpdate!
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        }


    }

    private void OnTriggerEnter(Collider other) {
        // Layer 7 is the coin layer
        if (other.gameObject.layer == 7) {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

    }

}
