using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Enemy : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;

    public float thirstTime;
    [HideInInspector]
    public float thirstTimer;

    public GameObject player;

    new void Start()
    {
        base.Start();

    }

    private void Update()
    {

        hungerTimer += Time.deltaTime;
        thirstTimer += Time.deltaTime;



        float dist = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(dist);
        if (dist <= 20f)
        {
            if (!agentInternalState.HasState("Run"))
            {
                agentInternalState.ModifyInternalState("Run");
                StopAction();
                if (inventory.FindItemWithTag("Food"))
                {
                    World.Instance.GetQueue("Food").AddResource(inventory.FindItemWithTag("Food"));
                    inventory.RemoveItem(inventory.FindItemWithTag("Food"));
                }
                if (inventory.FindItemWithTag("Water"))
                {
                    World.Instance.GetQueue("Water").AddResource(inventory.FindItemWithTag("Water"));
                    inventory.RemoveItem(inventory.FindItemWithTag("Water"));
                }
            }
        }
        else
        {
            agentInternalState.RemoveState("Run");


            if (hungerTimer >= hungerTime && !agentInternalState.HasState("Hungry"))
            {
                GetHungry();
            }

            if (thirstTimer >= thirstTime && !agentInternalState.HasState("Thirsty"))
            {
                GetThirsty();
            }

        }
    }

    void GetHungry()
    {
        agentInternalState.ModifyInternalState("Hungry");
        agentInternalState.RemoveState("SatisfyHunger");
    }

    void GetThirsty()
    {
        agentInternalState.ModifyInternalState("Thirsty");
        agentInternalState.RemoveState("SatisfyThirst");

    }
}
