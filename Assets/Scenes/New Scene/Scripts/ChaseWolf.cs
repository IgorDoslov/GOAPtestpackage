using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChaseWolf : Action
{
    public GameObject wolf;
    public float chaseSpeed = 15;
    public float normalSpeed = 3.5f;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        if (wolf == null)
            return false;
        navAgent.speed = chaseSpeed;
        navAgent.SetDestination(wolf.transform.position);
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {
        navAgent.SetDestination(wolf.transform.position);

    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        float dist = Vector3.Distance(transform.position, wolf.transform.position);
        if (dist < 2.0f)
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        navAgent.speed = normalSpeed;
        agentInternalState.RemoveState("CanSeeWolf");
        wolf.GetComponent<Wolf>().WolfDie();
        return true;
    }
}
