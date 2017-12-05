using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interaction {

    private Animator animator = null;
	[SerializeField]
	private List<Door> connectedDoors;

	private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Open()
    {

		if (interactive)
		{
			interactive = false;
			animator.SetBool("open", true);
			foreach (Door door in connectedDoors)
			{
				door.interactive = false;
				door.animator.SetBool("open", true);

			}
		}
    }


}

