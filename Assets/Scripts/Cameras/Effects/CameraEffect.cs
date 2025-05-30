using System.Collections;
using UnityEngine;
namespace Orpul1.Camera.Effect
{
    public abstract class CameraEffect : MonoBehaviour
    {
        public abstract void ActiveEffect();
        public abstract void DeactiveEffect();
    }
}