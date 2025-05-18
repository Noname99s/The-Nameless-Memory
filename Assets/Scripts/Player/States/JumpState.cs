using UnityEngine;

public class JumpState : PlayerBaseState
{
    public JumpState(PlayerContext playerContext) : base(playerContext) {}
    public override void OnStart()
    {
        Debug.Log("JumpStart");
        playerContext.jumpCollider.enabled = false;
    }
    public override void OnUpdate()
    {
        if (playerContext.jumpState == EJumpState.Jumping) return;
        playerContext.rigid.AddForce(Vector2.up * playerContext.jumpPower, ForceMode2D.Impulse);
        playerContext.jumpState = EJumpState.Jumping;
        playerContext.jumpCollider.enabled = true;
        Debug.Log("JumpUpdate");
    }

    public override void OnExit()
    {
        Debug.Log("JumpExit");
    }
}
