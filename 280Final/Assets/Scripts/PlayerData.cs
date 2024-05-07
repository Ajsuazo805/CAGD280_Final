using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    //Players Rigidbody
    private Rigidbody playerRigidbody;
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


    // Start is called before the first frame update
    private void Awake()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();
        playerInputActions = new InputSystem();
        playerInputActions.Enable();
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
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 myVector = context.ReadValue<Vector2>();
        playerRigidbody.transform.Translate(new Vector3(myVector.x, 0) * speed); 
    }

    private void FixedUpdate()
    {
        
        Vector2 moveVector = playerInputActions.InGame.Move.ReadValue<Vector2>();
        playerRigidbody.transform.Translate(new Vector3(moveVector.x, 0) * speed);
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
        }
        if (other.tag == ("StarPower"))
        {
            Destroy(other.gameObject);
            poweredUp = true;
            powerUpStart = Time.time;
            typeOfPowerUP = 2;
            StartCoroutine(Blink());
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
                if (poweredUp && (powerUpStart + invincibilityDuration < Time.time) && typeOfPowerUP == 2)
                {
                    poweredUp = false;
                    typeOfPowerUP = 0;
                }
                if (poweredUp == false)
                {
                    Destroy(this.gameObject);
                    SceneManager.LoadScene(1);

                }
            }
        }
    }

    public IEnumerator Blink()
    {
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }
}
