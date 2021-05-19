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

            // First node
            Node start = new Node(null, 0f, World.Instance.GetStateCollection().GetStateDictionary(), a_internalstates.GetStateDictionary(), null);

            // Pass in first node to build the graph
            bool success = BuildGraph(start, leaves, a_actions, a_goal);

            // If no plan found
            if (!success)
            {
                //Debug.Log("No Plan");
                return null;
            }

            Node cheapestLeaf = null;

            // Find the cheapest plan
            foreach (Node leaf in leaves)
            {
                // Make the first leaf the cheapest to be checked against the rest
                if (cheapestLeaf == null)
                    cheapestLeaf = leaf;
                else
                {
                    // If a cheaper path is found make it the cheapest path
                    if (leaf.cost < cheapestLeaf.cost)
                        cheapestLeaf = leaf;
                }
            }
            // final path
            List<Action> resultPlan = new List<Action>();

            while (cheapestLeaf != null)
            {
                if (cheapestLeaf.action != null)
                {
                    resultPlan.Insert(0, cheapestLeaf.action);
                }
                cheapestLeaf = cheapestLeaf.parent;
            }

            // A queue of actions
            Queue<Action> actionQueue = new Queue<Action>();

            foreach (Action a in resultPlan)
            {
                // Enqueue the actions from our plan
                actionQueue.Enqueue(a);
            }

            return actionQueue;
        }

        private bool BuildGraph(Node a_parent, List<Node> a_leaves, List<Action> a_possibleActions, Dictionary<string, int> a_goal)
        {
            bool foundPath = false;


            foreach (Action action in a_possibleActions)
            {
                // Check Preconditions
                if (action.IsActionAchievable(a_parent.stateDic))
                {
                    // The first time through the current state will be the state of the world.
                    // After the first time, it will be the next node of the current one
                    Dictionary<string, int> currentState = new Dictionary<string, int>(a_parent.stateDic);
                    // Add the effects
                    foreach (var effect in action.effectsDic)
                    {
                        if (!currentState.ContainsKey(effect.Key))
                            currentState.Add(effect.Key, effect.Value);
                    }

                    // Create the next node. It's parent is the current node. Cost accumulates as we move through the actions each recursion
                    Node node = new Node(a_parent, a_parent.cost + action.cost, currentState, action);

                    // Is the current state after the action the goal? If yes then a plan is found
                    if (GoalAchieved(a_goal, currentState))
                    {
                        a_leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        // Check the other actions for a plan if plan wasn't found
                        List<Action> subset = NewListOfPossibleActions(a_possibleActions, action); // prevents circular path by removing unusable action. 

                        // Build a new graph without the unusable action just removed above and go through the process again
                        bool found = BuildGraph(node, a_leaves, subset, a_goal);
                        if (found)
                            foundPath = true;
                    }
                }
            }
            return foundPath;
        }

        // Does the state of the world match the goal? If yes, then goal is achieved
        private bool GoalAchieved(Dictionary<string, int> a_goal, Dictionary<string, int> a_state)
        {
            // Check if the goal exists in the after effects
            foreach (KeyValuePair<string, int> g in a_goal)
            {
                if (!a_state.ContainsKey(g.Key))
                    return false;
            }
            return true; // goal found
        }

        // Remove unusable actions
        private List<Action> NewListOfPossibleActions(List<Action> a_actions, Action a_removeThisAction)
        {
            // New list of actions without the unusable action
            List<Action> newListOfPossibleActions = new List<Action>();

            foreach (Action a in a_actions)
            {
                // Add actions to the list excluding the unusable action
                if (!a.Equals(a_removeThisAction))
                    newListOfPossibleActions.Add(a);
            }
            // new list of possible actions with unusable action removed
            return newListOfPossibleActions;
        }


    }
}