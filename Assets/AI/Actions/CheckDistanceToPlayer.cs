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
    private float lastAttack;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        player = ai.WorkingMemory.GetItem<GameObject>("Player");
        self = ai.Body;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        distance = Vector3.Distance(self.transform.position, player.transform.position);
        lastAttack = self.GetComponent<Brawler>().GetLastAttackTime();
        
        ai.WorkingMemory.SetItem<float>("distanceToTarget", distance);
        ai.WorkingMemory.SetItem<float>("lastAttack", lastAttack);
        ai.WorkingMemory.SetItem<float>("currentTime", Time.time);
        if(ai.WorkingMemory.GetItem<float>("currentTime") - ai.WorkingMemory.GetItem<float>("lastAttack") > ai.WorkingMemory.GetItem<float>("AttackReload"))
        {
            ai.WorkingMemory.SetItem("Attacking", true);
        }
        else
        {
            ai.WorkingMemory.SetItem("Attacking", false);
        }
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}