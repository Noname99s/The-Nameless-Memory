using UnityEngine;

public interface ISetCamerGroupAble
{
    void SetGroup(Transform target, float weight, float radius);
    void RemoveGroup(Transform target);
}
