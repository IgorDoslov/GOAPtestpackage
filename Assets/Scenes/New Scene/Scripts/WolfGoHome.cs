using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class WolfGoHome : Action
{
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = GameObject.FindGameObjectWithTag("WolfHome");
        if (target == null)
            return false;
        navAgent.SetDestination(target.transform.position);
        if (!agentInternalState.HasState("ChickenNotFound"))
            agentInternalState.AddState("ChickenNotFound", 1);
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {

    }

    // The condition to exit the action
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
        GetComponent<Wolf>().hungerTimer = 0;
        agentInternalState.RemoveState("CatchChicken");
        agentInternalState.RemoveState("ChickenFound");
        return true;
    }
}
