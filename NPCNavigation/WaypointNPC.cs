namespace GameCreator.Core
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class WaypointNPC : IAction
    {
        // debug enable
        public bool myDebug = false;

        // debug Allow 2 WPs for same NPC
        public bool WpDublicatesAllowed = false;

        // localVariable Gameobject from somewhere where you want to store next target for this selection.
        // In my case on my NPC, i have local var list, with a localvariables component for the Waypoints.
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty NextTarget = new VariableProperty(Variable.VarType.LocalVariable);

        // Localstore of waypoints from shared
        public GameObject[] SharedWPs;

        public string GlobalStoreTag = "WaypointArray";
        // placeholder for globalGameObject with all the WP's.
        public GameObject GlobalWPStore;
        public WayPointArray ArrayScript;


        // MaxRange of next target
        public float NxtTargetMaxRange = 5f;
        // MaxAttempts to find before continue
        public int MaxAttempts = 5;

        // privates
        private int _tmpAttempts = 0;
        private int _index = 0;
        private GameObject _testTarget;
        private int _oldTarget;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (_index != 0) _oldTarget = _index;

            //reset
            RESET:
            _tmpAttempts = 0;

            // Grab waypoints from Globalstore
            GlobalWPStore = GameObject.FindGameObjectWithTag(GlobalStoreTag);
            ArrayScript = GlobalWPStore.GetComponent<WayPointArray>();
            SharedWPs = ArrayScript.Waypoints;
            if (myDebug) Debug.Log("Array Copied");


            // Find next target x ammount of times

            RETRY:
            _tmpAttempts++;
            if (myDebug) Debug.Log("Attempt: " + _tmpAttempts);
            // if max, then exit action
            if (_tmpAttempts >= MaxAttempts) goto SUCCES;

            if(FindNextWp())
            {
                goto SUCCES;
            } else
            {
                goto RETRY;
            }


            SUCCES:

            // check if waypoint is in Use.
            if (ArrayScript.WpInUse[_index] == true)
            {
                // WP allready in use, RESET selection find a new WP
                goto RESET;
            }
            else
            {
                // Set WP as in use.
                ArrayScript.WpInUse[_index] = true;
                // Set old WP as free
                ArrayScript.WpInUse[_oldTarget] = false;
            }

            return true;
        }



        // Find actual waypoint and store it.
        private bool FindNextWp()
        {
            //select random number and select a waypoint from array with this number.
            _index = Random.Range(0, SharedWPs.Length);
            _testTarget = SharedWPs[_index];

            // check if WP is in range of maxRange from npc
            if((gameObject.transform.position - SharedWPs[_index].transform.position).magnitude < NxtTargetMaxRange)
            {
                // it is in range, now store it in GC next target variable.
                if (myDebug) Debug.Log("Range to target: " + (gameObject.transform.position - SharedWPs[_index].transform.position).magnitude);

                VariablesManager.SetLocal(this.NextTarget.local.targetObject, this.NextTarget.local.ToString(), SharedWPs[_index]);
                return (true);
            }

            return (false);
        }


#if UNITY_EDITOR
        public static new string NAME = "MVT/Navigation/WaypointNPC";
#endif
    }
}
