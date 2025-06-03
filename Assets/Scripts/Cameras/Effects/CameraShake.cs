using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Orpul1.Camera.Effect
{
    public class CameraShake : CameraEffect
    {
        [SerializeField] CinemachineImpulseSource impulseSource;
        [SerializeField] float shakeStrength;
        [SerializeField] float delayShake=0.2f;
        [SerializeField] int shakeCount = 3;
        Coroutine shakeCoroutine;
        WaitForSeconds waitShake;
        private void Awake()
        {
            waitShake = new WaitForSeconds(delayShake);
        }
        public override void ActiveEffect()
        {
            StopShakeCoroutine();
            shakeCoroutine = StartCoroutine("ShakeCamera");
        }

        public override void DeactiveEffect()
        {
            StopShakeCoroutine();
        }

        IEnumerator ShakeCamera()
        {
            for (int i = 0; i < shakeCount; i++)
            {
                impulseSource.GenerateImpulse();
                yield return waitShake;
            }
        }

        void StopShakeCoroutine()
        {
            if (shakeCoroutine == null) return;
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }

        
    }
}