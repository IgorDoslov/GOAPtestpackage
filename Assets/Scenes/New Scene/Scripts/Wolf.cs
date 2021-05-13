using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Wolf : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;

    public float distanceToChicken = 30f;

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
        hungerTimer += Time.deltaTime;

        float dist = Vector3.Distance(transform.position, World.Instance.GetQueue("Chicken").queue.Peek().transform.position);

        if (hungerTimer >= hungerTime && !agentInternalState.HasState("Hungry"))
        {
            GetHungry();
            if (dist <= distanceToChicken)
            {
                agentInternalState.ModifyInternalState("ChickenFound");

            }
            else
            {
                agentInternalState.ModifyInternalState("ChickenNotFound");
                agentInternalState.RemoveState("ChickenFound");

            }
        }
    }

    void GetHungry()
    {
        agentInternalState.ModifyInternalState("Hungry");
        agentInternalState.RemoveState("SatisfyHunger");
    }
}
