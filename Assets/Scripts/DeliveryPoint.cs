using UnityEngine;
using UnityEngine.UI;

public class DeliveryPoint : MonoBehaviour
{
    public Collider trigger;
    public Rigidbody rb;
    public Text scoreText;
    private int score;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("package"))
        {
            score++;
            scoreText.text = "Pizza Points: " + score.ToString();
            Debug.Log("Delivered!");

            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
