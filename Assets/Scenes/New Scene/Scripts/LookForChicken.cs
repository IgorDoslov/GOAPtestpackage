using UnityEngine;
using GOAP;

public class LookForChicken : Action
{
    Vector3 wanderTarget = Vector3.zero;
    public bool keepWandering = true;
    public int targetChickenIndex;
    public GameObject targetChicken;
    public Wolf wolf;

    public float chaseRange = 7f;

    // called at the begining of this action
    public override bool OnActionEnter()
    {

        wolf = GetComponent<Wolf>();
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
        for (int i = 0; i < wolf.chickens.Count; i++)
        {
            // Check the distance to each chicken
            float dist = Vector3.Distance(transform.position, wolf.chickens[i].transform.position);

            // If chicken is in range
            if (dist < chaseRange)
            {
                // Select a chicken
                targetChickenIndex = i;
                if (!inventory.FindItemWithTag("Chicken"))
                {
                    inventory.AddItem(wolf.chickens[i].gameObject);
                    targetChicken = wolf.chickens[i].gameObject;
                }
                agentInternalState.AddInternalState("ChickenFound");
                agentInternalState.RemoveState("ChickenNotFound");
                keepWandering = false; // Stop wandering
                return true;
            }
            else
            {
                if (inventory.inventoryItems.Contains(wolf.chickens[i].gameObject))
                    inventory.RemoveItem(wolf.chickens[i].gameObject);
                agentInternalState.AddInternalState("ChickenNotFound");
                agentInternalState.RemoveState("ChickenFound");
                keepWandering = true;
            }
        }

        return false;


    }

    // Wander behaviour
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
