using UnityEngine;
namespace Orpul1.Player.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerContext playerContext) : base(playerContext) {}

        public override void OnStart()
        {
        }
        public override void OnUpdate()
        {
            Debug.Log($"{EPlayerState.Idle.ToString()}");

        }
        public override void OnExit()
        {
        }
    }
}
