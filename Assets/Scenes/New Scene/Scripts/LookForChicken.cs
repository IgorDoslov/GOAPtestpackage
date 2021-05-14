using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class LookForChicken : Action
{
    Vector3 wanderTarget = Vector3.zero;
    public bool keepWandering = true;
    public List<Chicken> chickens = new List<Chicken>();
    public int targetChickenIndex;

    public float chaseRange = 7f;

    // called at the begining of this action
    public override bool OnActionEnter()
    {

        keepWandering = true;
        //StartCoroutine(Wander());
        return true;

    }

    // On exiting the state
    public override bool OnActionExit()
    {
        keepWandering = false;
        return true;
    }


    public override void OnActionUpdate()
    {
        Wander();

    }

    public override bool ActionExitCondition()
    {
        for (int i = 0; i < chickens.Count; i++)
        {

            float dist = Vector3.Distance(transform.position, chickens[i].transform.position);

            if (dist < chaseRange)
            {
                targetChickenIndex = i;
                inventory.AddItem(chickens[i].gameObject);
                agentInternalState.ModifyInternalState("ChickenFound");
                agentInternalState.RemoveState("ChickenNotFound");
                keepWandering = false;
                return true;
            }
            else
            {
                agentInternalState.ModifyInternalState("ChickenNotFound");
                agentInternalState.RemoveState("ChickenFound");
            }
        }
        
        return false;


    }

    public void Wander()
    {
        //while (keepWandering)
        // {
        //yield return new WaitForSeconds(3.0f);
        float wanderRadius = 40f;
        float wanderDistance = 20f;
        float wanderJitter = 9f;

        wanderTarget = new Vector3(Random.Range(0.1f, 1.0f) * wanderJitter, 0, Random.Range(0.1f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        navAgent.SetDestination(targetWorld);
        //  }
    }
}
