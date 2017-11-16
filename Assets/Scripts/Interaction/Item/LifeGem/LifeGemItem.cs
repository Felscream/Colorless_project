using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGemItem : Item {
    private Inventory playerInventory;
    [SerializeField]
    private string id, prefabName;
    [SerializeField]
    private int amount = 0;
    [SerializeField]
    private float speed;

    private Vector3 playerPosition;
    private bool moving = false;
    // Update is called once per frame

    private void Start()
    {
        playerPosition = Player.GetInstance().transform.position;
    }
    void Update()
    {
        if (moving)
        {
            playerPosition = Player.GetInstance().transform.position;
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
            Vector3 heading = Vector3.Normalize((playerPosition - transform.position));
            transform.Translate(heading * speed * Time.deltaTime);
        }
    }
    public void EnableMove()
    {
        moving = true;
    }
}
