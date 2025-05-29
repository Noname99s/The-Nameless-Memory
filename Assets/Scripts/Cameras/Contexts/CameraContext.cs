using Orpul1.Camera;
using Orpul1.Camera.State;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraContext : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rigid;

    public float moveSpeed;
    public ECameraEffect cameraEffect;
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
