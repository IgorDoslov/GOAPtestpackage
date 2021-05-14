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
        destination = transform.position + ((transform.position - wolf.transform.position) * 0.5f);
        navAgent.speed = 10.0f;
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
        navAgent.speed = 3.5f;

        return true;
    }
}
