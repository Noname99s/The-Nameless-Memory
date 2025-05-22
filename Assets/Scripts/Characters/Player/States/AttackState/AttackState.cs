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
        ICommand[] attackCommands;
        EPlayerAttack prevAttack;
        bool canAttack = true;
        float delayInterval = 0;
        public AttackState(PlayerContext playerContext, Action<int> transitionTo) : base(playerContext, transitionTo) 
        {
            attackCommands = new ICommand[3]
            {
                new NormalAttack(playerContext),
                new StrongAttack(playerContext),
                new Guard(playerContext),
            };
        }

        public override void OnStart()
        {
            data.animator.SetBool(EPlayerAni.IsAttack.ToString(), true);
            delayInterval = data.attackContext.attackDelays[(int)data.attackContext.attackType];
            prevAttack = data.attackContext.attackType;
            data.canAttack = true;
        }

        public override void OnUpdate()
        {
            ReadyAttack();
            Attack();
            ChangeAttackType();
        }

        public override void OnExit()
        {
            data.attackContext.Initialize();

            data.animator.SetBool(EPlayerAni.IsAttack.ToString(), false);
            data.animator.SetInteger(EPlayerAni.NormalAttackIndex.ToString(), 0);
        }

        private void Attack()
        {
            attackCommands[(int)data.attackContext.attackType].Execute();

            if (data.canAttack|| data.isAttack)
            {
                return;
            }
            TransitionState();
        }
        private void ChangeAttackType()
        {
            if (prevAttack == data.attackContext.attackType) return;
            data.attackContext.Initialize();
            
            prevAttack = data.attackContext.attackType;
        }
        private void ReadyAttack()
        {
            if (data.canAttack) return;

            float firstFrameKey = data.attackContext.attackDelays[(int)data.attackContext.attackType] * 0.5f;
            float lastFrameKey = data.attackContext.attackDelays[(int)data.attackContext.attackType];
            bool isDetectKeySection = (data.delayInterval >= firstFrameKey && data.delayInterval <= lastFrameKey);
            data.canAttack = isDetectKeySection && data.isAttack;
        }
        private void TransitionState()
        {
            if (IsFallState()) return;
            transitionTo((int)EPlayerState.Idle);
        }
        private bool IsFallState()
        {
            if (!data.IsFallState) return false;
            transitionTo((int)EPlayerState.Fall);

            return true;
        }

    }
}
