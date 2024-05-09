using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: [Suazo, Angel]
 * Last Updated: [05/09/2024]
 * [Script that handles everything for the magic box gameobject]
 */
public class MagicBox : MonoBehaviour
{
    //power up prefabs
    public GameObject fire;
    public GameObject ice;
    public GameObject star;

    // int to show which prefab we want to spawn
    private int currentPow = 0;

    //timer to remember when we started to drop a prefab
    private float lastDrop = -10f;

    //delay for the power ups to not drop simultaniously
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
