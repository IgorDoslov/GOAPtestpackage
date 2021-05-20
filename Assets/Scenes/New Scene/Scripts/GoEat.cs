using UnityEngine;
using GOAP;

public class GoEat : Action
{
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        // If there is food
        if (World.Instance.GetQueue("Food").queue.Count > 0)
            target = World.Instance.GetQueue("Food").RemoveResource().transform.gameObject; // Target it
        if (target == null)
            return false;
        inventory.AddItem(target);
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
        // Check distance to food
        float dist = Vector3.Distance(transform.position, target.transform.position);
        // if within eating distance
        if (dist < 2.0f)
            return true;
        else
            return false;

    }

    // On exiting the state
    public override bool OnActionExit()
    {
        // Reset hunger
        GetComponent<Chicken>().hungerTimer = 0;
        agentInternalState.RemoveState("Hungry");
        agentInternalState.ModifyState("SatisfyHunger", 1);
        World.Instance.GetQueue("Food").AddResource(target); // remove after wander working
        inventory.RemoveItem(target);

        //if (target.GetComponent<Food>().foodAmount != 0)
        //{
        //    target.GetComponent<Food>().foodAmount -= 1;
        //    World.Instance.GetQueue("Food").AddResource(target);
        //}
        //else
        //{
        //    World.Instance.GetQueue("Food").RemoveResource(target);
        //}



        return true;
    }
}
