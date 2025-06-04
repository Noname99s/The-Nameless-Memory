using UnityEngine;
using Orpul1;
using Orpul1.State.Attack;
using static UnityEngine.Rendering.DebugUI;

namespace Orpul1.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] PlayerContext context;

        int maxMemoryPeace;
        int hpIncreaseValue = 0;
        float[] damageIncreaseValues = new float[2];

        private void Awake()
        {
            hpIncreaseValue = (int)GetIncreaseValue(context.playerInfo.hp);
            damageIncreaseValues[(int)EPlayerAttack.NormalAttack] = GetIncreaseValue(context.attackInfo.damages[(int)EPlayerAttack.NormalAttack]);
            damageIncreaseValues[(int)EPlayerAttack.StrongAttack] = GetIncreaseValue(context.attackInfo.damages[(int)EPlayerAttack.StrongAttack]);
            maxMemoryPeace = context.playerInfo.maxMemoryPeaceCount;
        }
        private float GetIncreaseValue(float value)
        {
            return ((value * 0.01f) * context.playerInfo.increaseStateValue);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(ETag.Item.ToString())) return;
            if (Managers.Map.stageType >= Stage.Max) return;
            if (context.playerInfo.getMemoryPeaces[(int)Managers.Map.stageType] > maxMemoryPeace) return;
            IncreaseState();
        }
        private void IncreaseState()
        {
            context.playerInfo.getMemoryPeaces[(int)Managers.Map.stageType]++;
            context.playerInfo.curStatePercent += context.playerInfo.increaseStateValue;
            context.playerInfo.hp += hpIncreaseValue;
            for (int i = 0; i <= (int)EPlayerAttack.StrongAttack; i++)
            {
                context.attackInfo.damages[i] += damageIncreaseValues[i];
            }
        }
    }
}
