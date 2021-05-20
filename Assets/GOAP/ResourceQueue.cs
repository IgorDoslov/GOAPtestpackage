using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP
{
    public class ResourceQueue
    {
        public Queue<GameObject> queue = new Queue<GameObject>();
        public string tag;
        public string state;

        // Constructor
        public ResourceQueue(string a_tag, string a_modState, StateCollection a_worldStates)
        {
            tag = a_tag;
            state = a_modState;
            
            if (tag != "")
            {
                // Find the gameobjects
                GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
                // Enqueue them
                foreach (GameObject r in resources)
                {
                    queue.Enqueue(r);
                }
                // If state isn't empty add it to the world states
                if (state != "")
                {
                    a_worldStates.ModifyState(state, queue.Count);
                }
            }
        }

        // Add a resource
        public void AddResource(GameObject a_resource)
        {
            queue.Enqueue(a_resource);
        }

        // Remove a gameobject from a resource queue
        public void RemoveResource(GameObject a_resource)
        {
            // create a new queue and copy over values from the old queue, but leave out a_resource so we can remove it
            queue = new Queue<GameObject>(queue.Where(p => p != a_resource));
        }

        // Remove a resource
        public GameObject RemoveResource()
        {
            if (queue.Count == 0) return null;
            return queue.Dequeue();
        }
    }
}