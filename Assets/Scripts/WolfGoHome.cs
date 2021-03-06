using UnityEngine;
using GOAP;

// Makes the wolf go home after catching a chicken
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
            agentInternalState.AddInternalState("ChickenNotFound");
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {

    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        // Is the wolf close enough to catch the chicken
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
