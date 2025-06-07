using System.Collections;
using Orpul1.Camera.Effect;
using Unity.Cinemachine;
using UnityEngine;

namespace Orpul1.Camera.Effect
{
    public class ZoomInOut : CameraEffect, ISetDataAble<float>
    {
        [SerializeField] float zoomSize = 5;
        [SerializeField, Range(0, 1f)] float zoomSpeed = 1;
        [SerializeField] CinemachineCamera cameraZoom;
        CinemachineConfiner2D confinder;
        bool isZoomIn = false;
        int zoomValue = 0;
        Coroutine zoomProcess;
        
        [ContextMenu("Test Set ZoomValue")]
        public override void ActiveEffect()
        {
            if (zoomProcess != null)
                StopCoroutine(zoomProcess);

            isZoomIn = cameraZoom.Lens.OrthographicSize > zoomSize;
            zoomValue = isZoomIn ? -1 : 1;
            zoomProcess = StartCoroutine("ZoomCamera");
        }

        public override void DeactiveEffect()
        {
        }
        IEnumerator ZoomCamera()
        {
            float curZoomValue = cameraZoom.Lens.OrthographicSize;
            while (!IsFinishZoom())
            {
                cameraZoom.Lens.OrthographicSize = Mathf.Lerp(cameraZoom.Lens.OrthographicSize, zoomSize, zoomSpeed * Time.deltaTime);
                yield return null;
            }
        }
        private bool IsFinishZoom()
        {
            if (isZoomIn && cameraZoom.Lens.OrthographicSize <= zoomSize)
                return true;
            if (!isZoomIn && cameraZoom.Lens.OrthographicSize >= zoomSize)
                return true;
            return false;
        }
        public void SetValue(float zoomSize)
        {
            this.zoomSize = zoomSize;
        }
    }
}