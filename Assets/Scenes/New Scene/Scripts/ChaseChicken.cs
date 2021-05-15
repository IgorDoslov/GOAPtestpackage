using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChaseChicken : Action
{
    public float chaseSpeed = 10f;
    public float wanderSpeed = 5f;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = inventory.FindItemWithTag("Chicken");
        navAgent.speed = chaseSpeed;
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
        navAgent.speed = wanderSpeed;
        GetComponent<Wolf>().hungerTimer = 0;
        agentInternalState.RemoveState("Hungry");
        agentInternalState.RemoveState("ChickenFound");
        agentInternalState.ModifyInternalState("ChickenNotFound");
        agentInternalState.ModifyInternalState("CatchChicken");
        GetComponent<LookForChicken>().chickens.RemoveAt(GetComponent<LookForChicken>().targetChickenIndex);
        inventory.RemoveItem(target);
        target.GetComponent<Chicken>().ChickenDie();

        return true;
    }
}
