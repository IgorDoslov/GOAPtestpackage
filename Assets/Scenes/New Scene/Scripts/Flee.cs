using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Flee : Action
{
    public GameObject wolf;
    Vector3 fleeTarget = Vector3.zero;
    public float fleeSpeed = 10;
    public float normalSpeed = 3.5f;
    public float distanceToWolf = 30.0f;
    public float distanceToHome = 30.0f;

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

        float dist = Vector3.Distance(transform.position, wolf.transform.position);
        if (dist > distanceToWolf)
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

    public void ChickenFlee()
    {

        destination = transform.position + ((transform.position - wolf.transform.position) * 8.2f);

        fleeTarget = new Vector3(Random.Range(0.1f, 1.0f) * destination.x, destination.y, Random.Range(0.1f, 1.0f) * destination.z);

        navAgent.SetDestination(fleeTarget);
    }
}
