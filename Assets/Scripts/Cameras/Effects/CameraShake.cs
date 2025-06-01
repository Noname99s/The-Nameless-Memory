using System.Collections;
using UnityEngine;

namespace Orpul1.Camera.Effect
{
    public class CameraShake : CameraEffect
    {
        [SerializeField] Transform trCamera;
        [SerializeField] float shakeStrength;
        [SerializeField] float delayShake=0.2f;
        bool isAborted = false;
        Coroutine shakeCoroutine;
        WaitForSeconds waitShake;
        Vector3 prevCameraPosi = Vector3.zero;
        private void Awake()
        {
            waitShake = new WaitForSeconds(delayShake);
        }
        public override void ActiveEffect()
        {
            isAborted = false;
            StopShakeCoroutine();
            shakeCoroutine = StartCoroutine("ShakeCamera");
        }

        public override void DeactiveEffect()
        {
            isAborted = true;
            StopShakeCoroutine();
        }

        IEnumerator ShakeCamera()
        {
            prevCameraPosi = trCamera.position;
            while (!isAborted)
            {
                ChangePosi(SetRandomCoord());
                yield return waitShake;
                trCamera.position = prevCameraPosi;
            }
            trCamera.position = prevCameraPosi;
        }

        void StopShakeCoroutine()
        {
            if (shakeCoroutine == null) return;
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }
        Vector2 SetRandomCoord()
        {
            return new Vector2
                (
                Random.Range(-shakeStrength, shakeStrength),
                Random.Range(-shakeStrength, shakeStrength)
                );
        }
        void ChangePosi(Vector3 posi)
        {
            trCamera.position += posi;
        }

        
    }
}