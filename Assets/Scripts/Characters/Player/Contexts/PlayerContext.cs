using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [Header("PlayerInfo")]
    public Rigidbody2D rigid;
    public Rigidbody2D.SlideMovement slideMovement = new Rigidbody2D.SlideMovement();
    public Rigidbody2D.SlideResults slideResult = new Rigidbody2D.SlideResults();
    public SpriteRenderer sprite;
    public Transform trPlayer;
    public Animator animator;
    public int hp;
    public int maxHp;

    [Header("MoveState")]
    public Vector2 moveDir = Vector2.zero;
    public float moveSpeed = 5;

    [Header("JumpState")]
    public EJumpState jumpState = EJumpState.JumpReady;
    public float gravityStrength;
    public float jumpPower = 10;
    public float decelerate = 0.8f;
    public float minJumpSpeed = 0.2f;
    public int jumpCount = 0;
    public bool isPressJump = false;

    [Header("FallState")]
    public bool isFalling = false;
    public float damageFallHeight;
    public float defaultFallSpeed;
    public float maxFallSpeed;
    [HideInInspector] public int fallDamage;
    public int maxFallDamage;


    public bool IsIdleState
    {
        get
        {
            return moveDir == Vector2.zero;
        }
    }

    public bool IsJumpState
    {
        get
        {
            return (isPressJump && jumpState == EJumpState.JumpReady);
        }
    }
    public bool IsFallState
    {
        get
        {
            return (isFalling && jumpState != EJumpState.Jumping);
        }
    }
    public bool IsMoveState
    {
        get
        {
            return moveDir != Vector2.zero;
        }
    }
    public void Initialize()
    {
        hp = maxHp;
        fallDamage = 0;
    }
}
