using Orpul1.State;
using Orpul1.State.Attack;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Orpul1.Player
{
    public class PlayerController : MonoBehaviour,IDamageAble, IknockbackAble
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerData;
        [SerializeField] float groundRayDistance = 1f;
        [SerializeField] float wallRayDistance = 1f;
        [SerializeField] float ladderRayDistance = 1f;
        [SerializeField] Vector3 groundRayStartPosi = Vector3.zero;
        [SerializeField] Vector3 wallRayStartPosi = Vector3.zero;
        [SerializeField] LayerMask floorLayer, ladderLayer;
        Ray2D groundRay = new Ray2D();
        Ray2D wallRay = new Ray2D();
        Ray2D ladderRay = new Ray2D();
        WaitForSeconds delayInvincible;
        //TODO: 플레이어 HP UI연결 시킬 것

        //IInGameView playerHpUI;
        [SerializeField] EPlayerState state;
        private void Awake()
        {
            groundRay.origin = transform.position;
            playerData.Initialize();
            
            playerData.playerInfo.actionFlipX = FlipX;
            delayInvincible = new WaitForSeconds(playerData.playerInfo.invincibleTime);
        }
        void Start()
        {
            //playerHpUI = Managers.UI.GetPopupUI<InGameView>();
            stateController.TransitionState((int)EPlayerState.Idle);
            playerData.playerInfo.slideMovement.gravity = Physics2D.gravity;
            playerData.playerInfo.slideMovement.surfaceAnchor = Vector2.down * 0.1f;
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
                playerData.fallInfo.isFalling = true;
                return;
            }

            playerData.fallInfo.isFalling = false;
            playerData.jumpInfo.jumpState = EJumpState.JumpReady;
            playerData.playerInfo.hp -= playerData.fallInfo.fallDamage;
            playerData.fallInfo.fallDamage = 0;
        }
        private void OnAir()
        {
            SetWallRayPosi();
            if (!IsHitRay(SetRay(wallRay, wallRayStartPosi, Vector2.right * transform.localScale.x, wallRayDistance, floorLayer.value))) 
            {
                playerData.moveInfo.canClimbWall = false;
                return;
            }
            playerData.moveInfo.canClimbWall = true;
        }
        private void OnLadder()
        {
            
            if (SetRay(ladderRay, Vector2.zero, Vector2.up, ladderRayDistance, ladderLayer.value).transform == null)
            {
                playerData.moveInfo.canClimbLadder = false;
                return;
            }
            playerData.moveInfo.canClimbLadder = true;
        }
        private void SetWallRayPosi()
        {
            if (!playerData.playerInfo.sprite.flipX && wallRayStartPosi.x < 0)
                wallRayStartPosi.x *= -1;
            else if (playerData.playerInfo.sprite.flipX && wallRayStartPosi.x > 0)
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
        private void FlipX()
        {
            if (playerData.moveInfo.moveDir.x == 0) return;

            Vector3 scale = playerData.playerInfo.originScale;
            scale.x = Mathf.RoundToInt(playerData.moveInfo.moveDir.x);
            playerData.playerInfo.trPlayer.localScale = scale;
        }
        public void OnDamage(int damage)
        {
            if (!playerData.playerInfo.canGetDamage) return;

            playerData.playerInfo.hp -= damage;
            playerData.playerInfo.isHit = playerData.playerInfo.hp > 0;
            playerData.playerInfo.isDeath = !playerData.playerInfo.isHit;
            StartCoroutine("CountInvincibleTime");
        }

        public void knockback(Vector2 dir)
        {
            if (!playerData.playerInfo.canGetDamage) return;
            playerData.playerInfo.rigid.linearVelocity = Vector2.zero;
            playerData.playerInfo.rigid.AddForce(dir * playerData.playerInfo.knockBackPower, ForceMode2D.Impulse);
        }

        IEnumerator CountInvincibleTime()
        {
            playerData.playerInfo.canGetDamage = false;
            yield return delayInvincible;
            playerData.playerInfo.canGetDamage = true;
        }
    }

}