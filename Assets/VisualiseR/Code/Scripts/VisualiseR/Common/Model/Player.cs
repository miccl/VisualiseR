namespace VisualiseR.Common
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JoinDate { get; set; }
        public bool IsHost { get; set; }

        public Player(string name, bool isHost)
        {
            Name = name;
            IsHost = isHost;
        }
    }
}