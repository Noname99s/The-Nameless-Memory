using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Orpul1.Camera.Effect
{
    public class CameraVignette : CameraEffect
    {
        [SerializeField] VolumeProfile profile;
        [SerializeField] Transform trPlayer;
        [SerializeField] UnityEngine.Camera mainCam;
        [SerializeField, Range(0, 1f)] float vignetteSpeed = 0;
        [SerializeField, Range(0, 1f)] float vignetteMaxAmount = 1;
        [SerializeField, Range(0, 1f)] float vignetteMinAmount = 1;
        
        Vignette vignette;
        Coroutine activeCoroutine, deactiveCoroutine;
        float interval = 0;
        bool isActived = false;

        private void Awake()
        {
            profile.TryGet<Vignette>(out vignette);
            if (vignette == null)
                Debug.LogError("Vignette is Null!");
            vignette.intensity.value = vignetteMinAmount;
        }
        private void OnDisable()
        {
            vignette.intensity.value = vignetteMinAmount;
            vignette.active = false;
        }
        private void Update()
        {
            if (!isActived) return;
            SetVignetteCenter();
        }
        public override void ActiveEffect()
        {
            StopVignetteCoroutine(deactiveCoroutine);
            activeCoroutine = StartCoroutine("ActiveVignette");
        }

        public override void DeactiveEffect()
        {
            StopVignetteCoroutine(activeCoroutine);
            deactiveCoroutine = StartCoroutine("DeactiveVignette");
        }
        private IEnumerator ActiveVignette()
        {
            vignette.active = true;
            isActived = true;
            interval = 0;
            while (IsArriveAmount(vignette.intensity.value, vignetteMaxAmount))
            {
                interval += Time.deltaTime;
                vignette.intensity.value += interval * vignetteSpeed;
                yield return null;
            }
            //yield return ExecuteVignette(1, vignetteMaxAmount);
        }
        private IEnumerator DeactiveVignette()
        {
            interval = 0;
            while (IsArriveAmount(vignetteMinAmount, vignette.intensity.value))
            {
                interval -= Time.deltaTime;
                vignette.intensity.value += interval * vignetteSpeed;
                yield return null;
            }
            isActived = false;
            //yield return ExecuteVignette(-1, vignetteMinAmount);
        }
        private void SetVignetteCenter()
        {
            Vector2 center = mainCam.WorldToViewportPoint(trPlayer.position);
            vignette.center.value = center;
        }
        private bool IsArriveAmount(float value, float arrive)
        {
            return value < arrive;
        }

        private void StopVignetteCoroutine(Coroutine coroutine)
        {
            if (coroutine == null) return;
            StopCoroutine(coroutine);
        }
    }
}