using Orpul1;
using Orpul1.Player;
using Orpul1.State.Attack;
using System;
using UnityEngine;

[Serializable]
public class PlayerAttackInfo
{
    public EPlayerAttack attackType = EPlayerAttack.NormalAttack;
    public float[] attackDelays = new float[3];
    public int[] attackCounts = new int[3];
    public int[] damages = new int[2];
    public int curAttack = 0;


    public bool isPressAttack;
    public bool canAttack;
    public float delayInterval;
    public bool isStillAttack;
    [SerializeField] CharacterAttack characterAttack;
    public ISetDataAble<int> setDamage
    {
        get { return characterAttack; }
    }
    public void Initialize()
    {
        attackType = EPlayerAttack.NONE;
        curAttack = 0;
    }
}
[Serializable]
public class PlayerInfo
{
    public Rigidbody2D rigid;
    
    public Collider2D playerCollider;
    public SpriteRenderer sprite;
    public Transform trPlayer;
    public Animator animator;
    public Vector3 originScale = Vector3.one;
    public int hp;
    public int maxHp;
    public int deathCount;
    public Vector3 dectectedGroundPosi;
    public Action actionFlipX;
}

[Serializable]
public class PlayerMoveInfo
{
    public Rigidbody2D.SlideMovement slideMovement = new Rigidbody2D.SlideMovement();
    public Rigidbody2D.SlideResults slideResult = new Rigidbody2D.SlideResults();

    [Header("MoveInfo")]
    public Vector2 moveDir = Vector2.zero;
    public Vector2 slideMoveGravity = new Vector2(0,-20);
    public float moveSpeed = 5;

    [Header("ClimbLadder")]
    public bool isPressClimbLadder;
    public bool canClimbLadder;

    [Header("WallClimbState")]
    public bool canClimbWall;
    public bool isPressWallClimb;
    public float climbSpeed;
}

[Serializable]
public class PlayerJumpInfo
{
    public EJumpState jumpState = EJumpState.JumpReady;
    public float gravityStrength;
    public float jumpPower = 10;
    public float decelerate = 0.8f;
    public float minJumpSpeed = 0.2f;
    public int jumpCount = 0;
    public bool isPressJump = false;
    public bool shouldWallJump = false;
}

[Serializable]
public class PlayerFallInfo
{
    public bool isFalling = false;
    public float defaultFallSpeed;
    public float fallDamageOverTime;
    public float onAirMoveSpeed = 2;
    [HideInInspector] public int fallDamage;
    public int maxFallDamage;
}

[Serializable]
public class PlayerHitInfo
{
    public float knockBackPower;
    public float invincibleTime = 0.5f;
    public float hitDuration = 1f;
    public float cameraShakeDuration = 0;
    public bool canGetDamage = true;
    public bool isHit = false;
    public bool isDeath = false;
}
public class PlayerContext : MonoBehaviour
{
    [SerializeField] PlayerContextDB contextDB;
    public PlayerInfo playerInfo;

    public PlayerMoveInfo moveInfo;

    public PlayerJumpInfo jumpInfo;

    public PlayerFallInfo fallInfo;

    public PlayerAttackInfo attackInfo;

    public PlayerHitInfo hitInfo;
    private void Awake()
    {
        //SetContextInfos();
    }
    private void SetContextInfos()
    {
        contextDB.SetPlayerInfo(playerInfo);
        contextDB.SetPlayerMoveInfo(moveInfo);
        contextDB.SetPlayerJumpInfo(jumpInfo);
        contextDB.SetPlayerFallInfo(fallInfo);
        contextDB.SetPlayerAttackInfo(attackInfo);
        contextDB.SetPlayerHitInfo(hitInfo);
    }
    public bool IsIdleState
    {
        get
        {
            return moveInfo.moveDir.x == 0 && !fallInfo.isFalling;
        }
    }

    public bool IsJumpState
    {
        get
        {
            return (jumpInfo.isPressJump && jumpInfo.jumpState == EJumpState.JumpReady && jumpInfo.jumpCount > 0);
        }
    }
    public bool IsFallState
    {
        get
        {
            return (fallInfo.isFalling && jumpInfo.jumpState != EJumpState.Jumping);
        }
    }
    public bool IsMoveState
    {
        get
        {
            return moveInfo.moveDir.x != 0 && !fallInfo.isFalling;
        }
    }
    public bool IsAttackState
    {
        get
        {
            return attackInfo.isPressAttack && !fallInfo.isFalling;
        }
    }
    public bool IsWallClimbState
    {
        get
        {
            return moveInfo.isPressWallClimb && moveInfo.canClimbWall;
        }
    }
    public bool IsClimbLadderState
    {
        get
        {
            return moveInfo.isPressClimbLadder && moveInfo.canClimbLadder;
        }
    }
    public void Initialize()
    {
        playerInfo.hp = playerInfo.maxHp;
        fallInfo.fallDamage = 0;
        attackInfo.attackType = EPlayerAttack.NONE;
        playerInfo.originScale = transform.localScale;
        hitInfo.canGetDamage = true;
    }
}
