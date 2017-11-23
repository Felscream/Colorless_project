using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGemItem : Item {
    private Inventory playerInventory;
    [SerializeField]
    private string id, prefabName;
    [SerializeField]
    private int amount;
    [SerializeField]
    private float speed;
    private Vector3 playerPosition;
    private bool moving = false;
    private float distToGround;
    private Rigidbody rb;
    // Update is called once per frame

    private void Start()
    {
        playerPosition = Player.GetInstance().transform.position;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (moving)
        {
            playerPosition = Player.GetInstance().transform.position;
            MoveTowardPlayer();
        }
        if (IsGrounded())
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, LayerMask.GetMask("Obstacle"));
    }
    public void MoveTowardPlayer()
    {
        if(playerPosition != null)
        {
            Vector3 heading = Vector3.Normalize((playerPosition - transform.position));
            transform.Translate(heading * speed * Time.deltaTime);
        }
    }
    public void EnableMove()
    {
        moving = true;
    }
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Inventory inventory = Inventory.GetInstance();
            if (inventory != null)
            {
                inventory.CollectLifeGem(Amount);
                DestroyItem();
            }
        }
    }
}
