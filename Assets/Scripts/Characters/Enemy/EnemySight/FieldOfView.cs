using Orpul1.Enemy;
using UnityEngine;

namespace Orpul1
{
    public class FieldOfView : ISightFindAble
    {
        SightInfo sightInfo;
        public FieldOfView(SightInfo sightInfo)
        {
            this.sightInfo = sightInfo;
        }

        public Transform DetectTarget()
        {
            sightInfo.ray = new Ray(sightInfo.trTarget.position + sightInfo.originPosi, Vector3.right);
            sightInfo.hit = Physics2D.Raycast(sightInfo.ray.origin, sightInfo.ray.direction, sightInfo.distance, sightInfo.detectMask.value);
            Debug.DrawRay(sightInfo.ray.origin, sightInfo.ray.direction * sightInfo.distance);
            if (sightInfo.hit.collider == null) return null;
            return sightInfo.hit.transform;
        }
    }

}
