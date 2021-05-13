using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Inventory
    {
        public List<GameObject> items = new List<GameObject>();

        public void AddItem(GameObject item)
        {
            items.Add(item);
        }

        public GameObject FindItemWithTag(string tag)
        {
            foreach (GameObject item in items)
            {
                if (item == null) break;
                if (item.tag == tag)
                {
                    return item;
                }
            }
            return null;
        }

        public void RemoveItem(GameObject item)
        {
            int indexToRemove = -1;
            foreach (GameObject i in items)
            {
                indexToRemove++;
                if (i == item)
                    break;
            }
            if (indexToRemove >= -1)
                items.RemoveAt(indexToRemove);
        }
    }
}