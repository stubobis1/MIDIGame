using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace OnRails
{
    public class OnRailsVehicle : MonoBehaviour
    {
        public GameObject waypoints;
        public float Speed = 0.2f;
        public Rigidbody body;
        public bool debug = false;

        private List<Transform> targets;
        private int targetIndex;
        
        //private Rigidbody body;

        // Start is called before the first frame update
        void Start()
        {
            targets = waypoints.GetComponentInChildren<Waypoints>().points;
            if (body == null)
            { 
                body = GetComponent<Rigidbody>();
                if (body == null)
                {
                    Console.Error.WriteLine("NO BODY FOR THE ONRAILS PLAYER");
                }
            }

        }

        // Update is called once per frame
        bool changedTargets;
        void Update()
        {
            if (changedTargets)
            {
                changedTargets= false;
               
            }
            transform.LookAt(targets[targetIndex].transform);
            //body.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
            transform.position = transform.position + (transform.forward * Speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Waypoint")
            {
                //targetIndex++;
                targetIndex = targets.IndexOf(other.transform) + 1;
                if (debug)
                {
                    print("targetIndex: " + targetIndex);
                }
                changedTargets= true;

                if (targetIndex >= targets.Count)
                { 
                    targetIndex = 0;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {

        }
    }
}
