using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Flee : Action
{
    public GameObject wolf;
    
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        navAgent.speed = 10.0f;
        return true;

    }

    public override void OnActionUpdate()
    {
        destination = transform.position + ((transform.position - wolf.transform.position) * 2.5f);
        navAgent.SetDestination(destination);

    }

    public override bool ActionExitCondition()
    {

        float dist = Vector3.Distance(transform.position, wolf.transform.position);
        if (dist > 10.0f)
            return true;
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        navAgent.speed = 3.5f;

        return true;
    }
}
