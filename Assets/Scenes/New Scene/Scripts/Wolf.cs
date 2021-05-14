using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Wolf : Agent
{
    public float hungerTime;
    [HideInInspector]
    public float hungerTimer;

    public List<Chicken> chickens = new List<Chicken>();

    public float distanceToChicken = 30f;
    public float closestChickenDistance = Mathf.Infinity;
    public GameObject closestChickenObj = null;

    new void Start()
    {
        base.Start();

        //example:
        // Invoke("FunctionName", Random.Range(10, 20));
    }

    //void FunctionName()
    //{
    //    agentInternalState.ModifyState("condition", 0);
    //}

    private void Update()
    {
        //hungerTimer += Time.deltaTime;

        //if (!inventory.FindItemWithTag("Chicken"))
        //{
        //    if (hungerTimer >= hungerTime)
        //    {
        //        GetHungry();

        //        foreach (Chicken c in chickens)
        //        {
        //            float dist = Vector3.Distance(transform.position, c.transform.position);

        //            if (dist < closestChickenDistance)
        //            {
        //                closestChickenDistance = dist;
        //                closestChickenObj = c.gameObject;
        //            }
        //        }

        //        if (closestChickenObj != null)
        //        {
        //            inventory.AddItem(closestChickenObj);
        //            closestChickenDistance = Mathf.Infinity;
        //            closestChickenObj = null;
        //            agentInternalState.ModifyInternalState("ChickenFound");
        //            agentInternalState.RemoveState("ChickenNotFound");

        //        }
        //    }
        //}
        //else
        //{
        //    agentInternalState.ModifyInternalState("ChickenNotFound");
        //    agentInternalState.RemoveState("ChickenFound");
        //}

    }

    void GetHungry()
    {
        if (!agentInternalState.HasState("Hungry"))
        {
            agentInternalState.ModifyInternalState("Hungry");
            Debug.Log("Wolf is hungry");
        }
    }

}
