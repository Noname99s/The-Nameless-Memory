using System;
using UnityEngine;

namespace Orpul1
{
    [Serializable]
    public class SightInfo
    {
        public Vector3 originPosi;
        public LayerMask detectMask;
        public LayerMask ignoreMask;
        [Range(0,360)] public float angle;
        public float radius;
        public float rotateZ;
        public Ray2D obstacleRay;
        public RaycastHit2D obstacleHit;
    }
    public class FieldOfView : MonoBehaviour, ISightFindAble, ISetDataAble<float,float>
    {
        [SerializeField] SightInfo sightInfo;
        [SerializeField] Vector3 originScale = Vector3.one;
        private void Reset()
        {
            SetOriginScale();
        }
        
        [ContextMenu("SetOriginScale")]
        private void SetOriginScale()
        {
            originScale = transform.localScale;
        }

        public void SetValue(float radius, float angle)
        {
            sightInfo.radius = radius;
            sightInfo.angle = angle;
        }
        public Transform DetectTarget()
        {
            int flipX = (int)(transform.localScale.x / originScale.x);
            Vector2 originPosi = sightInfo.originPosi;
            originPosi.x *= flipX;
            originPosi += (Vector2)transform.position;
            Collider2D target = Physics2D.OverlapCircle(originPosi, sightInfo.radius, sightInfo.detectMask.value);
            if (target == null) return null;
            if (!IsInSight(target, originPosi,flipX)) return null;
            if(!IsPlayer(target)) return null;
            DrawRayPlayerPosi(originPosi, target.transform.position);
            IGetValue<Transform> targetTransform = target.GetComponent<IGetValue<Transform>>();
            if (targetTransform == null) return null;
            return targetTransform.GetValue();
        }
        private void DrawRayPlayerPosi(Vector3 origin, Vector3 target)
        {
            Vector3 dir = target - origin;
            float distance = Vector3.Distance(origin, dir);
            Debug.DrawRay(origin, (Vector2)dir * distance, Color.yellow);

        }
        private bool IsInSight(Collider2D target, Vector3 originPosi, int flipX)
        {
            Vector2 dir = (target.transform.position - originPosi).normalized;
            float dot = Vector2.Dot(dir, RadToDeg(sightInfo.rotateZ * flipX));
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            if (angle >= (sightInfo.angle * 0.5f)) return false;
            return true;
        }

        private bool IsPlayer(Collider2D target)
        {
            sightInfo.obstacleRay.origin = transform.position + sightInfo.originPosi;
            sightInfo.obstacleRay.direction = (target.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(target.transform.position, transform.position);
            sightInfo.obstacleHit = Physics2D.Raycast(sightInfo.obstacleRay.origin, sightInfo.obstacleRay.direction, distance, sightInfo.ignoreMask.value);
            return IsDetectedIgnoreLayer(sightInfo.obstacleHit.collider);
        }
        private bool IsDetectedIgnoreLayer(Collider2D target)
        {
            return target == null;
        }
        private void OnDrawGizmos()
        {
            float flipX = transform.position.x / originScale.x;
            Vector3 originPosi =  sightInfo.originPosi;
            originPosi.x *= flipX;
            originPosi += transform.position;
            DrawCircle(originPosi);
            DrawAngle(originPosi, flipX);
        }
        private void DrawCircle(Vector3 originPosi)
        {
            Gizmos.DrawWireSphere(originPosi, sightInfo.radius);
        }
        private void DrawAngle(Vector3 originPosi,float flipX)
        {
            float angle = sightInfo.angle * 0.5f;
            Vector2 viewAngle1 = RadToDeg(-angle + sightInfo.rotateZ * flipX);
            Vector2 viewAngle2 = RadToDeg(angle + sightInfo.rotateZ * flipX);
            DrawAngleLine(originPosi, (Vector2)originPosi + viewAngle1 * sightInfo.radius, Color.blue);
            DrawAngleLine(originPosi, (Vector2)originPosi + viewAngle2 * sightInfo.radius, Color.red);
        }
        private Vector2 RadToDeg(float angle)
        {
            angle = (angle - transform.eulerAngles.z) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        }
        private void DrawAngleLine(Vector3 origin, Vector3 anglePosi, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(origin, anglePosi);
        }

    }
}
