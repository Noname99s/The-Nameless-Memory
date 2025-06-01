using UnityEngine;
using System.Collections.Generic;

namespace Orpul1.Camera
{
    public class ViewBoundary : MonoBehaviour,IGetValue<List<Transform>>
    {
        [SerializeField] List<Transform> posis = new List<Transform>();

        public List<Transform> GetValue()
        {
            return posis;
        }

        private void OnDrawGizmos()
        {
            if (posis == null) return;
            Gizmos.color = Color.yellow;
            List<Vector2> dotPosis = new List<Vector2>();
            for (int i = 0; i < posis.Count - 1; i++) 
            {
                //dotPosis.Add(posis[i].position);

                dotPosis.Add(new Vector2(posis[i].position.x, posis[i + 1].position.y));
                dotPosis.Add(posis[i + 1].position);
                dotPosis.Add(new Vector2(posis[i + 1].position.x, posis[i].position.y));
                dotPosis.Add(posis[i].position);
                dotPosis.Add(new Vector2(posis[i].position.x, posis[i + 1].position.y));
            }
            ;

            for (int i = 0; i < dotPosis.Count - 1; i++)
            {
                Gizmos.DrawLine(dotPosis[i], dotPosis[i + 1]);
            }
        }
    }
}