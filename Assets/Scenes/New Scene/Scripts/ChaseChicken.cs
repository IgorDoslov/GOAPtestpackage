using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChaseChicken : Action
{
     // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = World.Instance.GetQueue("Chicken").RemoveResource().transform.gameObject;
        if (!inventory.FindItemWithTag("Chicken"))
            inventory.AddItem(target);
        if (target == null)
            return false;
        return true;

    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Wolf>().hungerTimer = 0;
        internalState.RemoveState("Hungry");
        internalState.ModifyInternalState("SatisfyHunger");
        World.Instance.GetQueue("Chicken").AddResource(target);
        inventory.RemoveItem(target);
        return true;
    }
}
