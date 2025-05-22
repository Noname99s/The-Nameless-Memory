namespace Orpul1.Player.State.Attack
{
    public class StrongAttack : ICommand
    {
        PlayerContext playerData;
        public StrongAttack(PlayerContext playerData)
        {
            this.playerData = playerData;
        }

        public void Execute()
        {
        }
    }
}