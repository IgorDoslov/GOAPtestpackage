using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChaseChicken : Action
{
     // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = inventory.FindItemWithTag("Chicken");
        if (target == null)
            return false;
        return true;

    }

    public override bool OnActionUpdate()
    {
        return true;
    }

    public override bool ActionExitCondition()
    {
        return true;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Wolf>().hungerTimer = 0;
        internalState.RemoveState("Hungry");
        inventory.RemoveItem(target);
        return true;
    }
}
