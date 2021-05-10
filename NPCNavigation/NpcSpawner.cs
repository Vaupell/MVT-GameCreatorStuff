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

        // Npcs and max ammount
        public GameObject[] NpcsToSpawn; // Remember to add npc's in the inspector
        public string NpcTagName = "Enemy";
        public bool RespawnNPCs = false;
        public int MaintainNpcAmmount = 3;
        public int CurrentNpcs = 0;

        // script internal use only
        private bool initialized = false;
        private int _RndNpc;
        private int _RndSp;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {
            // private initialization, run only once.
            if (initialized == false)
            {
                // setup array
                SpawnPoints = GameObject.FindGameObjectsWithTag(SpawnpointTagName);

                // initial spawns based on maintain npc ammount
                for (int i = 0; i <= MaintainNpcAmmount; i++)
                {
                    // rnd spawn and npc selection
                    _RndNpc = Random.Range(0, NpcsToSpawn.Length);
                    _RndSp = Random.Range(0, SpawnPoints.Length);

                    UpdateNpcs();
                    SpawnNewNpc(_RndNpc, _RndSp);
                }

                initialized = true;
            }


            // normal check or "loop" by GC ;)
            if (CurrentNpcs <= MaintainNpcAmmount && RespawnNPCs == true)
            {
                // select random range to select a NPC to spawn
                _RndNpc = Random.Range(0, NpcsToSpawn.Length);
                if (myDebug) Debug.Log("Random range found: " + _RndNpc);

                // select random spawnpoint
                _RndSp = Random.Range(0, SpawnPoints.Length);
                if (myDebug) Debug.Log("Random spawn point found: " + _RndSp);

                // spawn new npc
                SpawnNewNpc(_RndNpc, _RndSp);
            }

            // count Npcs in scene
            UpdateNpcs();
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
