namespace GameOfGoose.PlayerObject
{
    public class PlayerManager
    {
        private List<Player> playerList;

        public PlayerManager()
        {
            playerList = new List<Player>();
        }
        public void AddPlayer(Player player)
        {
            playerList.Add(player);
        }
        public List<Player> GetPlayerList()
        {
            return playerList;
        }
    }
}
