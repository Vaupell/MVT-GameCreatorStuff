namespace GameCreator.Core
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class NpcSpawner : IAction
    {
        // enable debug messages
        public bool myDebug = false;

        // The main Spawnpoint array
        public string SpawnpointTagName = "NpcSpawnPoint";
        public GameObject[] SpawnPoints;
        public bool[] SpInUse;

        // Npcs and max ammount
        public GameObject[] NpcsToSpawn; // Remember to add npc's in the inspector
        public string NpcTagName = "Enemy";
        public int MaintainNpcAmmount = 3;
        public int CurrentNpcs = 0;

        // script internal use only
        private int _RndNpc;
        private int _RndSp;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            if (CurrentNpcs <= MaintainNpcAmmount)
            {
                // find and setup spawnpoints
                SpawnPoints = GameObject.FindGameObjectsWithTag(SpawnpointTagName);
                SpInUse = new bool[SpawnPoints.Length];

                // select random range to select a NPC to spawn
                _RndNpc = Random.Range(0, NpcsToSpawn.Length);
                if (myDebug) Debug.Log("Random range found: " + _RndNpc);

                // select random spawnpoint
                _RndSp = Random.Range(0, SpawnPoints.Length);
                if (myDebug) Debug.Log("Random spawn point found: " + _RndSp);

                // spawn new npc
                SpawnNewNpc(_RndNpc, _RndSp);
            } else
            {
                UpdateNpcs();
            }
            return true;
        }

        private void UpdateNpcs()
        {
            CurrentNpcs = GameObject.FindGameObjectsWithTag(NpcTagName).Length;
        }

        private void SpawnNewNpc(int Character, int SelectedSpawn)
        {
            Instantiate(NpcsToSpawn[Character], SpawnPoints[SelectedSpawn].transform.position, Quaternion.Euler(0, 0, 0));
            CurrentNpcs++;
        }

#if UNITY_EDITOR
        public static new string NAME = "MVT/NPC/NpcManager";
#endif

    }
}
