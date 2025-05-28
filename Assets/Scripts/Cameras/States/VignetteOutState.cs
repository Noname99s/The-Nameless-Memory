using System;
using UnityEngine;

namespace Orpul1.Camera.State
{
    public class VignetteOutState : CameraState
    {
        public VignetteOutState(CameraContext data, Action<int> transitionTo) : base(data, transitionTo)
        {
        }

        public override void OnStart()
        {
        }

        public override void OnUpdate()
        {
            if (IsVignetteMinValue())
            {
                transitionTo((int)ECameraState.Follow);
                return;
            }
            DecreaseVignetteValue();
        }
        public override void OnExit()
        {
            data.vignetteInfo.camVignette.active = false;
        }
        private bool IsVignetteMinValue()
        {
            if (data.vignetteInfo.camVignette.intensity.value > data.vignetteInfo.minAmouont)
                return false;
            return true;
        }
        private void DecreaseVignetteValue()
        {
            data.vignetteInfo.camVignette.intensity.value -= data.vignetteInfo.interval * Time.deltaTime;
        }
    }
}