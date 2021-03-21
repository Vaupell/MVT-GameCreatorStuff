namespace GameCreator.Core
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[AddComponentMenu("")]
	public class IgniterMVTKeyDown : Igniter 
	{
		public string UinputName;

#if UNITY_EDITOR
        public new static string NAME = "MVT/Unity Input";
#endif

		private void Update()
		{
			if (Input.GetButton(UinputName))
			{
                this.ExecuteTrigger(gameObject);
			}
		}
	}
}