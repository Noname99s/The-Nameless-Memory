using Orpul1.Camera.State;
using Orpul1.State;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    [SerializeField] StateController stateController;
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] Vignette camVignette;

    private void Start()
    {
        if (volumeProfile != null) 
            volumeProfile.TryGet<Vignette>(out camVignette);
        stateController.TransitionState((int)ECameraState.Follow);
    }
    private void LateUpdate()
    {
        stateController.ProcessState();
    }

}
