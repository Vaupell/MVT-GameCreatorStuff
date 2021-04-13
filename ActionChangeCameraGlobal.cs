namespace GameCreator.Camera
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GameCreator;
    using GameCreator.Core;
    using GameCreator.Core.Hooks;
    using GameCreator.Variables;

    [AddComponentMenu("")]
    public class ActionChangeCameraGlobal : IAction
    {

        // public TargetGameObject target = new TargetGameObject();
        [VariableFilter(Variable.DataType.GameObject)]
        public VariableProperty MyGlobalCam = new VariableProperty(Variable.VarType.GlobalVariable);

        private CameraMotor NyCameraMotor;
        public bool mainCameraMotor = false;

        [Range(0.0f, 60.0f)]
        public float transistionTime = 0.0f;

         public override bool InstantExecute(GameObject target, IAction[] actions, int index)
        {

            if (HookCamera.Instance != null)
            {

                CameraController cameraController = HookCamera.Instance.Get<CameraController>();
                if (cameraController != null)
                {

                    CameraMotor motor = null;
                    GameObject CamGO = this.MyGlobalCam.Get(target) as GameObject;
                    if (CamGO != null) motor = CamGO.GetComponent<CameraMotor>();

                    if (motor != null)
                    {
                        cameraController.ChangeCameraMotor(
                            motor,
                            this.transistionTime
                        );
                    }
                }
            }
            return true;
        }

#if UNITY_EDITOR
        public static new string NAME = "MVT/GlobalCamVar";
#endif
    }
}
