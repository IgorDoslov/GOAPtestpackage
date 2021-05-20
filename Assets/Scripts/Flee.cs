using UnityEngine;
using GOAP;

// Chicken flee behaviour
public class Flee : Action
{
    public GameObject wolf;
    Vector3 fleeTarget = Vector3.zero;
    public float fleeSpeed = 10;
    public float normalSpeed = 3.5f;
    public float distanceToWolf = 30.0f;
    public float distanceToHome = 30.0f;
    float dist = 0;

    // called at the begining of this action
    public override bool OnActionEnter()
    {
        navAgent.speed = fleeSpeed;
        return true;

    }

    public override void OnActionUpdate()
    {
        ChickenFlee();

    }

    public override bool ActionExitCondition()
    {
        if (wolf != null) // Check distance to wolf if it exists
            dist = Vector3.Distance(navAgent.transform.position, wolf.transform.position);
        // Exit the action if we are far away enough
        if (wolf == null || dist > distanceToWolf)
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        agentInternalState.RemoveState("Run");
        navAgent.speed = normalSpeed;

        return true;
    }

    // Make the chicken go in the opposite direction to the wolf but with random fluctuations to prevent always straight lines
    public void ChickenFlee()
    {
        if (wolf != null)
            destination = transform.position + ((transform.position - wolf.transform.position) * 8.2f);

        fleeTarget = new Vector3(Random.Range(0.1f, 1.0f) * destination.x, destination.y, Random.Range(0.1f, 1.0f) * destination.z);

        navAgent.SetDestination(fleeTarget);
    }
}
