using System.Collections;
using Orpul1.Camera.Effect;
using Unity.Cinemachine;
using UnityEngine;

namespace Orpul1.Camera.Effect
{
    public class ZoomInOut : CameraEffect, ISetDataAble<float>
    {
        [SerializeField] float zoomValue = 5;
        [SerializeField] CinemachineCamera cameraZoom;
        bool isZoomIn = false;
        
        [ContextMenu("Test Set ZoomValue")]
        public override void ActiveEffect()
        {
            cameraZoom.Lens.OrthographicSize = zoomValue;
            /*isZoomIn = cameraZoom.Lens.OrthographicSize > zoomValue;
            StartCoroutine("ZoomCamera");*/
        }

        public override void DeactiveEffect()
        {
        }
        IEnumerator ZoomCamera()
        {
            float curZoomValue = cameraZoom.Lens.OrthographicSize--;
            bool isZoomIn = curZoomValue > zoomValue;
            while(!IsFinishZoom())
            {
                cameraZoom.Lens.OrthographicSize -= Time.deltaTime;
                yield return null;
            }
        }
        private bool IsFinishZoom()
        {
            if (isZoomIn && cameraZoom.Lens.OrthographicSize <= zoomValue)
                return true;
            if (!isZoomIn && cameraZoom.Lens.OrthographicSize >= zoomValue)
                return true;
            return false;
        }
        public void SetValue(float zoomValue)
        {
            this.zoomValue = zoomValue;
        }
    }
}