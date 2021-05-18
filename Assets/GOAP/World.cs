﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GOAP;

namespace GOAP
{

    public sealed class World : MonoBehaviour
    {
        [System.Serializable]
        public class InspectorResource
        {
            [TagSelector]
            public string tag;
            public string objectQueue;
        }

        private static StateCollection worldStates;

        // The resources entered in the inspector to be added to the resources dictionary
        public List<InspectorResource> resourceConfig = new List<InspectorResource>();

        // resources dictionary
        private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();


        public void Start()
        {
            worldStates = new StateCollection();

            // add the resources entered in the inspector to the resources dictionary
            foreach (var r in resourceConfig)
            {
                var res = new ResourceQueue(r.tag,r.objectQueue, worldStates);
                resources.Add(r.tag, res);
            }

        }

        void Awake()
        {

            if (Instance == null)
            {

                Instance = this;
                DontDestroyOnLoad(gameObject);

                //Rest of your Awake code
            }
            else
            {
                Destroy(this);
            }
        }

        public ResourceQueue GetQueue(string type)
        {
            return resources[type];
        }

        public static World Instance { get; private set; }

        public StateCollection GetStateCollection()
        {
            return worldStates;
        }
        
    }
}