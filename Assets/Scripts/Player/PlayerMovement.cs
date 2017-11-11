using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class PlayerMovement : MonoBehaviour {
    private static PlayerMovement instance;
    [SerializeField]
    private float speed, jumpSpeedModifier, jumpHeight, fallMultiplier, frameCounterX, frameCounterY, minimumY = -60f, maximumY = 60f;
    private float distToGround, rotationX = 0f, rotationY = 0f;
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;
    private Quaternion originalRotation;

    private List<float> rotArrayX = new List<float>();
    private List<float> rotArrayY = new List<float>();
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
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;

        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }

    private void DynamicFall()
    {
        if (rb != null && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
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
        Debug.Log(distToGround);
    }

    private void Start()
    {
        if (rb)
        {
            rb.freezeRotation = true;
        }
        originalRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        DynamicFall();
    }

    public void Jump()
    {
         Debug.Log("Jumping " + IsGrounded() );
         if (rb != null && IsGrounded())
         {
            rb.velocity = new Vector3(rb.velocity.x, Vector3.up.y * jumpHeight, rb.velocity.z);
            Debug.Log(rb.velocity);
         }
     }
    public void Move(float moveX, float moveY)
    {
        //Slow player and trying to adjust his position mid air (not going forward)
        float ySpeed = (moveY < 0 && !IsGrounded()) ?  speed * jumpSpeedModifier :  speed;

        //impeding X movements when aerial
        //float xSpeed = IsGrounded() ? speed : speed * jumpSpeedModifier;

        //LIMIT DIAGONAL SPEED
        Vector3 movement = Vector3.Normalize((new Vector3(moveX, 0f, moveY)));
        //movement = transform.TransformDirection(movement);
        //not impeding X movements when aerial
        movement.x *= speed;

        movement.z *= ySpeed;
        transform.Translate(movement * Time.deltaTime);
        //controller.Move(movement * Time.deltaTime);   
    }
    public void Rotate(float rotX, float rotY, Transform cam)
    {
        
        cam.Rotate(new Vector3(rotY, 0f, 0f));
        //limit camera yaw
        cam.localEulerAngles = new Vector3(Mathf.Clamp(cam.localEulerAngles.x, 0, 360), 0, 0);


        float rotAverageX = 0f;
        rotationX += rotX;
        rotArrayX.Add(rotationX);
        if(rotArrayX.Count >= frameCounterX)
        {
            rotArrayX.RemoveAt(0);
        }
        for (int i_counterX = 0; i_counterX < rotArrayX.Count; i_counterX++)
        {
            rotAverageX += rotArrayX[i_counterX];
        }
        rotAverageX /= rotArrayX.Count;
        transform.Rotate(new Vector3(0f, rotX, 0f));

        float rotAverageY = 0;
        rotationY -= rotY;
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        rotArrayY.Add(rotationY);

        if (rotArrayY.Count >= frameCounterY)
        {
            rotArrayY.RemoveAt(0);
        }

        for (int i_counterY = 0; i_counterY < rotArrayY.Count; i_counterY++)
        {
            rotAverageY += rotArrayY[i_counterY];
        }

        rotAverageY /= rotArrayY.Count;

        xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        transform.localRotation = originalRotation * xQuaternion;
    }
    //function may b
    public bool IsGrounded()
    {
        //returns true if collides with an obstacle underneath object
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f, LayerMask.GetMask("Obstacle"));
    }
}
