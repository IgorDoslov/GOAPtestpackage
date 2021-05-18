﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        [HideInInspector]
        public List<Action> actions = new List<Action>();
        public Dictionary<SubGoal, int> goalsDic = new Dictionary<SubGoal, int>();
        public Inventory inventory = new Inventory();
        public StateCollection agentInternalState = new StateCollection();
        [Tooltip("How far from the target does the agent need to be to complete the goal")]
        public float distanceToTargetThreshold = 2f;
        public Goal[] myGoals;
        Planner planner;
        Queue<Action> actionQueue;
        public Action currentAction;
        SubGoal currentGoal;

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
            for (int i = 0; i < myGoals.Length; i++)
            {
                Goal g = myGoals[i];
                SubGoal sg = new SubGoal(g.goal, g.value, g.shouldRemove);
                goalsDic.Add(sg, g.priority);
            }
        }

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
            if (currentAction != null && currentAction.running)
            {

                if (currentAction.ActionExitCondition())
                {
                    Duration();

                    if (durationFinished)
                        CompleteAction();

                    return;

                }

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

                var sortedGoals = from entry in goalsDic orderby entry.Value descending select entry;

                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.Plan(actions, sg.Key.subGoals, agentInternalState); // trying to create a plan for the most important goal
                    if (actionQueue != null)
                    {
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
                if (currentGoal.remove)
                {
                    goalsDic.Remove(currentGoal);
                }
                planner = null; // get new plan
            }
            // sets our action and gets the target
            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
                if (currentAction.OnActionEnter())
                {
                    durationTimer = 0f;
                    durationFinished = false;
                    currentAction.running = true;
                }
                else
                {
                    actionQueue = null; // go get a new plan
                }
            }

        }

        public void StopAction()
        {
            if (currentAction != null)
            {
                currentAction.running = false;
                currentAction = null;
                actionQueue = null;
            }
        }

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