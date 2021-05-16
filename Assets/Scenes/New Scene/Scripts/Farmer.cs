using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class Farmer : Agent
{
    public List<Food> food = new List<Food>();
    public List<Water> water = new List<Water>();

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

    //private void Update()
    //{

    //}

    
}
