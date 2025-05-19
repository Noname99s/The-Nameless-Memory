using System;
using Orpul1.State;
using UnityEngine;

namespace Orpul1.Pet.State
{
    public class FollowState : PetBaseState
    {
        public FollowState(PetContext context, Action<int> transitionTo) : base(context, transitionTo) { }

        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            float targetDistance = Vector2.Distance(context.rigid.position, context.target.position);
            if (targetDistance > context.targetDistanceMaxGap)
            {
                context.transform.position = context.target.position;
                return;
            }
            if (targetDistance <= context.targetDistanceGap)
            {
                return;
            }
            Vector2 dir = (Vector2)context.target.position - context.rigid.position;
            //context.transform.position = Vector2.Lerp(context.transform.position, context.target.position, t * context.moveSpeed);
            context.rigid.linearVelocityX = (dir.normalized * context.moveSpeed * Time.deltaTime).x;
        }
        public override void OnExit()
        {
        }
    }
}