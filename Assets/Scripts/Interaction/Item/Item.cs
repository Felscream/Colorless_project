using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Interaction {
    [SerializeField]
    protected string itemName;

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
