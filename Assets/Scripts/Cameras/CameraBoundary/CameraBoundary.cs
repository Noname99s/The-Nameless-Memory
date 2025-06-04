using UnityEngine;
using System.Collections.Generic;
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
        Vector3 startPosi = posis[(int)EViewPosi.StartPosi].localPosition;
        Vector3 endPosi = posis[(int)EViewPosi.EndPosi].localPosition;

        Vector2 middlePosi = (startPosi + endPosi) * 0.5f;
        boundary.offset = middlePosi;
        boundary.size = endPosi - startPosi;
    }


    private void Start()
    {
        CameraManager.Instance.SetCameraEvent(SetCameraBoundary);
        SetCameraBoundary();
    }
    private void SetCameraBoundary()
    {
        ISetDataAble<Collider2D> setConfinder = CameraManager.Instance.GetCamera<ISetDataAble<Collider2D>>((int)ECameraType.MainCamera);
        if (setConfinder == null) return;
        setConfinder.SetValue(boundary);
    }
    private void OnDrawGizmos()
    {
        if (IsNullPosis()) return;
        Gizmos.color = Color.yellow;
        List<Vector2> lines = new List<Vector2>();
        Vector3 startPosi = posis[(int)EViewPosi.StartPosi].position;
        Vector3 endPosi = posis[(int)EViewPosi.EndPosi].position;
        lines.Add(new Vector2(startPosi.x, endPosi.y));
        lines.Add(endPosi);
        lines.Add(new Vector2(endPosi.x, startPosi.y));
        lines.Add(startPosi);
        lines.Add(new Vector2(startPosi.x, endPosi.y));

        for (int i = 0; i < lines.Count - 1; i++) 
        {
            Gizmos.DrawLine(lines[i], lines[i + 1]);
        }
    }
    private Vector3 GetColliderOriginPosi(Vector3 posi)
    {
        return transform.position + posi;
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
