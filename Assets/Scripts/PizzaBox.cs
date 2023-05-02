using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
    [SerializeField] public List<Collider> allCollisions = new List<Collider>();
    public Rigidbody rb;
    [SerializeField] GameObject damagedPrefab;

    GameObject damagedPizza;

    Vector3 storedPostion;
    Quaternion storedRotation;
    [SerializeField] Vector3 displacement;
    [SerializeField] Vector3 rotationalDisplacement;

    public GameObject targetPackage;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Vector3 targetRotation;
    [SerializeField] float rotationMod;
    public int proximityToPlatform = 0;
    public GameObject platformInStructure;
    private bool isDamaged = false;

    RaycastHit hit;

    private void Awake() {
        damagedPizza = Instantiate(damagedPrefab);
        damagedPizza.SetActive(false);
    }
    private void OnCollisionStay(Collision other) {
        var raycast = Physics.Raycast(transform.position,Vector3.down, out hit, 0.5f);
        Debug.DrawRay(transform.position,Vector3.down*0.5f, Color.red, Mathf.Infinity);
        if(raycast)
        {
                if(hit.collider.gameObject.tag != "platform" && hit.collider.gameObject.tag != "package" && hit.collider.gameObject.tag != "trigger")
                {
                    allCollisions.Remove(hit.collider);

                    if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
                    {
                        //Create Damaged Asset
                        isDamaged = true;
                    }
                }
                else if(hit.collider.gameObject.tag != "package")
                {
                    if(other.gameObject.GetComponent<PizzaBox>().platformInStructure == null)
                        isDamaged = true;
                }
                else if(!allCollisions.Contains(hit.collider))
                {
                    allCollisions.Add(hit.collider);     
                    Debug.Log("Added " + hit.collider.transform.name + " to " + this.name + " collider Collection");
                    //set target position plus rotation if platform found
                    if(DetermineIfPlatformIsInStructure(allCollisions))
                    {
                        targetPackage = hit.collider.gameObject;
                        targetPosition = new Vector3(targetPackage.transform.position.x,rb.position.y+0.005f,targetPackage.transform.position.z);
                        targetRotation = targetPackage.transform.rotation.eulerAngles;  

                        displacement = rb.position - targetPosition; 
                        rotationalDisplacement = rb.rotation.eulerAngles - targetRotation;
                    }             
                }
        }
    }

    void OnDestroy()
    {
        if(isDamaged)
        {
            damagedPizza.SetActive(true);
            damagedPizza.transform.position = transform.position;
            damagedPizza.transform.rotation = transform.rotation;
            damagedPizza.GetComponent<Rigidbody>().velocity = rb.velocity;
            targetPackage = null;
            platformInStructure = null; 
            GameManager.gManager.damageCharge();  
        }
  
    }

    private void OnCollisionExit(Collision other) {
        if(allCollisions.Contains(other.collider))
            allCollisions.Remove(other.collider);
    }

    private void FixedUpdate() {   
        foreach(Collider c in allCollisions)
        {
            if(c == null)
            {
                allCollisions.Remove(c);
                break;
            }
        }
        if(DetermineIfPlatformIsInStructure(allCollisions) && targetPackage != null && platformInStructure != null)
        {
            targetPosition = new Vector3(targetPackage.transform.position.x,rb.position.y+0.005f,targetPackage.transform.position.z);
            targetRotation = targetPackage.transform.rotation.eulerAngles;    

            float distanceToTarget = Vector3.Distance(rb.position,targetPosition);
            rotationMod = (1-Mathf.Clamp(distanceToTarget, 0, 5f)) / 5f;
            if(Vector3.Distance(rb.position,targetPosition) > 0.005f)
            {
                var pos = rb.position;
                pos.x = Mathf.Lerp(pos.x,targetPosition.x,rotationMod);
                pos.z = Mathf.Lerp(pos.z, targetPosition.z,rotationMod);
                rb.MovePosition(pos);              
            }
            Quaternion.Slerp(rb.rotation,Quaternion.Euler(rb.rotation.x, targetRotation.y, rb.rotation.z), rotationMod);
        }
        if(isDamaged)
        {
            Destroy(this.gameObject);
        }
    }

    bool DetermineIfPlatformIsInStructure(List<Collider> allCollisions)
    {
        foreach(Collider c in allCollisions)
        {
            if(c.transform.tag == "platform")
            {
                platformInStructure = c.gameObject;
                Debug.Log("platform detected touching " + this.name);
                return true;
            }
            else if(c.transform.tag == "package")
            {
                PizzaBox p = c.gameObject.GetComponent<PizzaBox>();
                if(p.platformInStructure != null)
                {
                    platformInStructure = p.platformInStructure;
                    return true;
                }
            }
        }
        platformInStructure = null;
        return false;
    }
}
