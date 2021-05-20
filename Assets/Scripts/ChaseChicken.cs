using UnityEngine;
using GOAP;

// Wolf chase chicken state
public class ChaseChicken : Action
{
    public float chaseSpeed = 10f;
    public float wanderSpeed = 5f;
    public bool chickenCaught = false;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        // Set chicken as destination
        target = GetComponent<LookForChicken>().targetChicken;

        if (target == null)
            return false;

        chickenCaught = false;
        navAgent.speed = chaseSpeed;
        return true;

    }

    public override void OnActionUpdate()
    {
        navAgent.SetDestination(target.transform.position);
    }

    public override bool ActionExitCondition()
    {
        // Distance to chicken
        float dist = Vector3.Distance(transform.position, target.transform.position);

        // If chicken caught
        if (dist < 2.0f)
        {
            chickenCaught = true;
            return true;
        }
        else
            return false;
    }

    // On exiting the state
    public override bool OnActionExit()
    {
        navAgent.speed = wanderSpeed;
        GetComponent<Wolf>().hungerTimer = 0;
        agentInternalState.RemoveState("Hungry");
        agentInternalState.RemoveState("ChickenFound");
        agentInternalState.AddInternalState("ChickenNotFound");

        if (chickenCaught == true)
        {
            chickenCaught = false;
            agentInternalState.AddInternalState("CatchChicken");
            target.GetComponent<Chicken>().ChickenDie();
        }
        // Remove from wolf's inventory
        if (inventory.FindItemWithTag(target.tag))
            inventory.RemoveItem(target);

        // remove the specific chicken
        GetComponent<LookForChicken>().wolf.chickens.RemoveAt(GetComponent<LookForChicken>().targetChickenIndex);

        return true;
    }
}
