namespace GameCreator.Core
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DungeonArchitect.Builders.GridFlow;
    using DungeonArchitect;

    [AddComponentMenu("")]
    public class MVT_DA_starter : IAction
    {
        public Dungeon dungeon;
        public GridFlowDungeonConfig GFDungeonConfig;

        public bool RandomSeed = false;
        public float MinVal = 1;
        public float MaxVal = 200;
        public uint FixedSeed = 12;

        public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            if(dungeon != null)
            {
                if (RandomSeed)
                {
                    GFDungeonConfig.Seed = (uint)Random.Range(MinVal, MaxVal);
                } else
                {
                    GFDungeonConfig.Seed = FixedSeed;
                }
                dungeon.Build();
            }

            return true;
        }

#if UNITY_EDITOR
        public static new string NAME = "MVT/DungeonArchitect/DAStarter";
#endif
    }
}