using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GOAP
{
    [System.Serializable]
    public class State
    {
        public string key;
        public int value;
    }

    public class StateCollection
    {
        public Dictionary<string, int> states;

        public StateCollection()
        {
            states = new Dictionary<string, int>();
        }

        public bool HasState(string key)
        {
            return states.ContainsKey(key);
        }

        public void AddState(string key, int value)
        {
            states.Add(key, value);
        }

        public void ModifyState(string key, int value)
        {
            if (states.ContainsKey(key))
            {
                states[key] += value;
                if (states[key] <= 0)
                    RemoveState(key);
            }
            else
                states.Add(key, value);
        }

        public void ModifyInternalState(string key)
        {
            if (states.ContainsKey(key))
            {
                states[key] += 0;
                if (states[key] <= 0)
                    RemoveState(key);
            }
            else
                states.Add(key, 0);
        }

        public void RemoveState(string key)
        {
            if (states.ContainsKey(key))
                states.Remove(key);
        }

        public void SetState(string key, int value)
        {
            if (states.ContainsKey(key))
                states[key] = value;
            else
                states.Add(key, value);
        }

        public Dictionary<string, int> GetStateDictionary()
        {
            return states;
        }
    }
}