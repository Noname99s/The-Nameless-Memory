using Orpul1.State;
using UnityEngine;

namespace Orpul1.Player.State
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerContext playerContext) : base(playerContext) {}
        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            Debug.Log($"{EPlayerState.Move.ToString()}");
            Vector2 movePosi = (Vector2)playerContext.trPlayer.position + playerContext.movePosi * playerContext.moveSpeed * Time.deltaTime;
            playerContext.rigid.MovePosition(movePosi);

        }
        public override void OnExit()
        {
        }
    }

}
