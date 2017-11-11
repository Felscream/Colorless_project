using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private static PlayerMovement instance;
    [SerializeField]
    public float speed, jumpSpeedModifier, jumpVelocity, fallMultiplier;
    private float distToGround;
    private Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private void FixedUpdate()
    {
        DynamicFall();
    }
    private void DynamicFall()
    {
        if (rb != null && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
    public void Move(float moveX, float moveY)
    {
        //Slow player and trying to adjust his position mid air (not going forward)
        float ySpeed = (moveY < 0 && !IsGrounded()) ?  speed * jumpSpeedModifier :  speed;

        //impeding X movements when aerial
        //float xSpeed = IsGrounded() ? speed : speed * jumpSpeedModifier;

        //LIMIT DIAGONAL SPEED
        Vector3 movement = Vector3.Normalize(new Vector3(moveX, 0f, moveY)) * Time.deltaTime;
        //not impeding X movements when aerial
        movement.x *= speed;

        movement.z *= ySpeed;
        transform.Translate(movement);
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

    public void Jump()
    {
        Debug.Log("Jumping");
        if (rb != null && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, Vector3.up.y * jumpVelocity, rb.velocity.z);
        }
    }

    public bool IsGrounded()
    {
        //returns true if collides with an something underneath object
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, LayerMask.GetMask("Obstacle"));
    }


}
