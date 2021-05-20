using UnityEngine;
using GOAP;

public class WolfFlee : Action
{
    public GameObject farmer;
    Vector3 fleeTarget = Vector3.zero;
    public float fleeSpeed = 10;
    public float normalSpeed = 6f;
    public float distanceToFarmer = 30.0f;
    public float distanceToHome = 30.0f;
    float dist = 0;

    // called at the begining of this action
    public override bool OnActionEnter()
    {
        navAgent.speed = fleeSpeed;
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {
        WFlee();
    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        if (farmer != null) // How far away is the farmer?
            dist = Vector3.Distance(navAgent.transform.position, farmer.transform.position);

        if (farmer == null || dist > distanceToFarmer) // Got away from the farmer
        {
            return true;
        }
        else if (dist < 2.1f) // Got caught by the farmer
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        // Return to normal speed after fleeing
        navAgent.speed = normalSpeed;
        agentInternalState.RemoveState("Run");

        return true;
    }

    // Move in the opposite direction to the farmer with some random movement to prevent only straight lines
    public void WFlee()
    {
        if (farmer != null)
            destination = transform.position + ((transform.position - farmer.transform.position) * 8.2f);

        fleeTarget = new Vector3(Random.Range(0.1f, 1.0f) * destination.x, destination.y, Random.Range(0.1f, 1.0f) * destination.z);

        navAgent.SetDestination(fleeTarget);
    }
}
