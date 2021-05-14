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

    public override void OnActionUpdate()
    {
        navAgent.SetDestination(target.transform.position);
        
    }

    public override bool ActionExitCondition()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 2.0f)
        { 
            return true; 
        }
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        GetComponent<Wolf>().hungerTimer = 0;
        agentInternalState.RemoveState("Hungry");
        agentInternalState.RemoveState("ChickenFound");
        agentInternalState.ModifyInternalState("ChickenNotFound");
        inventory.RemoveItem(target);
        return true;
    }
}
