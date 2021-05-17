using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Wolf : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;
    public GameObject home;
    public GameObject farmer;
    public float distanceToHome = 10f;
    public float distanceToFarmer = 30f;
    public float distToF = 0f;
    public List<GameObject> chickens = new List<GameObject>();
    private ChickensInScene cis = null;

    //public List<Chicken> chickens = new List<Chicken>();

    //public float distanceToChicken = 30f;
    //public float closestChickenDistance = Mathf.Infinity;
    //public GameObject closestChickenObj = null;

    new void Start()
    {
        cis = FindObjectOfType<ChickensInScene>();
        foreach (var c in cis.chicks)
        {
            chickens.Add(c);
        }
        base.Start();
        agentInternalState.AddInternalState("ChickenNotFound");


        //example:
        // Invoke("FunctionName", Random.Range(10, 20));
    }

    //void FunctionName()
    //{
    //    agentInternalState.ModifyState("condition", 0);
    //}

    private void Update()
    {
        hungerTimer += Time.deltaTime;

        if (farmer != null)
            distToF = Vector3.Distance(transform.position, farmer.transform.position);
        //float dist = Vector3.Distance(transform.position, home.transform.position);
        //if (dist < distanceToHome)
        //{
        //    agentInternalState.RemoveState("ChickenFound");
        //    agentInternalState.AddInternalState("ChickenNotFound");
        //}

        if (farmer != null && distToF <= distanceToFarmer)
        {
            if (!agentInternalState.HasState("Run"))
            {
                //if (dist < distanceToHome)
                //    agentInternalState.AddInternalState("CloseToHome");
                //else
                //    agentInternalState.RemoveState("CloseToHome");
                agentInternalState.AddInternalState("Run");
                StopAction();
                // put it back into the world
                if (inventory.FindItemWithTag("Chicken"))
                {
                    World.Instance.GetQueue("Chicken").AddResource(inventory.FindItemWithTag("Chicken"));
                    inventory.items.Clear();
                }

            }
        }
        else
        {
            agentInternalState.RemoveState("Run");


            //if (hungerTimer >= hungerTime && !agentInternalState.HasState("Hungry"))
            //{
            //    GetHungry();
            //}
        }

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
            Debug.Log("Wolf is hungry");
        }
    }

    public void WolfDie()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

}
