using UnityEngine;
using GOAP;

// Makes the farmer return home after catching the wolf
public class FarmerGoHome : Action
{
    public Transform homePoint;
     // called at the begining of this action
    public override bool OnActionEnter()
    {
        // Set destination to be home
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
        // Is farmer close to home?
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
