using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float tiltAngle = 7f;
    private Vector3 tiltAround;
    // Update is called once per frame
    void Update()
    {
        // Tilts a the transform towards target rotation based on user input.
        tiltAround.x = Input.GetAxis("Vertical") * tiltAngle;
        tiltAround.z = Input.GetAxis("Horizontal") * tiltAngle;

        Vector3 rotationVector = new Vector3(-tiltAround.x, rb.transform.rotation.eulerAngles.y, tiltAround.z);
        rb.transform.rotation = Quaternion.Euler(rotationVector);
    }
}