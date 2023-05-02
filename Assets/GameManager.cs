using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager;
    public GameObject deliveryTrigger;
    public GameObject existingTriggers;
    public PlayerMovement PlayerMovement;
    public GameObject gameOver;
    public bool gameActive = true;
    public GameObject[] pizzaNo;
    public CollectionTrigger ColTrig;
    [SerializeField]
    private float money;
    public int strikes;
    [SerializeField]
    private int deliveryTarget;
    public Text scoreText;
    public Text strikeText;
    public Text youmadeText;

    void Update()
    {
        if(gameActive == false && Input.GetAxisRaw("Jump") > 0.1f)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("TestScene");

        }

        deliveryTarget = ColTrig.spawnNo;
        scoreText.text = "$" + money.ToString();
        if(strikes == 1)
        {
            strikeText.text = "You have " + strikes.ToString() + " strike!";
        }
        else
        {
            strikeText.text = "You have " + strikes.ToString() + " strikes!";
        }
        youmadeText.text = "You made " + money.ToString() + " dollars!";

    }

    public void plotDestination()
    {
        GameObject[] deliveryLocations;
        deliveryLocations = GameObject.FindGameObjectsWithTag("deliveryLocation");
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
        GameObject[] trigger = GameObject.FindGameObjectsWithTag("delTrigger");
        
        for (int i = 0; i < pizzaNo.Length; i++)
        {
            Debug.Log("Delivery Success");
            money = 2.50f * pizzaNo.Length;
            Destroy(pizzaNo[i]);
        }

        Destroy(trigger[0]);
        ColTrig.spawnNo = Random.Range(1, 7);
    }
    public bool isOrderFailed()
    {   
        pizzaNo = GameObject.FindGameObjectsWithTag("package");
        if (pizzaNo.Length == 0)
        {
            Destroy(existingTriggers);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void damageCharge()
    {
        money -= 2.50f;
        Debug.Log("You have been charged for crimes against pizza :(");
        if(isOrderFailed())
        {
            strikes++;
            if(strikes == 3)
            {
                gameOver.SetActive(true);
                gameActive = false;
                PlayerMovement.isGrounded = false;
            }
        }
    }

    void Awake()
    {
        if (gManager == null)
        {
            gManager = this;
        }
    }
}
