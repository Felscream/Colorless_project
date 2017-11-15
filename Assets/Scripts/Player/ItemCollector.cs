using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour {
    private Inventory inventory;
        private Item item;
    public void Start()
    {
        inventory = Inventory.GetInstance();
    }
    public void OnTriggerEnter(Collider collider)
    {
        item = collider.GetComponent<Item>();
        if (item != null && inventory != null)
        {
            switch (item.GetType().ToString())
            {
                case "LifeGemItem":
                    LifeGemItem lifeGem = (LifeGemItem)item;
                    lifeGem.EnableMove();
                    break;
            }
        }

    }
}
