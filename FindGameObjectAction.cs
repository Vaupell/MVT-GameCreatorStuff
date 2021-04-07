namespace GameCreator.Core
{
    using System;
    using UnityEngine;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class FindGameObjectAction : IAction
    {
        private GameObject GameObjectToFind;
        public string NameOfGameObjectToFind;
        public string GlobalGOVarName;

        public override bool InstantExecute(GameObject invoker, IAction[] actions, int index)
        {
            GameObjectToFind = GameObject.Find(NameOfGameObjectToFind);
            VariablesManager.SetGlobal(GlobalGOVarName, GameObjectToFind);

            return true;
        }

        #if UNITY_EDITOR
        public static new string NAME = "MVT/MyActions/findAndStoreGameObject";
        #endif
    }
}