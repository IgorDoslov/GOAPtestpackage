using System.Collections;
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
        public GameObject target;   // Can be used to set a destination for the navMeshAgent
        [HideInInspector]
        public Vector3 destination = Vector3.zero;
        [Tooltip("The preconditions required for the action to occur")] // These two arrays are used so that
        public State[] preConditions;                                   // preconditions and after effects can
        [Tooltip("The effects after the action has occured")]           // be added in the inspector as Unity
        public State[] afterEffects;                                    // doesn't as yet let dictionaries be 
        [HideInInspector]                                               // exposed in the inspector
        public NavMeshAgent navAgent;
        public float duration = 0f; // How long the action should last

        public Dictionary<string, int> preconditionsDic;
        public Dictionary<string, int> effectsDic;

        // The agent's inventory
        public Inventory inventory;

        // The internal state of the agent
        public StateCollection agentInternalState;

        [HideInInspector]
        public bool running = false;    //Is the action running

        public Action()
        {
            preconditionsDic = new Dictionary<string, int>();
            effectsDic = new Dictionary<string, int>();
        }

        public void Awake()
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            // Add the preconditions to our preconditions dictionary
            if (preConditions != null)

                foreach (State w in preConditions)
                {
                    preconditionsDic.Add(w.key, w.value);
                }

            // Add the effects to our effects dictionary
            if (afterEffects != null)

                foreach (State w in afterEffects)
                {
                    effectsDic.Add(w.key, w.value);
                }

            // Agent's inventory
            inventory = GetComponent<Agent>().inventory;
            // Agent's internal state
            agentInternalState = GetComponent<Agent>().agentInternalState;
        }


        // Do the conditions passed in match all the preconditions? If yes, then the action is achievable.
        public bool IsActionAchievable(Dictionary<string, int> conditions)
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

        // On entering the action
        public abstract bool OnActionEnter();
        // On exiting the action
        public abstract bool OnActionExit();
        // While the action is occuring
        public abstract void OnActionUpdate();
        // The conditions to exit the action
        public abstract bool ActionExitCondition();


    }
}