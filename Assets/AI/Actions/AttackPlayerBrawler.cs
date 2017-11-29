using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class AttackPlayerBrawler : RAINAction
{
    Brawler self;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        self = ai.Body.GetComponent<Brawler>();
        
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if(self == null)
        {
            Debug.Log("Brawler class not found on " + ai.Body);
            return ActionResult.FAILURE;
        }
        else
        {
            self.StartCoroutine(self.Attack());
            return ActionResult.SUCCESS;
        }
        
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}