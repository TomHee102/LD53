using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float movementSpeed;
    public float turnSpeed;

    private Vector3 movement;
    private Vector3 direction;

    [SerializeField]
    private LayerMask Environment;
    [SerializeField]
    private float Time;
    [SerializeField]
    private AnimationCurve animCurve;

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        movement = new Vector3(Input.GetAxisRaw("Vertical"), 0f, 0f).normalized;
        direction = new Vector3(0f, Input.GetAxisRaw("Horizontal"), 0f).normalized;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Quaternion RotationRef = Quaternion.Euler(0, 0, 0);
        Debug.DrawRay(transform.position, -transform.up, Color.red);

        if (Physics.Raycast(ray, out hit, 1f, Environment))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 down = transform.TransformDirection(Vector3.down);
            Vector3 toOther = hit.transform.position;

            Vector3 infoVector = hit.normal;

            if (Vector3.Dot(forward, toOther) == 0) 
            {

            }
            else if (Vector3.Dot(forward, toOther) > 0)
            {
                // Debug.Log("Slope ahead!");
                infoVector = new Vector3(hit.normal.x, hit.normal.y, hit.normal.z);
            }
            else
            {
                // Debug.Log("Slope behind!");
                infoVector = new Vector3(-hit.normal.x, hit.normal.y, -hit.normal.z);
            }

            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, infoVector), animCurve.Evaluate(Time));
            transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.rotation.eulerAngles.y, RotationRef.eulerAngles.z);
        }

        if (movement.x > 0f)
        {
            rb.AddForce((rb.transform.forward) * movementSpeed);
        }
        else if (movement.x < 0f)
        {
            rb.AddForce(((rb.transform.forward) * -1) * movementSpeed);           
        }
        
        rb.angularVelocity = (direction * turnSpeed);
    }
}
