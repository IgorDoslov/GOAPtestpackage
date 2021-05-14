using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class LookForChicken : Action
{
    Vector3 wanderTarget = Vector3.zero;
    public bool keepWandering = true;
    public List<Chicken> chickens = new List<Chicken>();

    public float distanceToChicken = 30f;
    public float closestChickenDistance = Mathf.Infinity;
    public GameObject closestChickenObj = null;
    // called at the begining of this action
    public override bool OnActionEnter()
    {
        keepWandering = true;
        StartCoroutine(Wander());
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
        foreach (Chicken c in chickens)
        {
            float dist = Vector3.Distance(transform.position, c.transform.position);

            if (dist < closestChickenDistance)
            {
                closestChickenDistance = dist;
                closestChickenObj = c.gameObject;

                inventory.AddItem(closestChickenObj);
                closestChickenDistance = Mathf.Infinity;
                closestChickenObj = null;
                agentInternalState.ModifyInternalState("ChickenFound");
                agentInternalState.RemoveState("ChickenNotFound");

            }
            else
            {
                agentInternalState.ModifyInternalState("ChickenNotFound");
                agentInternalState.RemoveState("ChickenFound");
            }
            break;

        }
        
    }

    public override bool ActionExitCondition()
    {
        keepWandering = false;

        return true;
    }

    IEnumerator Wander()
    {
        while (keepWandering)
        {
            yield return new WaitForSeconds(3.0f);
            float wanderRadius = 40f;
            float wanderDistance = 20f;
            float wanderJitter = 9f;

            wanderTarget = new Vector3(Random.Range(0.1f, 1.0f) * wanderJitter, 0, Random.Range(0.1f, 1.0f) * wanderJitter);
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

            navAgent.SetDestination(targetWorld);
        }
    }
}
