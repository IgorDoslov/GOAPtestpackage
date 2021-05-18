using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Node
    {
        // Parent node
        public Node parent;
        // Cost of travelling to this node
        public float cost;
        // State of the world
        public Dictionary<string, int> stateDic;
        // The action in the plan
        public Action action;

        // Constructor for just the world states
        public Node(Node a_parent, float a_cost, Dictionary<string, int> a_worldStates, Action a_action)
        {
            parent = a_parent;
            cost = a_cost;
            stateDic = new Dictionary<string, int>(a_worldStates);
            action = a_action;
        }

        // This constructor also takes in the internal states of the agent to be matched to preconditions
        public Node(Node a_parent, float a_cost, Dictionary<string, int> a_worldStates, Dictionary<string, int> a_internalStates, Action a_action)
        {
            parent = a_parent;
            cost = a_cost;
            stateDic = new Dictionary<string, int>(a_worldStates);
            // Adding the internal states of the agents
            foreach (KeyValuePair<string, int> interalState in a_internalStates)
            {
                if (!stateDic.ContainsKey(interalState.Key))
                    stateDic.Add(interalState.Key, interalState.Value);
            }
            action = a_action;
        }
    }
}