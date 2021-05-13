using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<string, int> state; // world state
        public Action action;

        public Node(Node a_parent, float a_cost, Dictionary<string, int> a_worldStates, Action a_action)
        {
            parent = a_parent;
            cost = a_cost;
            state = new Dictionary<string, int>(a_worldStates);
            action = a_action;
        }

        public Node(Node a_parent, float a_cost, Dictionary<string, int> a_worldStates, Dictionary<string, int> a_internalStates, Action a_action)
        {
            parent = a_parent;
            cost = a_cost;
            state = new Dictionary<string, int>(a_worldStates);
            foreach (KeyValuePair<string, int> interalState in a_internalStates)
            {
                if (!state.ContainsKey(interalState.Key))
                    state.Add(interalState.Key, interalState.Value);
            }
            action = a_action;
        }
    }
}