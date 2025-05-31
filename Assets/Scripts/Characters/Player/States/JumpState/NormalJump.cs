using Orpul1.Player.Animation;

namespace Orpul1.Player.State.Jump
{
    public class NormalJump : IStrategy
    {
        PlayerContext data;
        float jumpAmount = 0;
        public NormalJump(PlayerContext data)
        {
            this.data = data;
        }
        public void Initialize()
        {
            jumpAmount = data.jumpInfo.jumpPower;

            data.playerInfo.animator.SetBool(EPlayerAni.IsGround.ToString(), false);
            data.playerInfo.animator.SetTrigger(EPlayerAni.JumpTrigger.ToString());
            data.jumpInfo.jumpState = EJumpState.Jumping;
        }
        public void Execute()
        {
            DoJump();
            if (jumpAmount <= 0)
                data.jumpInfo.jumpState = EJumpState.JumpEnd;
        }

        
        private void DoJump()
        {
            //if (!data.jumpInfo.isPressJump || data.jumpInfo.jumpState == EJumpState.JumpEnd) return;
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
    }
}