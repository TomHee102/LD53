using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    public GameObject pizza;
    private bool isTriggered = false;
    private Vector3 otherPos;
    private Rigidbody otherRb;
    public int spawnNo;
    private int i = 0;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Collection Point!");
            isTriggered = true;
            otherPos = other.transform.position;
            otherRb = other.attachedRigidbody;
        }
    }
    void FixedUpdate()
    {

        if (i < spawnNo && isTriggered && otherRb.velocity.x < 0.1f)
        {
            Vector3 spawnOffset = new Vector3(0f, 3, 0f);
            Instantiate(pizza, otherPos + spawnOffset, Quaternion.identity);
            i++;
        }
        else
        {
            isTriggered = false;
        }
    }
}
