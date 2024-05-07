using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerData : MonoBehaviour
{
    //enum to have different playerstates
    private enum PlayerState
    {
        Normal,
        Fire,
        Invincible,
        Ice
    }

    //setting the player to a normal state
    private PlayerState playerState = PlayerState.Normal;

    //period where the player is safe from getting killed after going back to normal state
    public float safePeriod;

    //timer for the safePeriod 
    private float safePeriodStart = -10f;

    //Players Rigidbody
    private Rigidbody playerRigidBody;

    //Input Actions System
    private InputSystem playerInputActions;

    //players movement speed
    public float speed = 1f;

    //force variable for players jump
    public float jumpForce = 8f;

    //variable to check if we are on the ground
    private bool onGround;

    //checking to see if we are poweredUP
    private bool poweredUp = false;

    //start time for power ups
    private float powerUpStart = -10f;

    //timer for the invincibility power up
    public float invincibilityDuration;

    // These are the 4 different kinds of player variants our player can change into when
    // he equips the different kinds of power ups
    public GameObject defaultPlayer;
    public GameObject firePlayer;
    public GameObject starPlayer;
    public GameObject icePlayer;

    public GameObject fireBall;
    public GameObject iceBall;

    private bool goingLeft = false;

    // last shot from the player
    private float lastShot = -10f;
    //the delay for the shots being shot
    public float shootDelay;


    // Start is called before the first frame update
    private void Awake()
    {
        playerRigidBody = this.GetComponent<Rigidbody>();
        playerInputActions = new InputSystem();
        playerInputActions.Enable();
        defaultPlayer = this.gameObject;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            onGround = true;
            Debug.Log("I am grounded");
        }
        else
        {
            onGround = false;
            Debug.Log("I am not grounded");
        }
        if (onGround == true)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        print("Shoot button was pressed");
        if (lastShot + shootDelay < Time.time)
        {
            if (playerState == PlayerState.Ice)
            {
                Vector3 myVector3;
                if (goingLeft)
                {
                    myVector3 = new Vector3(transform.position.x -0.5f, transform.position.y);
                }
                else
                {
                    myVector3 = new Vector3(transform.position.x + 0.5f, transform.position.y);
                }
                Instantiate(iceBall, myVector3, transform.rotation);
                lastShot = Time.time;
            }
            else if (playerState == PlayerState.Fire)
            {
                Vector3 myVector3;
                if (goingLeft)
                {
                    myVector3 = new Vector3(transform.position.x - 0.5f, transform.position.y);
                }
                else
                {
                    myVector3 = new Vector3(transform.position.x + 0.5f, transform.position.y);
                }
                Instantiate(fireBall, myVector3, transform.rotation);
                lastShot = Time.time;
            }            
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 myVector = context.ReadValue<Vector2>();
        if (myVector.x > 0)
        {
            goingLeft = false;
        }
        else if (myVector.x < 0)
        {
            goingLeft = true;
        }
        playerRigidBody.transform.Translate(new Vector3(myVector.x, 0) * speed);

    }

    private void FixedUpdate()
    {

        Vector2 moveVector = playerInputActions.InGame.Move.ReadValue<Vector2>();
        playerRigidBody.transform.Translate(new Vector3(moveVector.x, 0) * speed);
    }

    private void TransformPlayer(GameObject newPlayer)
    {
        Vector3 holdPos = this.gameObject.transform.position;
        Quaternion holdRot = this.gameObject.transform.rotation;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        GameObject tempObj = Instantiate(newPlayer,holdPos, holdRot);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        print("GameObject tag " + other.tag+ " current state =" +playerState);
        if (other.tag == ("FirePower"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            playerState = PlayerState.Fire;
            TransformPlayer(firePlayer);

        }
        if (other.tag == ("StarPower"))
        {
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            playerState = PlayerState.Invincible;
            TransformPlayer(starPlayer);

        }
        if (other.tag == ("IcePower"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            playerState = PlayerState.Ice;
            TransformPlayer(icePlayer);


        }
        if (other.tag == ("Enemy"))
        {
            if (playerState == PlayerState.Fire || playerState == PlayerState.Ice)
            {
                poweredUp = false;
                playerState = PlayerState.Normal;
                safePeriodStart = Time.time;
            }
            else
            {
                if (poweredUp && playerState == PlayerState.Invincible)
                {
                    if (powerUpStart + invincibilityDuration < Time.time)
                    {
                        poweredUp = false;
                        playerState = PlayerState.Normal;
                        TransformPlayer(defaultPlayer);
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }

                }
                if (poweredUp == false && (safePeriodStart + safePeriod < Time.time))
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}