using Orpul1.Camera;
using UnityEngine;
using System.Collections.Generic;

public class CameraContext : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rigid;
    public Camera mainCam;

    public float moveSpeed;
    public ECameraEffect cameraEffect;
    public List<Transform> viewBoundary;
    public Vector2 cameraView;
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
