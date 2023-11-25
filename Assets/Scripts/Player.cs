using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool jumpKeyWasPressed = false;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;

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

        if (jumpKeyWasPressed)
        {
            jumpKeyWasPressed = false;
            // Physics updates should be done in FixedUpdate!
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        }

        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);
        
    }
}
