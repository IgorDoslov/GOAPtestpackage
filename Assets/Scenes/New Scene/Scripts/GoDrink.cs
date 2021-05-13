using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GoDrink : Action
{
     // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = World.Instance.GetQueue("Water").RemoveResource().transform.gameObject;
        if (!inventory.FindItemWithTag("Water"))
            inventory.AddItem(target);
        if (target == null)
            return false;
        return true;

    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Enemy>().thirstTimer = 0;
        internalState.RemoveState("Thirsty");
        internalState.ModifyInternalState("SatisfyThirst");
        World.Instance.GetQueue("Water").AddResource(target);
        inventory.RemoveItem(target);

        return true;
    }
}
