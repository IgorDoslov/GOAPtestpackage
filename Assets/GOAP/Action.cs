﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        [Tooltip("The name of the action")]
        public string actionName = "Action";
        [Tooltip("The cost of the action")]
        public float cost = 1.0f;
        [HideInInspector]
        public GameObject target;
        [HideInInspector]
        public Vector3 destination = Vector3.zero;
        [Tooltip("The preconditions required for the action to occur")]
        public State[] preConditions;
        [Tooltip("The effects after the action has occured")]
        public State[] afterEffects;
        [HideInInspector]
        public NavMeshAgent navAgent;
        public float duration = 0f;

        public Dictionary<string, int> preconditionsDic;
        public Dictionary<string, int> effectsDic;

        public Inventory inventory;

        public StateCollection agentInternalState;

        [HideInInspector]
        public bool running = false;

        public Action()
        {
            preconditionsDic = new Dictionary<string, int>();
            effectsDic = new Dictionary<string, int>();
        }

        public void Awake()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();

            if (preConditions != null)
                foreach (State w in preConditions)
                {
                    preconditionsDic.Add(w.key, w.value);
                }

            if (afterEffects != null)
                foreach (State w in afterEffects)
                {
                    effectsDic.Add(w.key, w.value);
                }

            inventory = GetComponent<Agent>().inventory;
            agentInternalState = GetComponent<Agent>().agentInternalState;
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> p in preconditionsDic)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool OnActionEnter();

        public abstract bool OnActionExit();

        public abstract void OnActionUpdate();

        public abstract bool ActionExitCondition();


    }
}