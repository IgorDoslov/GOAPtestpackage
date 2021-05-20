using UnityEngine;
using GOAP;

public class Farmer : Agent
{
    public GameObject wolf;
    public float distanceToWolf = 30f;
    float dist = 0;
    new void Start()
    {
        agentInternalState.AddInternalState("CantSeeWolf");
        agentInternalState.AddInternalState("IsHome");
        base.Start();

    }


    private void Update()
    {
        if (wolf != null) // Check distance to wolf if it exists
            dist = Vector3.Distance(transform.position, wolf.transform.position);

        // If I haven't caught the wolf. Prevents catching the wolf after it resets to back at home
        if (!agentInternalState.HasState("CatchWolf"))
            if (wolf != null && dist < distanceToWolf)
            {
                // Can see the wolf
                agentInternalState.RemoveState("CantSeeWolf");
                agentInternalState.RemoveState("IsHome");
                agentInternalState.AddInternalState("CanSeeWolf");
            }
    }
}
