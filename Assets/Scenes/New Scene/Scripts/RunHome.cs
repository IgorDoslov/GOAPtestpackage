using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class RunHome : Action
{
     // called at the begining of this action
    public override bool OnActionEnter()
    {
        return true;

    }

    // Action Update
    public override void OnActionUpdate()
    {

    }

    // The condition to exit the action
    public override bool ActionExitCondition()
    {
        return true;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        return true;
    }
}
