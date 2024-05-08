using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirahnaPlant : MonoBehaviour
{
    public float topY;
    public float bottomY;

    public float risingSpeed;
    public float fallingSpeed;

    public bool goingUp = true;
    public bool waiting = false;

    public float waitTimeAtTop = 4f;
    public float waitTimeAtBottom = 2f;

    public float freezeTime;
    private bool frozen = false;
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
