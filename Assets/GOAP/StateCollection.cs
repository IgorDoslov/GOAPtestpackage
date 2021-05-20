using System.Collections.Generic;


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
        
        // Check if an agent has a state
        public bool HasState(string key)
        {
            return states.ContainsKey(key);
        }

        // Add a state with a value
        public void AddState(string key, int value)
        {
            states.Add(key, value);
        }

        // Add a state without a value
        public void AddInternalState(string key)
        {
            if (!states.ContainsKey(key))
                states.Add(key, 0);
        }

        // Add a state. If the value is less than 0 then remove it.
        // Can use to add or remove
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


        // Remove a state if it exists
        public void RemoveState(string key)
        {
            if (states.ContainsKey(key))
                states.Remove(key);
        }

        // Set a state if it exists
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