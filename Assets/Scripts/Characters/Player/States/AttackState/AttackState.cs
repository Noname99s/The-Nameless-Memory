using System;
using UnityEngine;
using Orpul1.Player.State.Attack;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
namespace Orpul1.Player.State
{
    public class AttackState : PlayerBaseState
    {
        IStrategy[] attackCommands;
        EPlayerAttack prevAttack;
        float attackDelay = 0;
        float nextComboTime = 0;
        public AttackState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) 
        {
            attackCommands = new IStrategy[3]
            {
                new NormalAttack(playerContext),
                new StrongAttack(playerContext),
                new Guard(playerContext),
            };
        }

        public override void OnStart()
        {
            Debug.Log("StartAttack");
            if ((int)data.attackInfo.attackType >= attackCommands.Length)
                data.attackInfo.attackType = EPlayerAttack.NormalAttack;
            data.playerInfo.rigid.gravityScale = 0;
            Initialize();
            SetAnimation();
        }

        public override void OnUpdate()
        {
            if(IsChangeAttackType()) return;
            Attack();
            SetReadyNextAttack();
        }

        public override void OnExit()
        {
            Debug.Log("EndAttack");
            data.attackInfo.Initialize();
             
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsAttack.ToString(), false);
            data.playerInfo.animator.SetInteger(EPlayerAniTransition.AttackIndex.ToString(), 0);
            data.playerInfo.rigid.gravityScale = data.jumpInfo.gravityStrength;
        }

        private void Initialize()
        {
            attackCommands[(int)data.attackInfo.attackType].Initialize();
            data.playerInfo.setDamage.SetValue((int)data.statusInfo.damages[(int)data.attackInfo.attackType]);
            prevAttack = data.attackInfo.attackType;
            data.attackInfo.canAttack = true;
            nextComboTime = 0;
            attackDelay = 0;
        }
        private void SetAnimation()
        {
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsAttack.ToString(), false);
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsAttack.ToString(), true);
        }
        private void Attack()
        {
            attackCommands[(int)data.attackInfo.attackType].Execute();

            if (Time.time < nextComboTime) return;
            if (!data.attackInfo.canAttack)
            {
                TransitionState();
                return;
            }
            PrepareNextComboAttack();
        }
        private void PrepareNextComboAttack()
        {
            attackDelay = Time.time;
            nextComboTime = attackDelay + data.attackInfo.attackDelays[(int)data.attackInfo.attackType];
            data.attackInfo.canAttack = false;
            data.attackInfo.isPressAttack = false;
        }
        
        private void SetReadyNextAttack()
        {
            if (data.attackInfo.canAttack) return;
            if (!CanComboAttack())
                return;

            data.attackInfo.canAttack = data.attackInfo.isPressAttack;
        }

        private bool CanComboAttack()
        {
            if (Time.time > nextComboTime)
                return false;
            
            return true;
        }
       
        private bool IsChangeAttackType()
        {
            if (prevAttack == data.attackInfo.attackType) return false;
            Debug.Log("Diff Attack State");
            data.attackInfo.canAttack = false;
            TransitionState();
            return true;
        }

        private void TransitionState()
        {
            if (IsHitState()) return;
            if (IsDeathState()) return;
            if (IsFallState()) return;
            transitionTo((int)EPlayerState.Idle);
        }

        private bool IsHitState()
        {
            if (!data.hitInfo.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }

        private bool IsDeathState()
        {
            if (!data.IsDieState) return false;
            transitionTo((int)EPlayerState.Death);

            return true;
        }

        private bool IsFallState()
        {
            if (!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }

    }
}
