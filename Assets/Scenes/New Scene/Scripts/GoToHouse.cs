using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GoToHouse : Action
{

    public float fleeSpeed = 10;
    public float normalSpeed = 3.5f;

    // called at the begining of this action
    public override bool OnActionEnter()
    {
        target = GameObject.FindGameObjectWithTag("Home");
        if (target == null)
            return false;
        navAgent.speed = fleeSpeed;
        navAgent.SetDestination(target.transform.position);
        return true;


    }

    // Action Update
    public override void OnActionUpdate()
    {

    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 2.0f)
        {
            navAgent.speed = normalSpeed;
            return true;
        }
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        return true;
    }
}
