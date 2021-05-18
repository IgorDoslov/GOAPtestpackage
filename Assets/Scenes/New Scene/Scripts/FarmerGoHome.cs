using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class FarmerGoHome : Action
{
    public Transform homePoint;
     // called at the begining of this action
    public override bool OnActionEnter()
    {

        navAgent.SetDestination(homePoint.position);
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {

    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        float dist = Vector3.Distance(transform.position, homePoint.position);
        if (dist < 2f)
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        agentInternalState.AddInternalState("IsHome");
        agentInternalState.RemoveState("CatchWolf");
        return true;
    }
}
