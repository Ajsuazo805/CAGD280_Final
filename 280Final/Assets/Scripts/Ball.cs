using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool goingLeft = true;

    public float speed;
    public void SetGoingLeft(bool goLeft)
    {
        goingLeft = goLeft;
    }

    private float startTime;
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
