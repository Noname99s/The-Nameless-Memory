using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] Vignette camVignette;

    private void Start()
    {
        volumeProfile.TryGet<Vignette>(out camVignette);
    }

}
