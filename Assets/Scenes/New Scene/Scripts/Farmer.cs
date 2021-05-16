using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Farmer : Agent
{
    public GameObject wolf;
    public float distanceToWolf = 30f;
    float dist = 0;
    new void Start()
    {
        base.Start();
        //example:
        // Invoke("FunctionName", Random.Range(10, 20));
    }

    //void FunctionName()
    //{
    //    agentInternalState.ModifyState("condition", 0);
    //}

    private void Update()
    {
        if (wolf != null)
            dist = Vector3.Distance(transform.position, wolf.transform.position);

        if (wolf != null && dist < distanceToWolf)
        {
            agentInternalState.ModifyInternalState("CanSeeWolf");
        }
        else
        {
            agentInternalState.RemoveState("CanSeeWolf");
        }
    }


}
