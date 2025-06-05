using Orpul1.Player.Animation;

namespace Orpul1.Player.State.Jump
{
    public class WallJump : IStrategy
    {
        PlayerContext data;

        float moveX = 0;
        float jumpDir = 0;
        float jumpAmount = 0;
        public WallJump(PlayerContext data)
        {
            this.data = data;
        }
        public void Initialize()
        {
            jumpAmount = data.jumpInfo.jumpPower;
            jumpDir = (int)-(data.playerInfo.trPlayer.localScale.x / data.playerInfo.originScale.x);
            data.jumpInfo.jumpCount = 1;
            data.playerInfo.animator.SetBool(EPlayerAniTransition.IsGround.ToString(), false);
            data.playerInfo.animator.SetTrigger(EPlayerAniTransition.WallJumpTrigger.ToString());
            data.jumpInfo.jumpState = EJumpState.Jumping;
        }
        public void Execute()
        {
            DoJump();
            AirMove();
            if (jumpAmount <= 0)
            {
                data.jumpInfo.jumpState = EJumpState.JumpEnd;
            }
        }

        private void DoJump()
        {
            if (!data.jumpInfo.isPressJump)
                jumpAmount *= data.jumpInfo.decelerate;

            jumpAmount *= data.jumpInfo.decelerate;
            data.playerInfo.rigid.linearVelocityY = jumpAmount;

            if (data.playerInfo.rigid.linearVelocityY < data.jumpInfo.minJumpSpeed)
            {
                data.jumpInfo.jumpState = EJumpState.JumpEnd;
                data.jumpInfo.isPressJump = false;
                jumpAmount = 0;
            }
        }
        private void AirMove()
        {
            moveX = jumpDir * data.fallInfo.onAirMoveSpeed;
            data.playerInfo.rigid.linearVelocityX = moveX;
        }
    }
}