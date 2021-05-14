using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Chicken : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;

    public float thirstTime;
    [HideInInspector]
    public float thirstTimer;

    public GameObject wolf;
    public float distanceToWolf = 20f;

    new void Start()
    {
        base.Start();

    }

    private void Update()
    {

        hungerTimer += Time.deltaTime;
        thirstTimer += Time.deltaTime;



        float dist = Vector3.Distance(transform.position, wolf.transform.position);
        Debug.Log(dist);
        if (dist <= distanceToWolf)
        {
            if (!agentInternalState.HasState("Run"))
            {
                agentInternalState.ModifyInternalState("Run");
                StopAction();
                // put it back into the world
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

    public void ChickenDie()
    {
        World.Instance.GetQueue("Chicken").RemoveResource(gameObject);
        
        Destroy(gameObject);
    }
}
