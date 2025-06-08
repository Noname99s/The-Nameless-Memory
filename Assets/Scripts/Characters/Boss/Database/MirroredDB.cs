using Orpul1;
using Orpul1.Boss.Mirrored;
using UnityEngine;

[CreateAssetMenu(fileName = "MirroredDB", menuName = "DataBase/MirroredDB")]
public class MirroredDB : ScriptableObject
{

    enum EDBInfo
    {
        MirroredBossInfoStart = 0,
        MirroredHitStateInfoStart = 5,
    }
    public void SetDB(MirroredBlackboardComponent blackboard)
    {
        SetMirroredInfo(blackboard.bossInfo);
        SetMirroredHitInfo(blackboard.hitStateInfo);
    }
    private void SetMirroredInfo(MirroredBossInfo bossInfo)
    {
        int index = (int)EDBInfo.MirroredBossInfoStart;
        bossInfo.hp = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
        bossInfo.maxHp = bossInfo.hp;
        bossInfo.phaseHpConditions[(int)EPhaseType.Phase1].range1 = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
        bossInfo.phaseHpConditions[(int)EPhaseType.Phase1].range2 = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
        bossInfo.phaseHpConditions[(int)EPhaseType.Phase2].range1 = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
        bossInfo.phaseHpConditions[(int)EPhaseType.Phase2].range2 = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
    }
    private void SetMirroredHitInfo(MirroredHitStateInfo hitInfo)
    {
        int index = (int)EDBInfo.MirroredHitStateInfoStart;
        hitInfo.blinkCount = (int)BG_DB_Bossdata.GetEntity(index++)._Value;
        hitInfo.blinkeDelay = BG_DB_Bossdata.GetEntity(index++)._Value;
    }
}
