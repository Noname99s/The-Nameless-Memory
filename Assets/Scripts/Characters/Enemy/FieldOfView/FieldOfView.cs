using UnityEngine;

namespace Orpul1
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] Vector3 originPosi;
        [SerializeField] float viewRadius = 1;
        [SerializeField] float distance = 0;
        [SerializeField] LayerMask detectMask;
        [SerializeField] GameObject objController;

        Ray ray;
        RaycastHit2D hit;
        private void Start()
        {
            
        }
        private void Update()
        {
            ray = new Ray(transform.position + originPosi, Vector3.right);
            hit = Physics2D.Raycast(ray.origin, ray.direction, distance, detectMask.value);
            if (hit.collider == null) return;

        } 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + originPosi, viewRadius);
        }
    }

}
