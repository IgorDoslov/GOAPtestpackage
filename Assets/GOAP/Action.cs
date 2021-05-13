using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public GameObject target;
        public Vector3 destination = Vector3.zero;
        [TagSelector]
        public string targetTag;
        public float duration = 0;
        public State[] preConditions;
        public State[] afterEffects;
        public NavMeshAgent navAgent;

        public Dictionary<string, int> preconditionsDic;
        public Dictionary<string, int> effectsDic;

        public Inventory inventory;

        public StateCollection internalState;

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
            internalState = GetComponent<Agent>().agentInternalState;
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


    }
}