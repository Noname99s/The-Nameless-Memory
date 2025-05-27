namespace Orpul1.Player.State.Attack
{
    public class Guard : IStrategy
    {
        PlayerContext playerData;
        public Guard(PlayerContext playerData)
        {
            this.playerData = playerData;
        }
        public void Execute()
        {
        }

        public void Initialize()
        {
            playerData.attackContext.curAttack = 0;
        }
    }
}