using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Orpul1.Camera.State
{
    [Serializable]
    public class VignetteStateInfo
    {
        public VolumeProfile volumeProfile;
        [HideInInspector] public Vignette camVignette;
        [Range(0, 1f)] public float interval;
        [Range(0, 1f)] public float minAmouont;
        [Range(0, 1f)] public float maxAmouont;
    }
    public class VignetteInState : CameraState
    {
        public VignetteInState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo)
        {
            if (data.vignetteInfo.volumeProfile != null)
                data.vignetteInfo.volumeProfile.TryGet<Vignette>(out data.vignetteInfo.camVignette);
        }

        public override void OnStart()
        {
            data.vignetteInfo.camVignette.active = true;
        }

        public override void OnUpdate()
        {
            if (data.cameraEffect != ECameraEffect.Vignette)
                transitionTo((int)ECameraState.VignetteOut);
            if (IsVignetteMaxValue())
                return;

            IncreaseVignetteValue();
        }

        public override void OnExit()
        {
        }
        private bool IsVignetteMaxValue()
        {
            if (data.vignetteInfo.camVignette.intensity.value < data.vignetteInfo.maxAmouont)
                return false;
            return true;
        }
        private void IncreaseVignetteValue()
        {
            data.vignetteInfo.camVignette.intensity.value += data.vignetteInfo.interval * Time.deltaTime;
        }
    }
}