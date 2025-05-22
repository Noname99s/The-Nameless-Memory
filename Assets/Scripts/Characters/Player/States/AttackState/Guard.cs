namespace Orpul1.Player.State.Attack
{
    public class Guard : ICommand
    {
        PlayerContext playerData;
        public Guard(PlayerContext playerData)
        {
            this.playerData = playerData;
        }
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}