using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class WolfWandering : Action
{
    Vector3 wanderTarget = Vector3.zero;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        Wander();
        return true;

    }

    public override bool OnActionUpdate()
    {
        return true;
    }

    public override bool ActionExitCondition()
    {
        return true;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        return true;
    }


    void Wander()
    {
        float wanderRadius = 40f;
        float wanderDistance = 20f;
        float wanderJitter = 9f;

        wanderTarget = new Vector3(Random.Range(0.1f, 1.0f) * wanderJitter, 0, Random.Range(0.1f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        destination = targetWorld;
    }
}
