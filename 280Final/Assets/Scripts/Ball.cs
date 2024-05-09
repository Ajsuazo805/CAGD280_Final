using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: [Suazo, Angel]
 * Last Updated: [05/09/2024]
 * [Script that handles all of the ball movement and collision]
 */
public class Ball : MonoBehaviour
{
    //checks to see if player was last going left
    private bool goingLeft = true;

    //speed variable
    public float speed;
   
    //setting the time when we launch our ball
    private float startTime;

    //timer for how long the balls will be active in the scene for if we do not hit an enemy
    public float maxBallTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetGoingLeft(bool goLeft)
    {
        goingLeft = goLeft;
    }
    private void Move()
    {
        if (startTime + maxBallTime < Time.time)
        {
            Destroy(this.gameObject);
            return;
        }
        if (goingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
       
    }
}
