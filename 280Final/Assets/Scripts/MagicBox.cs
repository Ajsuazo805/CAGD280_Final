using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBox : MonoBehaviour
{
    public GameObject fire;
    public GameObject ice;
    public GameObject star;

    private int currentPow = 0;
    private float lastDrop = -10f;
    public float dropDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entering Magic Box on collision");
        GameObject other = collision.gameObject;
        if (other.tag==("Player"))
        {
            Debug.Log("Magic Box collided with "+ other.gameObject);
            if (lastDrop + dropDelay > Time.time)
            {
                Debug.Log("Magic Box not ready yet. lastdrop = " + lastDrop + " dropdelay = " + dropDelay);
                return;
            }
            Vector3 holdPos = this.gameObject.transform.position;
            Quaternion holdRot = this.gameObject.transform.rotation;
            //this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            if (currentPow == 0)
            {
                Instantiate(fire, holdPos, holdRot);
            }
            else if (currentPow == 1)
            {
                Instantiate(ice, holdPos, holdRot);
            }
            else
            {
                Instantiate(star, holdPos, holdRot);
            }
            currentPow++;
            if (currentPow > 2)
            {
                currentPow = 0;
            }
            lastDrop = Time.time;
        }
    }
}
