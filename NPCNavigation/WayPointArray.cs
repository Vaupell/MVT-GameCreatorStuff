namespace GameCreator.Core
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class WayPointArray : IAction
    {

        // The main waypoint array
        public GameObject[] Waypoints;
        public bool[] WpInUse;
        

        // Tag of waypoints
        public string WaypointsTagName = "Waypoint";


        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            Waypoints = GameObject.FindGameObjectsWithTag(WaypointsTagName);
            WpInUse = new bool[Waypoints.Length];

            return true;
        }


#if UNITY_EDITOR
        public static new string NAME = "MVT/Navigation/WaypointArray";
#endif
    }
}