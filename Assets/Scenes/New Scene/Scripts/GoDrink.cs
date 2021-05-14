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
        if (target == null)
            return false;
        navAgent.SetDestination(target.transform.position);
        return true;

    }

    public override void OnActionUpdate()
    {

    }

    public override bool ActionExitCondition()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 2.0f)

            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Chicken>().thirstTimer = 0;
        agentInternalState.RemoveState("Thirsty");
        agentInternalState.ModifyInternalState("SatisfyThirst");
        World.Instance.GetQueue("Water").AddResource(target);
        inventory.RemoveItem(target);

        return true;
    }

}
