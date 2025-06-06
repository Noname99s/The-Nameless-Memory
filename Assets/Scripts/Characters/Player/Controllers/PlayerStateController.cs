using UnityEngine;
using Orpul1.State.Attack;

namespace Orpul1.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] PlayerContext context;
        PlayerStatusInfo statusInfo;

        int maxMemoryPeace;

        private void Awake()
        {
            statusInfo = context.statusInfo;
            maxMemoryPeace = context.statusInfo.maxMemoryPeaceCount;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(ETag.Item.ToString())) return;
            if (Managers.Map.stageType >= Stage.Max) return;
            if (statusInfo.getMemoryPeaceCounts[(int)Managers.Map.stageType] > maxMemoryPeace) return;
            IncreaseState();
        }
        private void IncreaseState()
        {
            statusInfo.getMemoryPeaceCounts[(int)Managers.Map.stageType]++;
            statusInfo.curStatePercent += statusInfo.increaseStateValue;
            statusInfo.maxHp += (int)statusInfo.increaseStateValue;
            statusInfo.hp = statusInfo.maxHp;
            statusInfo.damages[(int)EPlayerAttack.NormalAttack] *= (1 + statusInfo.curStatePercent / 100f);
            statusInfo.damages[(int)EPlayerAttack.StrongAttack] *= (1 + statusInfo.curStatePercent / 100f);

            SetPlayerDataToManager();
        }
        private void SetPlayerDataToManager()
        {
            PlayerStatusInfo playerStatus = new PlayerStatusInfo(statusInfo);
            Managers.Game.playData.playerStatus = playerStatus;
        }
        
    }
}
