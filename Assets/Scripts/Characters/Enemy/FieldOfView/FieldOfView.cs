using UnityEngine;

namespace Orpul1
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] Vector3 originPosi;
        [SerializeField] float viewRadius = 1;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + originPosi, viewRadius);
        }
    }

}
