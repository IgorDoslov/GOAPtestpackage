using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Inventory
    {
        // Inventory list
        public List<GameObject> inventoryItems = new List<GameObject>();

        // Add an item to the inventory
        public void AddItem(GameObject item)
        {
            inventoryItems.Add(item);
        }

        // Removes an item from the inventory
        public void RemoveItem(GameObject item)
        {
            int indexToRemove = -1;
            foreach (GameObject gO in inventoryItems)
            {
                indexToRemove++;
                if (gO == item)
                    break;
            }
            if (indexToRemove >= -1)
                inventoryItems.RemoveAt(indexToRemove);
        }

        // Finds an item in the inventory using tags
        public GameObject FindItemWithTag(string tag)
        {
            // Go through each object
            foreach (GameObject item in inventoryItems)
            {
                // if empty
                if (item == null) break;
                // if found
                if (item.tag == tag)
                {
                    return item;
                }
            }
            // if not found
            return null;
        }

    }
}