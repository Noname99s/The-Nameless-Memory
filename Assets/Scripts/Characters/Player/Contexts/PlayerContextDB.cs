using UnityEngine;
using Orpul1.State.Attack;
using BansheeGz.BGDatabase;
using Orpul1.Player.Skill;
namespace Orpul1.Player
{
    [CreateAssetMenu(fileName = "PlayerContextDB", menuName = "DataBase/PlayerContextDB")]
    public class PlayerContextDB : ScriptableObject
    {
        enum EContextInfo
        {
            InfoStart = 0,
            MoveInfoStart = 9,
            JumpInfoStart = 11,
            FallInfoStart = 16,
            HitInfoStart = 20,
            AttackInfoStart = 24,
            SkillInfoStart = 29,
        }

        //TODO: 플레이어 데이터 캐싱 후 PlayerContext에 넣기
        [SerializeField] PlayerInfo playerInfo = new PlayerInfo();
        [SerializeField] PlayerMoveInfo moveInfo = new PlayerMoveInfo();
        [SerializeField] PlayerJumpInfo jumpInfo = new PlayerJumpInfo();
        [SerializeField] PlayerFallInfo fallInfo = new PlayerFallInfo();
        [SerializeField] PlayerHitInfo hitInfo = new PlayerHitInfo();
        [SerializeField] PlayerAttackInfo attackInfo = new PlayerAttackInfo();
        [SerializeField] PlayerStatusInfo statusInfo = new PlayerStatusInfo();
        [SerializeField] PlayerSkillInfo skillInfo = new PlayerSkillInfo();

        public PlayerInfo PlayerInfo { get { return playerInfo; } }
        public PlayerMoveInfo MoveInfo { get { return moveInfo; } }
        public PlayerJumpInfo JumpInfo { get { return jumpInfo; } }
        public PlayerFallInfo FallInfo { get { return fallInfo; } }
        public PlayerHitInfo HitInfo { get { return hitInfo; } }
        public PlayerAttackInfo AttackInfo { get { return attackInfo; } }
        public PlayerStatusInfo StatusInfo { get { return statusInfo; } }
        public PlayerSkillInfo SkillInfo { get { return skillInfo; } }

        [ContextMenu("Set PlayerDB Data")]
        public void SetData()
        {
            
            SetStatusInfo();
            SetPlayerMoveInfo();
            SetPlayerJumpInfo();
            SetPlayerFallInfo();
            SetPlayerHitInfo();
            SetPlayerAttackInfo();
            SetPlayerSkillInfo();
        }
        public void SetStatusInfo()
        {
            int index = (int)EContextInfo.InfoStart;
            if (Managers.Game.playData.isStatusLoaded) return;
            Managers.Game.playData.isStatusLoaded = true;
            statusInfo.maxHp = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.hp = statusInfo.maxHp;

            //TODO : 플레이어 데이터 베이스 갱신되면 주석 해재하기(2025/6/5)
            statusInfo.deathCount = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.getMemoryPeaceCounts[(int)Stage.Stage1]= (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.getMemoryPeaceCounts[(int)Stage.Stage2]= (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.getMemoryPeaceCounts[(int)Stage.Stage3]= (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.getMemoryPeaceCounts[(int)Stage.Stage4]= (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.maxMemoryPeaceCount = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.increaseStateValue = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.curStatePercent = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerMoveInfo()
        {
            int index = (int)EContextInfo.MoveInfoStart;
            moveInfo.moveSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            moveInfo.climbSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerJumpInfo()
        {
            int index = (int)EContextInfo.JumpInfoStart;
            jumpInfo.gravityStrength = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.jumpPower= BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.decelerate = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.minJumpSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.jumpCount = (int)BG_DB_PlayerData.GetEntity(index)._InitialValue;
        }
        public void SetPlayerFallInfo()
        {
            int index = (int)EContextInfo.FallInfoStart;
            fallInfo.defaultFallSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.fallDamageOverTime = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.onAirMoveSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.maxFallDamage = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerHitInfo()
        {
            int index = (int)EContextInfo.HitInfoStart;
            hitInfo.knockBackPower = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.invincibleTime = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.hitDuration = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.cameraShakeDuration = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerAttackInfo()
        {
            int index = (int)EContextInfo.AttackInfoStart;
            attackInfo.attackDelays[(int)EPlayerAttack.NormalAttack] = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            attackInfo.attackDelays[(int)EPlayerAttack.StrongAttack] = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            if (Managers.Game.playData.isStatusLoaded) return;
            Managers.Game.playData.isStatusLoaded = true;
            statusInfo.damages[(int)EPlayerAttack.NormalAttack] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            statusInfo.damages[(int)EPlayerAttack.StrongAttack] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerSkillInfo()
        {
            int index = (int)EContextInfo.SkillInfoStart;
            skillInfo.jumpSkills[(int)EPlayerSkillOpen.Close] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            skillInfo.jumpSkills[(int)EPlayerSkillOpen.Open] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
    }
}