using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class PlayerMovement : MonoBehaviour {
    private static PlayerMovement instance;
    [SerializeField]
    private float speed, jumpSpeedModifier, jumpHeight, fallMultiplier;
    private float distToGround;
    private Vector3 velocity;
    //private Rigidbody rb;
    private CharacterController controller;
    public static PlayerMovement GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("No instance of " + typeof(PlayerMovement));
            return null;
        }
        return instance;

    }
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private void Update()
    {
        if (IsGrounded() && InputManager.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
        
        if (velocity.y < 0)
        {
            velocity.y += Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    public void Move(float moveX, float moveY)
    {
        //Slow player and trying to adjust his position mid air (not going forward)
        float ySpeed = (moveY < 0 && !IsGrounded()) ?  speed * jumpSpeedModifier :  speed;

        //impeding X movements when aerial
        //float xSpeed = IsGrounded() ? speed : speed * jumpSpeedModifier;

        //LIMIT DIAGONAL SPEED
        Vector3 movement = Vector3.Normalize((new Vector3(moveX, 0f, moveY)));
        movement = transform.TransformDirection(movement);
        //not impeding X movements when aerial
        movement.x *= speed;

        movement.z *= ySpeed;
        //transform.Translate(movement);
        controller.Move(movement * Time.deltaTime);   
    }
    public void Rotate(float rotX, float rotY, Transform cam)
    {
        if (rotY != 0)
        {
            cam.Rotate(new Vector3(rotY, 0f, 0f));
            //limit camera yaw
            cam.localEulerAngles = new Vector3(Mathf.Clamp(cam.localEulerAngles.x, 0, 360), 0, 0);
        }
        if (rotX != 0)
        {
            transform.Rotate(new Vector3(0f, rotX, 0f));
        }
    }
    //function may b
    public bool IsGrounded()
    {
        //returns true if collides with an something underneath object
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, LayerMask.GetMask("Obstacle"));
    }
}
