using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Patrol : Action
{
    public Transform[] patrolPoints;
    private int destinationPoint = 0;

    // called at the begining of this action
    public override bool OnActionEnter()
    {
        if (patrolPoints == null)
        {
            return false;
        }
        else
        {
            navAgent.autoBraking = false;
            GotoNextPoint();
            return true;
        }
    }

    // Action Update
    public override void OnActionUpdate()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        if (!agentInternalState.HasState("CantSeeWolf"))
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        return true;
    }

    public void GotoNextPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        navAgent.destination = patrolPoints[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % patrolPoints.Length;
    }
}
