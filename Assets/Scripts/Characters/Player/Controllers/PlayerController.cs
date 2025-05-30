using Orpul1.State;
using Orpul1.State.Attack;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Orpul1.Player
{
    public class PlayerController : MonoBehaviour,IDamageAble
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerData;
        [SerializeField] CharacterAttack playerAttack;
        [SerializeField] float groundRayDistance = 1f;
        [SerializeField] float wallRayDistance = 1f;
        [SerializeField] float ladderRayDistance = 1f;
        [SerializeField] Vector3 groundRayStartPosi = Vector3.zero;
        [SerializeField] Vector3 wallRayStartPosi = Vector3.zero;
        [SerializeField] LayerMask floorLayer, ladderLayer;
        Ray2D groundRay = new Ray2D();
        Ray2D wallRay = new Ray2D();
        Ray2D ladderRay = new Ray2D();


        //TODO: 플레이어 HP UI연결 시킬 것

        //IInGameView playerHpUI;
        [SerializeField] EPlayerState state;
        private void Awake()
        {
            groundRay.origin = transform.position;
            playerAttack.SetValue(playerData.damage);
            playerData.Initialize();
        }
        void Start()
        {
            //playerHpUI = Managers.UI.GetPopupUI<InGameView>();
            stateController.TransitionState((int)EPlayerState.Idle);
            playerData.slideMovement.gravity = Physics2D.gravity;
            playerData.slideMovement.surfaceAnchor = Vector2.down * 0.1f;
        }

        void Update()
        {
            state = (EPlayerState)stateController.CurState;
            ShootRays();
        }
        void FixedUpdate()
        {
            stateController.ProcessState();
        }
        private void ShootRays()
        {
            OnGround();
            OnAir();
            OnLadder();
        }
        private void OnGround()
        {
            if (!IsHitRay(SetRay(groundRay, groundRayStartPosi, Vector2.down, groundRayDistance, floorLayer.value))) 
            {
                playerData.isFalling = true;
                return;
            }

            playerData.isFalling = false;
            playerData.jumpState = EJumpState.JumpReady;
            playerData.hp -= playerData.fallDamage;
            playerData.fallDamage = 0;
        }
        private void OnAir()
        {
            int flipXValue = playerData.sprite.flipX ? -1 : 1;
            SetWallRayPosi();
            if (!IsHitRay(SetRay(wallRay, wallRayStartPosi, Vector2.right * flipXValue, wallRayDistance, floorLayer.value)))
            {
                playerData.canClimbWall = false;
                return;
            }
            playerData.canClimbWall = true;
        }
        private void OnLadder()
        {
            
            if (SetRay(ladderRay, Vector2.zero, Vector2.up, ladderRayDistance, ladderLayer.value).transform == null)
            {
                playerData.canClimbLadder = false;
                return;
            }
            playerData.canClimbLadder = true;
        }
        private void SetWallRayPosi()
        {
            if (!playerData.sprite.flipX && wallRayStartPosi.x < 0)
                wallRayStartPosi.x *= -1;
            else if (playerData.sprite.flipX && wallRayStartPosi.x > 0)
                wallRayStartPosi.x *= -1;
        }
        private RaycastHit2D SetRay(Ray2D ray, Vector2 rayShootPosi, Vector2 dir,float distance, int detectLayer)
        {
            ray.origin = (Vector2)transform.position + rayShootPosi;
            ray.direction = dir;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
            return Physics2D.Raycast(ray.origin, ray.direction, distance, detectLayer);
        }
        private bool IsHitRay(RaycastHit2D hit2D)
        {
            if (hit2D.transform == null) return false;
            if (!hit2D.transform.CompareTag(ETag.Floor.ToString())) return false;

            return true;
        }

        public void OnDamage(int damage)
        {
            playerData.hp -= damage;
            
            playerData.isHit = playerData.hp > 0;
            playerData.isDeath = !playerData.isHit;
        }
    }

}