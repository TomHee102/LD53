using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;
    public GameObject deliveryTrigger;
    public CollectionTrigger ColTrig;
    [SerializeField]
    private float money;
    [SerializeField]
    private int deliveryTarget;
    public Text scoreText;

    void Update()
    {
        deliveryTarget = ColTrig.spawnNo;
        scoreText.text = money.ToString();
    }

    public void plotDestination()
    {
        GameObject[] deliveryLocations;
        deliveryLocations = GameObject.FindGameObjectsWithTag("deliveryLocation");
        GameObject existingTriggers;
        existingTriggers = GameObject.FindGameObjectWithTag("delTrigger");
        int locationChoice = Random.Range(1, deliveryLocations.Length + 1);

        if (existingTriggers == false)
        {
            Vector3 spawnOffset = new Vector3(0f, 3.5f, 0f);
            Instantiate(deliveryTrigger, deliveryLocations[locationChoice].transform.position + spawnOffset, Quaternion.identity);
        } 
    }

    public void checkDelivery()
    {
        GameObject[] pizzaNo;
        pizzaNo = GameObject.FindGameObjectsWithTag("package");
        
        if  (pizzaNo.Length == 0)
        {
            Debug.Log("You failed! good day sir!");
        }
        else
        {
            Debug.Log("Delivery Success");
            money = 2.50f * pizzaNo.Length;
        }
    }

    public void damageCharge()
    {
        money -= 2.50f;
        Debug.Log("You have been charged for crimes against pizza :(");
    }

    void Awake()
    {
        if (gManager == null)
        {
            gManager = this;
        }
    }
}
