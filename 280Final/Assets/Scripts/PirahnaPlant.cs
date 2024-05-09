using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: [Suazo, Angel]
 * Last Updated: [05/09/2024]
 * [Script that handles all of the pirahna enemys movement and collision]
 */
public class PirahnaPlant : MonoBehaviour
{
    // max and min values for enemy movement on the y
    public float topY;
    public float bottomY;

    //speed for going up
    public float risingSpeed;

    //speed for fallin
    public float fallingSpeed;

    //checks to see if we are going up and waiting
    public bool goingUp = true;
    public bool waiting = false;

    //timers for the waiting
    public float waitTimeAtTop = 4f;
    public float waitTimeAtBottom = 2f;

   //timer for how long enemy will be frozen
    public float freezeTime;

    //checks to see if we are frozen
    private bool frozen = false;

    //timer to start the freeze
    private float freezeStart = -10f;

    // Update is called once per frame
    void Update()
    {
        if (frozen)
        {
            if (freezeStart + freezeTime > Time.time)
            {
                return;
            }
            else
            {
                frozen = false;
            }
        }
        //check to see if the object is waiting before moving 
        if (!waiting)
        {
            if (goingUp)
            {
                //check to see if the object is at the top if so, flip direction
                if (transform.position.y >= topY)
                {
                    goingUp = false;
                    StartCoroutine(Wait(waitTimeAtTop));
                }
                else
                {
                    //since the object isnt at the top keep moving upwards
                    transform.position += Vector3.up * risingSpeed * Time.deltaTime;
                }
            }
            //if going up is false, go down until it reaches the bottom
            else
            {
                //check to see if the object is at the bottom, if so, flip direction
                if (transform.position.y <= bottomY)
                {
                    goingUp = true;
                    StartCoroutine(Wait(waitTimeAtBottom));
                }
                else
                {
                    //since the object is not at the bottom, keep moving down
                    transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make a local gameobject variable
        GameObject other = collision.gameObject;
        //do some stuff depending on what I just crashed into
        switch (other.tag)
        {
            case "FireBall":
                Debug.Log("Enemy collided with a fireball");
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
                other.gameObject.SetActive(false);
                Destroy(other.gameObject);
                break;
            case "IceBall":
                Debug.Log("Enemy collided with an iceball");
                other.gameObject.SetActive(false);
                Destroy(other.gameObject);
                frozen = true;
                freezeStart = Time.time;
                break;

            default:
                // do nothing here, it is probably a player and that is handled in player code
                break;
        }
        //out of the switch
    }

    IEnumerator Wait(float waitTime)
    {
        waiting = true;
        //start the timer,doesnt do any code after this line until the timer is up
        yield return new WaitForSeconds(waitTime);
        //after time has passed, the next lines occur
        waiting = false;
    }

}
