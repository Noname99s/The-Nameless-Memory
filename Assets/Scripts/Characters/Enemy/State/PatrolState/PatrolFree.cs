using System;
using Orpul1;
using Orpul1.Enemy;
using Orpul1.Enemy.State;
using UnityEngine;

public class PatrolFree : IStrategy
{
    EnemyContext context;
    Action<int> transitionTo;
    int isTurn = 1;
    float patrolEndTime = 0;
    public PatrolFree(EnemyContext context, Action<int> transitionTo)
    {
        this.context = context;
        this.transitionTo = transitionTo;
        isTurn = 1;
    }

    public void Initialize()
    {
        isTurn = -isTurn;
        patrolEndTime = Time.time + context.patrolInfo.moveTime;
    }

    public void Execute()
    {
        MovePosi();
        if (IsArrivePosi())
        {
            transitionTo((int)EEnemyState.Idle);
        }
    }
    private void MovePosi()
    {
        Vector2 dir = Vector2.right * isTurn;
        context.enemyInfo.slideResult = context.enemyInfo.rigid.Slide(Vector2.right * dir.x,
                                                                        Time.deltaTime * context.patrolInfo.patrolMoveSpeed,
                                                                        context.enemyInfo.slideMovement);
        context.enemyInfo.flipX(dir.x);
    }

    private bool IsArrivePosi()
    {
        return Time.time > patrolEndTime;
    }
}
