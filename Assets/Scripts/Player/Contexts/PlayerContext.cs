using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform trPlayer;
    public Vector2 movePosi = Vector2.zero;
    public EJumpState jumpState = EJumpState.Jumping;
    public Collider2D jumpCollider;
    
    public bool isJump = false;
    public float jumpPower = 10;
    public float moveSpeed = 5;
}
