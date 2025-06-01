using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Orpul1.Camera;
using Orpul1;

public class CameraBoundary : MonoBehaviour
{
    enum EViewPosi
    {
        StartPosi,
        EndPosi,
    }
    [SerializeField] BoxCollider2D boundary;
    [SerializeField] Transform[] posis;

    [ContextMenu("Set Camera boundary")]
    public void SetBoundary()
    {
        Vector2 middlePosi = (posis[(int)EViewPosi.StartPosi].position + posis[(int)EViewPosi.EndPosi].position) * 0.5f;
        boundary.offset = middlePosi;
        boundary.size = posis[(int)EViewPosi.EndPosi].position- posis[(int)EViewPosi.StartPosi].position;
    }


    private void Start()
    {
        ISetDataAble<Collider2D> setConfinder = CameraManager.Instance.GetCamera((int)ECameraType.MainCamera).GetComponent<ISetDataAble<Collider2D>>();
        setConfinder.SetValue(boundary);
    }
    private void OnDrawGizmos()
    {
        if (IsNullPosis()) return;
        Gizmos.color = Color.yellow;
        List<Vector2> lines = new List<Vector2>();
        lines.Add(new Vector2(posis[(int)EViewPosi.StartPosi].position.x, posis[(int)EViewPosi.EndPosi].position.y));
        lines.Add(posis[(int)EViewPosi.EndPosi].position);
        lines.Add(new Vector2(posis[(int)EViewPosi.EndPosi].position.x, posis[(int)EViewPosi.StartPosi].position.y));
        lines.Add(posis[(int)EViewPosi.StartPosi].position);
        lines.Add(new Vector2(posis[(int)EViewPosi.StartPosi].position.x, posis[(int)EViewPosi.EndPosi].position.y));

        for (int i = 0; i < lines.Count - 1; i++) 
        {
            Gizmos.DrawLine(lines[i], lines[i + 1]);
        }
    }
    private bool IsNullPosis()
    {
        if (posis.Length < 2) return true;
        for (int i = 0; i < posis.Length; i++)
        {
            if (posis[i] == null) return true;
        }
        return false;
    }
}
