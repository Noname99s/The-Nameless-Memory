using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform trPlayer;
    public Vector2 movePosi = Vector2.zero;
    public EJumpState jumpState = EJumpState.JumpReady;
    public Collider2D jumpCollider;

    public int jumpCount = 0;
    public float maxJumpHeight = 5f;
    public bool isPressJump = false;
    public float jumpPower = 10;
    public float moveSpeed = 5;
}
