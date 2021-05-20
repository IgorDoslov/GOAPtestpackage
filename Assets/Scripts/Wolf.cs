using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Wolf : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;
    public Transform resetPoint;
    public GameObject farmer;
    public float distanceToHome = 2f;
    public float distanceToFarmer = 30f;
    public float distToF = 0f; // Distance to farmer
    public List<GameObject> chickens = new List<GameObject>();


    new void Start()
    {

        base.Start();
        agentInternalState.AddInternalState("ChickenNotFound");

    }


    private void Update()
    {
        hungerTimer += Time.deltaTime;

        if (farmer != null)
            distToF = Vector3.Distance(transform.position, farmer.transform.position); // Distance to farmer

            if (farmer != null && distToF <= distanceToFarmer) // If close to farmer, run away
            {
                if (!agentInternalState.HasState("Run"))
                {

                    StopAction(); // Interrupt current action

                    agentInternalState.AddInternalState("Run"); // Flee state


                    // put it back into the world
                    if (inventory.FindItemWithTag("Chicken"))
                    {
                        World.Instance.GetQueue("Chicken").AddResource(inventory.FindItemWithTag("Chicken"));
                        inventory.inventoryItems.Clear();
                    }

                }
            }
            else
            {
                // Don't flee
                agentInternalState.RemoveState("Run");


            }

        // Make the wolf hungry
        if (!inventory.FindItemWithTag("Chicken"))
        {
            if (hungerTimer >= hungerTime)
            {
                GetHungry();
            }
        }
    }

    void GetHungry()
    {
        if (!agentInternalState.HasState("Hungry"))
        {
            agentInternalState.AddInternalState("Hungry");
        }
    }

    // Reset the wolf after getting caught by the farmer
    public void WolfReset()
    {
        if (gameObject != null)
        {
            gameObject.transform.position = resetPoint.position;
            currentAction.navAgent.velocity = Vector3.zero;
            StopAction();
            agentInternalState.states.Clear();
            agentInternalState.AddInternalState("CatchChicken");
            hungerTimer = 0;
        }
    }

}
