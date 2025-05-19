using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [Header("PlayerInfo")]
    public Rigidbody2D rigid;
    public Rigidbody2D.SlideMovement slideMovement = new Rigidbody2D.SlideMovement();
    public Rigidbody2D.SlideResults slideResult = new Rigidbody2D.SlideResults();
    public Transform trPlayer;
    public int hp;
    public int maxHp;

    [Header("MoveState")]
    public Vector2 movePosi = Vector2.zero;
    public float moveSpeed = 5;

    [Header("JumpState")]
    public EJumpState jumpState = EJumpState.JumpReady;
    public float gravityStrength;
    public float maxJumpHeight = 5f;
    public float jumpPower = 10;
    public int jumpCount = 0;
    public bool isPressJump = false;

    [Header("FallState")]
    public bool isFalling = false;
    public float damageFallHeight;
    public float maxFallSpeed;
    [HideInInspector] public int fallDamage;
    public int maxFallDamage;

    public void Initialize()
    {
        hp = maxHp;
        fallDamage = 0;
    }
}
