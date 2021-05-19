using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class ChickenWander : Action
{
    Vector3 wanderTarget = Vector3.zero;
    public float wanderSpeed = 1f;
    public float normalSpeed = 3.5f;

    // called at the begining of this action
    public override bool OnActionEnter()
    {
        navAgent.speed = wanderSpeed;
        return true;
    }

    // Action Update
    public override void OnActionUpdate()
    {
        Wander();
    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        // The chicken stops wandering if it can see the wolf, is hungry or is thirsty
        if (agentInternalState.HasState("Run") || (agentInternalState.HasState("Hungry") && agentInternalState.HasState("Thirsty")))
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        navAgent.speed = normalSpeed;

        return true;
    }

    // Wander in random direction
    public void Wander()
    {
        float wanderRadius = 20f;
        float wanderDistance = 1f;
        float wanderJitter = 9f;

        wanderTarget = new Vector3(Random.Range(0.1f, 1.0f) * wanderJitter, 0, Random.Range(0.1f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        navAgent.SetDestination(targetWorld);
       
    }
}
