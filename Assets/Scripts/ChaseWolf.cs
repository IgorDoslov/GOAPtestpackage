using UnityEngine;
using GOAP;

// Farmer chases the wolf behaviour
public class ChaseWolf : Action
{
    public GameObject wolf;
    public float chaseSpeed = 15;
    public float normalSpeed = 3.5f;
    public float distanceToWolf = 40f;
    public bool wolfCaught = false;
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
        // Distance to wolf
        float dist = Vector3.Distance(transform.position, wolf.transform.position);

        // If farmer caught the wolf
        if (dist < 2.0f)
        {
            wolfCaught = true;
            return true;
        }
        // If wolf got away
        else if (dist > distanceToWolf)
        {
            return true;
        }
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        navAgent.speed = normalSpeed;

        agentInternalState.AddInternalState("CantSeeWolf");
        agentInternalState.RemoveState("CanSeeWolf");
      
        if (wolfCaught)
        {
            // Reset the wolf if the wolf was cuaght
            agentInternalState.AddInternalState("CatchWolf");
            wolf.GetComponent<Wolf>().WolfReset();
            wolfCaught = false;
        }
        return true;
    }
}
