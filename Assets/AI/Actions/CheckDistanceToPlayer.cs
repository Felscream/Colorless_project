using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class CheckDistanceToPlayer : RAINAction
{
    private GameObject player, self;
    private float distance;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        player = ai.WorkingMemory.GetItem<GameObject>("Player");
        self = ai.Body;
        Debug.Log(player +" "+ self);
        
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        distance = Vector3.Distance(self.transform.position, player.transform.position);
        ai.WorkingMemory.SetItem<float>("distanceToTarget", distance);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}