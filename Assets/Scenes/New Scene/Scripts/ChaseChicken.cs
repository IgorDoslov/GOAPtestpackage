using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChaseChicken : Action
{
    public float chaseSpeed = 10f;
    public float wanderSpeed = 5f;
    public bool chickenCaught = false;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = GetComponent<LookForChicken>().targetChicken;

        if (target == null)
            return false;
        chickenCaught = false;
        navAgent.speed = chaseSpeed;
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
            chickenCaught = true;
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
        agentInternalState.AddInternalState("ChickenNotFound");
        if (chickenCaught == true)
        {
            chickenCaught = false;
            agentInternalState.AddInternalState("CatchChicken");
            target.GetComponent<Chicken>().ChickenDie();
        }
        if (inventory.FindItemWithTag(target.tag))
            inventory.RemoveItem(target);
        GetComponent<LookForChicken>().wolf.chickens.RemoveAt(GetComponent<LookForChicken>().targetChickenIndex);

        return true;
    }
}
