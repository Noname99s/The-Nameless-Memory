using System.Collections.Generic;
using UnityEngine;
using Orpul1.Camera;
namespace Orpul1.Camera.Effect
{
    public class CameraEffectController : MonoBehaviour, ICameraEffectAble
    {
        [SerializeField] List<CameraEffect> effects;
        
        public void Execute(int type)
        {
            effects[type].ActiveEffect();
        }
        public void Stop(int type)
        {
            effects[type].DeactiveEffect();
        }
    }
}