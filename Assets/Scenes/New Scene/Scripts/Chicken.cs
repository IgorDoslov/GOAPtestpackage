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
    public GameObject home;
    public float distanceToWolf = 30f;
    public float distanceToHome = 50;
    float distToWlf = 0;

    new void Start()
    {

        base.Start();

    }

    private void Update()
    {

        hungerTimer += Time.deltaTime;
        thirstTimer += Time.deltaTime;

        if (wolf != null)
            distToWlf = Vector3.Distance(transform.position, wolf.transform.position); // Check distance to wolf
        float distToHome = Vector3.Distance(transform.position, home.transform.position); // Check distance to home

        if (wolf!= null && distToWlf <= distanceToWolf) // If I can see the wolf
        {
            if (!agentInternalState.HasState("Run"))
            {
                if (distToHome < distanceToHome)
                    agentInternalState.ModifyState("CloseToHome", 1); // If I'm close to home make me run there
                else
                    agentInternalState.ModifyState("CloseToHome", -1); // If not, run away
                agentInternalState.AddInternalState("Run"); // Start fleeing
                StopAction(); // Interrupt current action

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

            // Get hungry
            if (hungerTimer >= hungerTime && !agentInternalState.HasState("Hungry"))
            {
                GetHungry();
            }
            // Get thirsty
            if (thirstTimer >= thirstTime && !agentInternalState.HasState("Thirsty"))
            {
                GetThirsty();
            }

        }
        //if (World.Instance.GetQueue("Food").queue.Count > 0)
        //{
        //    agentInternalState.ModifyInternalState("CanSeeFood");
        //    agentInternalState.RemoveState("CantSeeFood");

        //}
        //else
        //{
        //    agentInternalState.RemoveState("CanSeeFood");
        //    agentInternalState.ModifyInternalState("CantSeeFood");

        //}
    }

    void GetHungry()
    {
        agentInternalState.AddInternalState("Hungry");
        agentInternalState.RemoveState("SatisfyHunger");
    }

    void GetThirsty()
    {
        agentInternalState.AddInternalState("Thirsty");
        agentInternalState.RemoveState("SatisfyThirst");

    }

    // Kill the chicken when wolf catches it
    public void ChickenDie()
    {
        if (gameObject != null)
        {
            World.Instance.GetQueue("Chicken").RemoveResource(gameObject);

            Destroy(gameObject);
        }
    }
}
