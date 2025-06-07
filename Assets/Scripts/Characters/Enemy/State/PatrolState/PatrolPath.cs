using System;
using Orpul1;
using Orpul1.Enemy;
using Orpul1.Enemy.State;
using UnityEngine;

public class PatrolPath : IStrategy
{
    EnemyContext context;
    Action<int> transitionTo;
    int moveIndex = 0;

    public PatrolPath(EnemyContext context, Action<int> transitionTo)
    {
        this.context = context;
        this.transitionTo = transitionTo;
        moveIndex = 0;
    }
    public void Initialize()
    {
    }

    public void Execute()
    {
        MovePosi();
        if (IsArrivePosi())
        {
            moveIndex = (moveIndex + 1) % context.patrolInfo.movePosis.Length;
            transitionTo((int)EEnemyState.Idle);
        }
    }
    private void MovePosi()
    {
        Vector2 dir = (context.patrolInfo.movePosis[moveIndex].position - context.transform.position).normalized;
        context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x,
                                                                        Time.deltaTime * context.patrolInfo.patrolMoveSpeed,
                                                                        context.enemyInfo.slideMovement);
        context.enemyInfo.flipX(dir.x);
    }

    private bool IsArrivePosi()
    {
        float distance = Mathf.Abs(context.patrolInfo.movePosis[moveIndex].position.x - context.transform.position.x);
        if (distance > 0.1) return false;
        return true;
    }

}
