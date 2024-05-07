using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    //Players Rigidbody
    private Rigidbody playerRigidBody;
    //Input Actions System
    private InputSystem playerInputActions;

    //players movement speed
    public float speed = 1f;

    //force variable for players jump
    public float jumpForce = 8f;

    public bool onGround;

    private bool poweredUp = false;

    private float powerUpStart = -10f;
    private int typeOfPowerUP = 0;
    public float invincibilityDuration;

    public GameObject defaultPlayer;
    public GameObject firePlayer;
    public GameObject starPlayer;
    public GameObject icePlayer;

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
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),out hit,1.5f))
        {
            onGround = true;
            Debug.Log("I am grounded");
        }
        else
        {
            onGround = false;
            Debug.Log("I am not grounded");
        }
        if (onGround== true)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 myVector = context.ReadValue<Vector2>();
        playerRigidBody.transform.Translate(new Vector3(myVector.x, 0) * speed); 
    }

    private void FixedUpdate()
    {
        
        Vector2 moveVector = playerInputActions.InGame.Move.ReadValue<Vector2>();
        playerRigidBody.transform.Translate(new Vector3(moveVector.x, 0) * speed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        print("GameObject tag " + other.tag);
        if (other.tag == ("FirePower"))
        {
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            typeOfPowerUP = 1;
            //GameObject newPlayer = Instantiate(firePlayer);
            //newPlayer.transform.position = this.gameObject.transform.position;
            //newPlayer.transform.rotation = this.gameObject.transform.rotation;
            //newPlayer.SetActive(true);
            Instantiate(firePlayer, this.gameObject.transform.position, this.gameObject.transform.rotation);
            this.gameObject.SetActive(false);
            

        }
        if (other.tag == ("StarPower"))
        {
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            typeOfPowerUP = 2;
            
        }
        if (other.tag == ("IcePower"))
        {
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            typeOfPowerUP = 3;
            
        }
        if (other.tag == ("Enemy"))
        {
            if (typeOfPowerUP==1 || typeOfPowerUP==3)
            {
                poweredUp = false;
                typeOfPowerUP = 0;
            }
            else
            {
                if (poweredUp && typeOfPowerUP == 2)
                {
                    if (powerUpStart + invincibilityDuration < Time.time)
                    {
                        poweredUp = false;
                        typeOfPowerUP = 0;
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                    
                }
                if (poweredUp == false)
                {
                    SceneManager.LoadScene(1);

                }
            }
        }
    }
}
