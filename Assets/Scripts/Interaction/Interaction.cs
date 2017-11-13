using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    [SerializeField]
    protected string interactionName;

    public string GetItemName()
    {
        return interactionName;
    }
}
