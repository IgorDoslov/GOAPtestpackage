using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GoEat : Action
{
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = World.Instance.GetQueue("Food").RemoveResource().transform.gameObject;
        if (!inventory.FindItemWithTag("Food"))
            inventory.AddItem(target);
        if (target == null)
            return false;
        return true;

    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Enemy>().hungerTimer = 0;
        internalState.RemoveState("Hungry");
        internalState.ModifyInternalState("SatisfyHunger");
        World.Instance.GetQueue("Food").AddResource(target);
        inventory.RemoveItem(target);

        return true;
    }
}
