using Orpul1;
using UnityEngine;

[CreateAssetMenu(fileName = "MirroredDB", menuName = "DataBase/MirroredDB")]
public class MirroredDB : ScriptableObject, IVisitAble
{

    enum EDBInfo
    {
        MirroredBossInfoStart = 0,
        MirroredHitStateInfoStart = 5,
    }

    public void Visit<T>(T t)
    {
        SetMirroredInfo(t);
        SetMirroredHitInfo(t);
    }
    private void SetMirroredInfo<T>(T t)
    {
        if (!(t is MirroredBossInfo info)) return;
        int index = (int)EDBInfo.MirroredBossInfoStart;
    }
    private void SetMirroredHitInfo<T>(T t)
    {
        if (!(t is MirroredHitStateInfo info)) return;
        int index = (int)EDBInfo.MirroredBossInfoStart;
    }
}
