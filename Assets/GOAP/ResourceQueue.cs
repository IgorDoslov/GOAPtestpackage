using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP
{
    public class ResourceQueue
    {
        public Queue<GameObject> queue = new Queue<GameObject>();
        public string tag;
        public string modState;

        public ResourceQueue(string a_tag, string a_modState, StateCollection a_worldStates)
        {
            tag = a_tag;
            modState = a_modState;
            if (tag != "")
            {
                GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject r in resources)
                {
                    queue.Enqueue(r);
                }

                if (modState != "")
                {
                    a_worldStates.ModifyState(modState, queue.Count);
                }
            }
        }

        public void AddResource(GameObject a_resource)
        {
            queue.Enqueue(a_resource);
        }

        public void RemoveResource(GameObject a_resource)
        {
            // create a new queue and copy over values from the old queue, but leave out a_resource so we can remove it
            queue = new Queue<GameObject>(queue.Where(p => p != a_resource));
        }

        public GameObject RemoveResource()
        {
            if (queue.Count == 0) return null;
            return queue.Dequeue();
        }
    }
}