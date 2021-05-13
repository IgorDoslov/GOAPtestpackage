using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP
{
    public class Planner
    {
        public Queue<Action> Plan(List<Action> a_actions, Dictionary<string, int> a_goal, StateCollection a_internalstates)
        {

            List<Node> leaves = new List<Node>();
            Node start = new Node(null, 0, World.Instance.GetStateCollection().GetStateDictionary(), a_internalstates.GetStateDictionary(), null);

            bool success = BuildGraph(start, leaves, a_actions, a_goal);

            if (!success)
            {
                Debug.Log("No Plan");
                return null;
            }

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null)
                    cheapest = leaf;
                else
                {
                    if (leaf.cost < cheapest.cost)
                        cheapest = leaf;
                }
            }

            List<Action> result = new List<Action>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }
                n = n.parent;
            }

            Queue<Action> queue = new Queue<Action>();
            foreach (Action a in result)
            {
                queue.Enqueue(a);
            }

            Debug.Log("The Plan is: ");
            foreach (Action a in queue)
            {
                Debug.Log("Q: " + a.actionName);
            }

            return queue;
        }

        private bool BuildGraph(Node a_parent, List<Node> a_leaves, List<Action> a_useableActions, Dictionary<string, int> a_goal)
        {
            bool foundPath = false;
            foreach (Action action in a_useableActions)
            {
                if (action.IsAchievableGiven(a_parent.state))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(a_parent.state);
                    foreach (KeyValuePair<string, int> effect in action.effectsDic)
                    {
                        if (!currentState.ContainsKey(effect.Key))
                            currentState.Add(effect.Key, effect.Value);
                    }

                    Node node = new Node(a_parent, a_parent.cost + action.cost, currentState, action);

                    if (GoalAchieved(a_goal, currentState))
                    {
                        a_leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<Action> subset = ActionSubset(a_useableActions, action); // prevents circular path
                        bool found = BuildGraph(node, a_leaves, subset, a_goal);
                        if (found)
                            foundPath = true;
                    }
                }
            }
            return foundPath;
        }


        private bool GoalAchieved(Dictionary<string, int> a_goal, Dictionary<string, int> a_state)
        {
            foreach (KeyValuePair<string, int> g in a_goal)
            {
                if (!a_state.ContainsKey(g.Key))
                    return false;
            }
            return true;
        }

        private List<Action> ActionSubset(List<Action> a_actions, Action a_removeMe)
        {
            List<Action> subset = new List<Action>();
            foreach (Action a in a_actions)
            {
                if (!a.Equals(a_removeMe))
                    subset.Add(a);
            }
            return subset;
        }


    }
}