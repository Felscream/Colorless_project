using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGemItem : Item {
    private Inventory playerInventory;
    [SerializeField]
    protected string id, prefabName;
    [SerializeField]
    protected int amount = 0;

    [SerializeField]
    private int rotationX;
    [SerializeField]
    private int rotationY;
    [SerializeField]
    private int rotationZ;

    private Vector3 playerPosition;
    private bool moving = false;
    // Update is called once per frame

    private void Start()
    {
        playerPosition = Camera.main.transform.position;
    }
    void Update()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ) * Time.deltaTime);
        if (moving)
        {
            MoveTowardPlayer();
        }
    }
    public int GetAmount()
    {
        return amount;
    }

    public void MoveTowardPlayer()
    {
        if(playerPosition != null)
        {
            playerPosition = Camera.main.transform.position;
            Vector3 heading = (playerPosition - transform.position).normalized;
            transform.Translate(heading * 8 * Time.deltaTime);
        }
    }
    public void EnableMove()
    {
        moving = true;
    }
}
