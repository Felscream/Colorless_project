using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour {
    [SerializeField]
    protected string interactionName;
    [SerializeField]
    protected bool interactive = false;

    public bool IsInteractive()
    {
        return interactive;
    }
    public string GetItemName()
    {
        return interactionName;
    }
}
