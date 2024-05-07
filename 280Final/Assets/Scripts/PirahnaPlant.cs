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
    // Start is called before the first frame update
    void Start()
    {
        
    }
     
    // Update is called once per frame
    void Update()
    {
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

    IEnumerator Wait(float waitTime)
    {
        waiting = true;
        //start the timer,doesnt do any code after this line until the timer is up
        yield return new WaitForSeconds(waitTime);
        //after time has passed, the next lines occur
        waiting = false;
    }

}
