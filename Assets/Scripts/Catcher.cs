using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "package")
        {
            Destroy(other.gameObject);
            Debug.Log("Caught " + other.gameObject.name);
        }

    }
}
