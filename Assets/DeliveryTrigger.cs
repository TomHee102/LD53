using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTrigger : MonoBehaviour
{
    private bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "packaged")
        {

        }
    }
    void Update()
    {
        
    }
}
