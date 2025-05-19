using Orpul1.State;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Orpul1.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] StateController stateController;
        [SerializeField] PlayerContext playerData;
        [SerializeField] float rayDistance = 1f;
        [SerializeField] Vector3 rayPosi = Vector3.zero;
        [SerializeField] LayerMask floorLayer;
        Ray2D ray = new Ray2D();
        RaycastHit2D rayHit = new RaycastHit2D();
        private void Awake()
        {
            ray.origin = transform.position;
            playerData.Initialize();
        }
        void Start()
        {
            stateController.TransitionState((int)EPlayerState.Idle);
            playerData.slideMovement.gravity = Physics2D.gravity;
            playerData.slideMovement.surfaceAnchor = Vector2.down * 0.1f;
        }

        void Update()
        {
            OnGround();
        }
        private void OnGround()
        {
            SetRay();

            if (!IsGround())
            {
                playerData.isFalling = true;
                return;
            }

            playerData.isFalling = false;
            playerData.jumpState = EJumpState.JumpReady;
            playerData.hp -= playerData.fallDamage;
            playerData.fallDamage = 0;
        }

        private void SetRay()
        {
            ray.origin = transform.position + rayPosi;
            ray.direction = Vector2.down;
            rayHit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, floorLayer.value);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);
        }
        private bool IsGround()
        {
            if (rayHit.transform == null) return false;
            if (!rayHit.transform.CompareTag(ETag.Floor.ToString())) return false;

            return true;
        }

    }

}