namespace GameCreator.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class WaypointCharacter : IAction
    {
        // localVariable Gameobject from somewhere where you want to store next target for this selection.
        // In my case on my NPC, i have local var list, with a localvariables component for the Waypoints.
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty NextTarget = new VariableProperty(Variable.VarType.LocalVariable);

        // Tag name used for the waypoints.
        public string WayPointTag = "Waypoint";

        // All waypoints in map, might need adjusting if to long load time.
        public GameObject[] AllWaypoints;

        // Radius of waypoints to react to
        public float SelectionRadius = 5f;

        // enable debug logging
        public bool useDebug = false;

        // Attemps of getting a WP in range
        public int MaxAttemptsPrRun = 5;

        // Storing selection + Random selection
        public GameObject NextTargetSelected;
        int indexSelection;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            // Find all Waypoints in map
            AllWaypoints = GameObject.FindGameObjectsWithTag(WayPointTag);


            // number of attempts before exit
            MaxAttemptsPrRun = 0;

        retry:

            // find and store new waypoint
            MaxAttemptsPrRun++;
            if(useDebug) Debug.Log("Attemp: " + MaxAttemptsPrRun);
            if (MaxAttemptsPrRun >= 5) goto Succes;

            if (SelectWaypoint())
            {
                goto Succes;
            } else
            {
                goto retry;
            }

        Succes:
            return true;
        }


        private bool SelectWaypoint()
        {
            // find waypoint
            indexSelection = Random.Range(0, AllWaypoints.Length);
            NextTargetSelected = AllWaypoints[indexSelection];

            // check if in range
            if ((gameObject.transform.position - AllWaypoints[indexSelection].transform.position).magnitude < SelectionRadius)
            {
                // VariablesManager.SetLocal(NextTarget, NextTarget, AllWaypoints[indexSelection].gameObject);
                if(useDebug) Debug.Log((gameObject.transform.position - AllWaypoints[indexSelection].transform.position).magnitude);

                if(useDebug) Debug.Log("Storing wp in LocalVar");
                VariablesManager.SetLocal(this.NextTarget.local.targetObject, this.NextTarget.local.ToString(), AllWaypoints[indexSelection]);
                return (true);
            }
            return (false);
        }


    #if UNITY_EDITOR
        public static new string NAME = "MVT/WaypointCharacter";
    #endif

    }
}
