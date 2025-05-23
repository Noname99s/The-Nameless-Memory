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
            jumpAmount = data.jumpPower;

            data.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.animator.SetTrigger(EPlayerAni.WallJumpTrigger.ToString());
        }
        public void Execute()
        {
            DoJump();
        }

        private void DoJump()
        {
            if (!data.isPressJump || data.jumpState == EJumpState.JumpEnd) return;
            jumpAmount *= data.decelerate;
            data.jumpState = EJumpState.Jumping;
            data.rigid.linearVelocityY = jumpAmount;

            if (data.rigid.linearVelocityY < data.minJumpSpeed)
            {
                data.jumpState = EJumpState.JumpEnd;
                data.isPressJump = false;
                jumpAmount = 0;
            }
        }
    }
}