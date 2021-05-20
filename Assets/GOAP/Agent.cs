
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace GOAP
{
    public class SubGoal
    {
        // our goals
        public Dictionary<string, int> subGoals;
        // should it be removed uopn completion? leave false to repeat goals
        public bool remove;

        public SubGoal(string name, int value, bool shouldRemove)
        {
            subGoals = new Dictionary<string, int>();
            subGoals.Add(name, value);
            remove = shouldRemove;
        }
    }
    // To enter our goals in the inspector
    [System.Serializable]
    public class Goal
    {
        // Agent's goal
        public string goal;
        public int value;
        [Tooltip("Should the goal be removed once completed. Leave unticked to repeat the goal forever")]
        public bool shouldRemove;
        [Tooltip("The higher the number the higher the priority")]
        public int priority;
    }

    public class Agent : MonoBehaviour
    {
        // The agent's actions
        [HideInInspector]
        public List<Action> actions = new List<Action>();
        // The agent's subgoals
        public Dictionary<SubGoal, int> goalsDic = new Dictionary<SubGoal, int>();
        // The agent's inventory
        public Inventory inventory = new Inventory();
        // The agent's internal state
        public StateCollection agentInternalState = new StateCollection();

        [Tooltip("How far from the target does the agent need to be to complete the goal")]
        public float distanceToTargetThreshold = 2f;

        // For adding goals in the inspector
        public Goal[] personalGoals;

        Planner planner;
        
        Queue<Action> actionQueue;

        public Action currentAction;

        SubGoal currentGoal;

        // For completing the action
        protected float durationTimer = 0f;
        public bool durationFinished = false;

        //used to create a list to display our current plan of actions to get to goal.
        [HideInInspector] public List<Action> actionPlan = new List<Action>();

        // Start is called before the first frame update
        public void Start()
        {
            Action[] acts = GetComponents<Action>();
            foreach (Action a in acts)
                actions.Add(a);

            for (int i = 0; i < personalGoals.Length; i++)
            {
                Goal g = personalGoals[i];
                SubGoal sg = new SubGoal(g.goal, g.value, g.shouldRemove);
                goalsDic.Add(sg, g.priority);
            }
        }

        // Allows the agent to complete an action
        void CompleteAction()
        {
            if (currentAction != null)
            {
                durationFinished = false;
                currentAction.running = false;
                currentAction.OnActionExit();
            }

        }

        // Update is called once per frame
        void LateUpdate()
        {
            // If the current is not null and it is running
            if (currentAction != null && currentAction.running)
            {
                // Check the exit condition of the current action
                if (currentAction.ActionExitCondition())
                {
                    // Finish the action after the duration
                    Duration();
                    // If the exit condition is true then complete the action
                    if (durationFinished)
                        CompleteAction();

                    return;

                }

                // If the action is still running, then run its update loop
                if (currentAction.running)
                {
                    currentAction.OnActionUpdate();
                }
                else
                    planner = null; // Get a new plan
                return;
            }


            if (planner == null || actionQueue == null)
            {
                planner = new Planner();

                // Sort the goals
                var sortedGoals = from entry in goalsDic orderby entry.Value descending select entry;
               
                // Find an achievable plan
                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();
                    actionQueue = planner.Plan(actions, sg.Key.subGoals, agentInternalState); // trying to create a plan for the most important goal
                    stopwatch.Stop();
                    Debug.Log("Time taken: " + (stopwatch.Elapsed));
                    stopwatch.Reset();
                    // If there is a plan
                    if (actionQueue != null)
                    {
                        // Assign the goal
                        currentGoal = sg.Key;
                        break;
                    }
                }
            }

            // for debugging or use system.linq instead. Remove this maybe
            //actionPlan.Clear();
            //if (actionQueue != null)
            //{
            //    foreach (Action a in actionQueue)
            //    {
            //        actionPlan.Add(a);
            //    }
            //}


            if (actionQueue != null && actionQueue.Count == 0)
            {
                // If the current goal should be removed; do we want the goal to repeat or only happen once?
                if (currentGoal.remove)
                {
                    // If yes, then remove it
                    goalsDic.Remove(currentGoal);
                }
                planner = null; // get a new plan
            }

            // Are there still actions?
            if (actionQueue != null && actionQueue.Count > 0)
            {
                // Make the first action current action
                currentAction = actionQueue.Dequeue();

                if (currentAction.OnActionEnter())
                {
                    // reset
                    durationTimer = 0f;
                    durationFinished = false;
                    // current action is running
                    currentAction.running = true;
                }
                else
                {
                    actionQueue = null; // go get a new plan
                }
            }

        }

        // To interrupt an action
        public void StopAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                currentAction = null;
                actionQueue = null;
            }
        }

        // The duration of the action
        public void Duration()
        {
            durationTimer += Time.deltaTime;

            if (durationTimer > currentAction.duration)
            {
                durationTimer = 0f;
                durationFinished = true;
            }
        }
    }
}