using System;
using Orpul1;
using UnityEngine;

public class EnemyController : MonoBehaviour,ISetDataAble<Action<GameObject>>, IResetAble
{
    Action<GameObject> enqueueAction;
    [SerializeField] float deactiveTime = 0;
    float interval;

    // Update is called once per frame
    void Update()
    {
        interval += Time.deltaTime;
        if (interval < deactiveTime) 
            return;

        enqueueAction(gameObject);
    }
    public void ResetValue()
    {
        interval = 0;
    }

    public void SetValue(Action<GameObject> enqueueAction)
    {
        this.enqueueAction = enqueueAction;
    }
}
