using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
[RAINAction]

public class CheckPlayerHealth : RAINAction
{
    private Player player;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        player = Player.GetInstance();
        if(player == null)
        {
            Debug.Log("Enemies can't find any player");
        }
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        ai.WorkingMemory.SetItem<bool>("PlayerAlive", true);
        if (player.IsDead())
        {
            ai.WorkingMemory.SetItem<bool>("PlayerAlive", false);
        }
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}