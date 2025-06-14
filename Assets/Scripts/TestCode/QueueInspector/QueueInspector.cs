using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class QueueInspector : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] ObjectPool objPool;
    [SerializeField] List<GameObject> poolList = new List<GameObject>();
    private void Awake()
    {
        objPool.poolAction -= ShowQueue;
        objPool.poolAction += ShowQueue;
    }
    private void ShowQueue()
    {
        poolList.Clear();
        poolList = objPool.Pools.ToList();
    }
#endif
}
