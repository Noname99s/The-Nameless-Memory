using Orpul1.Camera;
using Orpul1.State;
using System;
using System.Collections;
using UnityEngine;

namespace Orpul1.Player
{
    [Serializable]
    public struct PlayerRayInfo
    {
        public float groundRayDistance;
        public float wallRayDistance;
        public float ladderRayDistance;
        public Vector3 groundRayStartPosi;
        public Vector3 wallRayStartPosi;
        public LayerMask floorLayer, ladderLayer;
        public Ray2D groundRay;
        public Ray2D wallRay;
        public Ray2D ladderRay;
        public RaycastHit2D groundHit;
    }
    public class PlayerController : MonoBehaviour,IDamageAble, IknockbackAble, IGetValue<Transform>
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerData;
        [SerializeField] Transform targetPosition;
        [SerializeField] PlayerRayInfo rayInfo;
        WaitForSeconds delayInvincible;
        WaitForSeconds delayCameraShake;
        ICameraEffectAble cameraEffect;
        ISetCamerGroupAble cameraGroup;
        //TODO: 플레이어 HP UI연결 시킬 것
        public event System.Action<int ,int> OnPlayerHpChanged;

        [SerializeField] EPlayerState state;
        private void Awake()
        {
            rayInfo.groundRay.origin = transform.position;
            playerData.Initialize();
            
            playerData.playerInfo.actionFlipX = FlipX;
            playerData.playerInfo.dectectedGroundPosi = transform.position;
            delayInvincible = new WaitForSeconds(playerData.hitInfo.invincibleTime);
            delayCameraShake = new WaitForSeconds(playerData.hitInfo.cameraShakeDuration);
            PlayerManager.Instance.SetPlayer(gameObject);
        }
        void Start()
        {
            OnPlayerHpChanged += Managers.UI.GetPopupUI<InGameComposition>().OnHpUpdateConnection;
            CameraManager.Instance.SetCameraEvent(SetCameraEvent);
            PlayerManager.Instance.SetPlayer(gameObject);
            SetCameraEvent();
            stateController.TransitionState((int)EPlayerState.Idle);
            playerData.moveInfo.slideMovement.gravity = playerData.moveInfo.slideMoveGravity;
            playerData.moveInfo.slideMovement.surfaceAnchor = Vector2.down * 0.1f;
        }
        private void SetCameraEvent()
        {
            Transform camera = CameraManager.Instance.GetCamera<Transform>((int)ECameraType.MainCamera);
            if (camera == null) return;
            cameraEffect = camera.GetComponent<ICameraEffectAble>();
            cameraGroup = camera.GetComponent<ISetCamerGroupAble>();
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
            rayInfo.groundHit = SetRay(rayInfo.groundRay, rayInfo.groundRayStartPosi, Vector2.down, rayInfo.groundRayDistance, rayInfo.floorLayer.value);
            if (!IsHitRay(rayInfo.groundHit)) 
            {
                playerData.fallInfo.isFalling = true;
                return;
            }
            playerData.fallInfo.isFalling = false;
            playerData.playerInfo.hp -= playerData.fallInfo.fallDamage;
            playerData.jumpInfo.jumpCount = 2;
            playerData.playerInfo.dectectedGroundPosi = rayInfo.groundHit.point;
            playerData.fallInfo.fallDamage = 0;
        }
        private void OnAir()
        {
            SetWallRayPosi();
            if (!IsHitRay(SetRay(rayInfo.wallRay, rayInfo.wallRayStartPosi, Vector2.right * transform.localScale.x, rayInfo.wallRayDistance, rayInfo.floorLayer.value))) 
            {
                playerData.moveInfo.canClimbWall = false;
                return;
            }
            playerData.moveInfo.canClimbWall = true;
        }
        private void OnLadder()
        {
            
            if (SetRay(rayInfo.ladderRay, Vector2.zero, Vector2.up, rayInfo.ladderRayDistance, rayInfo.ladderLayer.value).transform == null)
            {
                playerData.moveInfo.canClimbLadder = false;
                return;
            }
            playerData.moveInfo.canClimbLadder = true;
        }
        private void SetWallRayPosi()
        {
            if (!playerData.playerInfo.sprite.flipX && rayInfo.wallRayStartPosi.x < 0)
                rayInfo.wallRayStartPosi.x *= -1;
            else if (playerData.playerInfo.sprite.flipX && rayInfo.wallRayStartPosi.x > 0)
                rayInfo.wallRayStartPosi.x *= -1;
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

            return true;
        }
        private void FlipX()
        {
            if (playerData.moveInfo.moveDir.x == 0) return;

            Vector3 scale = playerData.playerInfo.originScale;
            scale.x = Mathf.RoundToInt(playerData.moveInfo.moveDir.x);
            playerData.playerInfo.trPlayer.localScale = scale;
        }
        public void RecoverHp(bool max , int recoverAmount = 0)
        {
            if (playerData.playerInfo.hp <= 0) return;
            if (max)
                recoverAmount = playerData.playerInfo.maxHp - playerData.playerInfo.hp;
            playerData.playerInfo.hp += recoverAmount;
            // Ensure hp does not exceed maxHp
            playerData.playerInfo.hp = Mathf.Clamp(playerData.playerInfo.hp, 0, playerData.playerInfo.maxHp);
            OnPlayerHpChanged?.Invoke(playerData.playerInfo.hp, playerData.playerInfo.maxHp);

        }
        public void OnDamage(int damage)
        {
            if (!playerData.hitInfo.canGetDamage) return;
            OnPlayerHpChanged?.Invoke(playerData.playerInfo.hp, playerData.playerInfo.maxHp);

            playerData.playerInfo.hp -= damage;
            playerData.hitInfo.isHit = playerData.playerInfo.hp > 0;
            playerData.hitInfo.isDeath = !playerData.hitInfo.isHit;
            StartCoroutine("CountInvincibleTime");
            StartCoroutine("CameraShake");
        }

        public void knockback(Vector2 dir)
        {
            if (!playerData.hitInfo.canGetDamage) return;
            playerData.playerInfo.rigid.linearVelocity = Vector2.zero;
            playerData.playerInfo.rigid.AddForce(dir * playerData.hitInfo.knockBackPower, ForceMode2D.Impulse);
        }
        IEnumerator CameraShake()
        {
            cameraEffect.Execute((int)ECameraEffect.Shake);
            yield return delayCameraShake;
            cameraEffect.Stop((int)ECameraEffect.Shake);
            
        }
        IEnumerator CountInvincibleTime()
        {
            playerData.hitInfo.canGetDamage = false;
            yield return delayInvincible;
            playerData.hitInfo.canGetDamage = true;
        }

        /// <summary>
        /// 위치 정보 전달
        /// </summary>
        /// <returns></returns>
        public Transform GetValue()
        {
            return targetPosition;
        }
    }
}