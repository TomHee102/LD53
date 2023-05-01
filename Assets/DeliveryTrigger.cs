using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTrigger : MonoBehaviour
{
    private bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "package")
        {
            Debug.Log("Delivery Point");
            isTriggered = true;
            GameObject pizzas = GameObject.FindGameObjectWithTag("package");
            Destroy(pizzas);

            GameManager.gManager.checkDelivery();
        }
    }
    void Update()
    {
        
    }
}
