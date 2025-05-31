using Orpul1.Player.Animation;

namespace Orpul1.Player.State.Jump
{
    public class WallJump : IStrategy
    {
        PlayerContext data;
        float jumpAmount = 0;
        public WallJump(PlayerContext data)
        {
            this.data = data;
        }
        public void Initialize()
        {
            jumpAmount = data.jumpInfo.jumpPower;

            data.playerInfo.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.playerInfo.animator.SetTrigger(EPlayerAni.WallJumpTrigger.ToString());
        }
        public void Execute()
        {
            DoJump();
        }

        private void DoJump()
        {
            if (!data.jumpInfo.isPressJump || data.jumpInfo.jumpState == EJumpState.JumpEnd) return;
            jumpAmount *= data.jumpInfo.decelerate;
            data.jumpInfo.jumpState = EJumpState.Jumping;
            data.playerInfo.rigid.linearVelocityY = jumpAmount;

            if (data.playerInfo.rigid.linearVelocityY < data.jumpInfo.minJumpSpeed)
            {
                data.jumpInfo.jumpState = EJumpState.JumpEnd;
                data.jumpInfo.isPressJump = false;
                jumpAmount = 0;
            }
        }
    }
}