using Orpul1.Camera;
using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CameraContext : MonoBehaviour
{
    [HideInInspector] public Transform target;
    public Camera mainCam;
    public CinemachineTargetGroup targetGroup;
    public float moveSpeed;
    public ECameraEffect cameraEffect;
    public Vector2 cameraView;
    public CinemachineCamera cinemachineCamera;
    public CinemachineConfiner2D confiner;
    public CameraTarget cameraTarget;//[HideInInspector]
    public bool IsFollowState
    {
        get
        {
            return (cameraEffect == ECameraEffect.None);
        }
    }
    public bool IsVignetteState
    {
        get
        {
            return cameraEffect == ECameraEffect.Vignette;
        }
    }
    
}
