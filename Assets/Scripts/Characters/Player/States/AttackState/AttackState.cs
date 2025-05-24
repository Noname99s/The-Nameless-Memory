using System;
using UnityEngine;
using Orpul1.Player.State.Attack;
using Orpul1.Player.Animation;
using Orpul1.State.Attack;
using Mono.Cecil;
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
            if ((int)data.attackContext.attackType >= attackCommands.Length)
                data.attackContext.attackType = EPlayerAttack.NormalAttack;

            attackCommands[(int)data.attackContext.attackType].Initialize();
            data.animator.SetBool(EPlayerAni.IsAttack.ToString(), false);
            data.animator.SetBool(EPlayerAni.IsAttack.ToString(), true);
            prevAttack = data.attackContext.attackType;
            data.canAttack = true;
            nextComboTime = 0;
            attackDelay = 0;
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
            data.attackContext.Initialize();
             
            data.animator.SetBool(EPlayerAni.IsAttack.ToString(), false);
            data.animator.SetInteger(EPlayerAni.AttackIndex.ToString(), 0);
        }

        private void Attack()
        {
            attackCommands[(int)data.attackContext.attackType].Execute();

            if (Time.time < nextComboTime) return;
            if (!data.canAttack)
            {
                TransitionState();
                return;
            }
            PrepareNextComboAttack();
        }
        private void PrepareNextComboAttack()
        {
            attackDelay = Time.time;
            nextComboTime = attackDelay + data.attackContext.attackDelays[(int)data.attackContext.attackType];
            data.canAttack = false;
            data.isPressAttack = false;
        }
        
        private void SetReadyNextAttack()
        {
            if (data.canAttack) return;
            if (!CanComboAttack())
                return;

            data.canAttack = data.isPressAttack;
        }

        private bool CanComboAttack()
        {
            if (Time.time > nextComboTime)
                return false;
            
            return true;
        }
       
        private bool IsChangeAttackType()
        {
            if (prevAttack == data.attackContext.attackType) return false;
            Debug.Log("Diff Attack State");
            data.canAttack = false;
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
            if (!data.isHit) return false;
            transitionTo((int)EPlayerState.Hit);

            return true;
        }

        private bool IsDeathState()
        {
            if (!data.isDeath) return false;
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
