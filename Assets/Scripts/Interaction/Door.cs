using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interaction {

    private Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Open()
    {
        this.interactive = false;
        animator.SetBool("open", true);
    }
}
