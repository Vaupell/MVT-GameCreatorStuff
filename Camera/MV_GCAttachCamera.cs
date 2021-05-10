using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCreator;
using GameCreator.Camera;

public class MV_GCAttachCamera : MonoBehaviour
{

    public CameraMotor MyCameraMotor;
    public GameObject MyMotorGameObject;
    public string CameraMotorToFind = "Camera Motor";

    void Start()
    {
        
    }

    public void Awake()
    {
        MyMotorGameObject = GameObject.Find(CameraMotorToFind);
        if(MyCameraMotor != null)
        {
            MyCameraMotor = MyMotorGameObject.GetComponent<CameraMotor>();
            gameObject.GetComponent<CameraController>().currentCameraMotor = MyCameraMotor;
        }
      
    }
}
