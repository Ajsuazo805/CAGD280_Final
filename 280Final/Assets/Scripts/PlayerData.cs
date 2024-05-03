using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
}
