using UnityEngine;
using Orpul1;
using Orpul1.State.Attack;

namespace Orpul1.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] PlayerContext context;

        int maxMemoryPeace;
        int hpIncreaseValue = 0;
        int[] damageIncreaseValues = new int[2];

        private void Awake()
        {
            hpIncreaseValue = (int)(context.playerInfo.increaseStateValue / context.playerInfo.hp);
            damageIncreaseValues[(int)EPlayerAttack.NormalAttack] = (int)(context.playerInfo.increaseStateValue / context.attackInfo.damages[(int)EPlayerAttack.NormalAttack]);
            damageIncreaseValues[(int)EPlayerAttack.StrongAttack] = (int)(context.playerInfo.increaseStateValue / context.attackInfo.damages[(int)EPlayerAttack.StrongAttack]);
            maxMemoryPeace = context.playerInfo.maxMemoryPeaceCount;
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
            context.playerInfo.curStatePercent += context.playerInfo.increaseStateValue;
            context.playerInfo.hp += hpIncreaseValue;
            for (int i = 0; i <= (int)EPlayerAttack.StrongAttack; i++)
            {
                context.attackInfo.damages[i] += damageIncreaseValues[i];
            }
        }
    }
}
