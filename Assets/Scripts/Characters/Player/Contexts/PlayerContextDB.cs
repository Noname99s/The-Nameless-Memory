using UnityEngine;
using Orpul1.State.Attack;
namespace Orpul1.Player
{
    [CreateAssetMenu(fileName = "PlayerContextDB", menuName = "DataBase/PlayerContextDB")]
    public class PlayerContextDB : ScriptableObject
    {
        enum EContextInfo
        {
            START = 0,
            InfoStart = 0,
            InfoEnd = 0,
            MoveInfoStart = 1,
            MoveInfoEnd = 2,
            JumpInfoStart = 3,
            JumpInfoEnd = 7,
            FallInfoStart = 8,
            FallInfoEnd = 11,
            HitInfoStart = 12,
            HitInfoEnd = 15,
            AttackInfoStart = 16,
            AttackInfoEnd = 19,
            END = 0
        }
        /*[SerializeField] PlayerInfo playerInfo = new PlayerInfo();
        [SerializeField] PlayerMoveInfo moveInfo = new PlayerMoveInfo();
        [SerializeField] PlayerJumpInfo jumpInfo = new PlayerJumpInfo();
        [SerializeField] PlayerFallInfo fallInfo = new PlayerFallInfo();
        [SerializeField] PlayerHitInfo hitInfo = new PlayerHitInfo();
        [SerializeField] PlayerAttackInfo attackInfo = new PlayerAttackInfo();

        public PlayerInfo PlayerInfo { get { return playerInfo; } }
        public PlayerMoveInfo MoveInfo { get {return moveInfo; } }
        public PlayerJumpInfo JumpInfo { get {return jumpInfo; } }
        public PlayerFallInfo FallInfo { get { return fallInfo; } }
        public PlayerHitInfo HitInfo { get { return hitInfo; } }
        public PlayerAttackInfo AttackInfo { get { return attackInfo; } }

        [ContextMenu("Set PlayerDB Data")]*/
        /*public void SetData()
        {
            SetPlayerInfo();
            SetPlayerMoveInfo();
            SetPlayerJumpInfo();
            SetPlayerFallInfo();
            SetPlayerHitInfo();
            SetPlayerAttackInfo();
        }*/
        public void SetPlayerInfo(PlayerInfo playerInfo)
        {
            int index = (int)EContextInfo.InfoStart;
            playerInfo.hp = (int)BG_DB_PlayerData.GetEntity(index)._InitialValue;
        }
        public void SetPlayerMoveInfo(PlayerMoveInfo moveInfo)
        {
            int index = (int)EContextInfo.MoveInfoStart;
            moveInfo.moveSpeed = BG_DB_PlayerData.GetEntity(index)._InitialValue;
            moveInfo.climbSpeed = BG_DB_PlayerData.GetEntity(index + 1)._InitialValue;
        }
        public void SetPlayerJumpInfo(PlayerJumpInfo jumpInfo)
        {
            int index = (int)EContextInfo.JumpInfoStart;
            jumpInfo.gravityStrength = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.jumpPower= BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.decelerate = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.minJumpSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            jumpInfo.jumpCount = (int)BG_DB_PlayerData.GetEntity(index)._InitialValue;
        }
        public void SetPlayerFallInfo(PlayerFallInfo fallInfo)
        {
            int index = (int)EContextInfo.FallInfoStart;
            fallInfo.defaultFallSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.fallDamageOverTime = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.onAirMoveSpeed = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            fallInfo.maxFallDamage = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerHitInfo(PlayerHitInfo hitInfo)
        {
            int index = (int)EContextInfo.HitInfoStart;
            hitInfo.knockBackPower = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.invincibleTime = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.hitDuration = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            hitInfo.cameraShakeDuration = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
        }
        public void SetPlayerAttackInfo(PlayerAttackInfo attackInfo)
        {
            int index = (int)EContextInfo.AttackInfoStart;
            attackInfo.attackDelays[(int)EPlayerAttack.NormalAttack] = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            attackInfo.attackDelays[(int)EPlayerAttack.StrongAttack] = BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            attackInfo.damages[(int)EPlayerAttack.NormalAttack] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;
            attackInfo.damages[(int)EPlayerAttack.StrongAttack] = (int)BG_DB_PlayerData.GetEntity(index++)._InitialValue;

        }
    }
}