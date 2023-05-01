using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrigger : MonoBehaviour
{
    public GameObject pizza;
    private bool isTriggered = false;
    private Vector3 otherPos;
    private Rigidbody otherRb;
    [SerializeField]
    public int spawnNo;
    private int i = 0;

    private GameObject previousPiza;
    void Awake()
    {
        spawnNo = Random.Range(1, 7);
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Collection Point!");
            isTriggered = true;
            otherPos = other.transform.position;
            otherRb = other.attachedRigidbody;
            GameManager.gManager.plotDestination();
        }
    }
    void FixedUpdate()
    {
        if (i < spawnNo && isTriggered && otherRb.velocity.x < 0.1f)
        {
            Vector3 spawnOffset = new Vector3(0f, 3, 0f);
            var temp = Instantiate(pizza, otherPos + spawnOffset, Quaternion.identity);
            temp.name = i.ToString();
            temp.GetComponent<PizzaBox>().targetPackage = previousPiza;
            if(previousPiza != null)
                temp.transform.position = previousPiza.transform.position + spawnOffset;
            if(i == spawnNo -1)
                previousPiza = null;
            else
                previousPiza = temp;
            i++;
        }
        else
        {
            isTriggered = false;
        }
    }
}
