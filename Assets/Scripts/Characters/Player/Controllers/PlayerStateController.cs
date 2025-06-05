using UnityEngine;
using Orpul1;
using Orpul1.State.Attack;
using static UnityEngine.Rendering.DebugUI;

namespace Orpul1.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] PlayerContext context;
        PlayerStatusInfo statusInfo;

        int maxMemoryPeace;
        int hpIncreaseValue = 0;
        float[] damageIncreaseValues = new float[2];

        private void Awake()
        {
            statusInfo = context.statusInfo;
            hpIncreaseValue = (int)GetIncreaseValue(statusInfo.hp);
            damageIncreaseValues[(int)EPlayerAttack.NormalAttack] = GetIncreaseValue(context.statusInfo.damages[(int)EPlayerAttack.NormalAttack]);
            damageIncreaseValues[(int)EPlayerAttack.StrongAttack] = GetIncreaseValue(context.statusInfo.damages[(int)EPlayerAttack.StrongAttack]);
            maxMemoryPeace = context.statusInfo.maxMemoryPeaceCount;
        }
        private float GetIncreaseValue(float value)
        {
            return ((value * 0.01f) * context.statusInfo.increaseStateValue);
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
            statusInfo.hp += hpIncreaseValue;
            for (int i = 0; i <= (int)EPlayerAttack.StrongAttack; i++)
            {
                statusInfo.damages[i] += damageIncreaseValues[i];
            }
            SetPlayerDataToManager();
        }
        private void SetPlayerDataToManager()
        {
            Managers.Game.playData.playerStatus.hp = statusInfo.hp;
            Managers.Game.playData.playerStatus.curStatePercent = statusInfo.curStatePercent;
            Managers.Game.playData.playerStatus.getMemoryPeaceCounts[(int)Managers.Map.stageType] = statusInfo.getMemoryPeaceCounts[(int)Managers.Map.stageType];
            for (int i = 0; i <= (int)EPlayerAttack.StrongAttack; i++)
            {
                Managers.Game.playData.playerStatus.damages[i] += damageIncreaseValues[i];
            }
        }
        
    }
}
