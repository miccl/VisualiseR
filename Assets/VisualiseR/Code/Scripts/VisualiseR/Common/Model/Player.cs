using System;

namespace VisualiseR.Common
{
    [Serializable]
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public DateTime JoinDate { get; private set; }

        public Player()
        {
            JoinDate = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("Name: {0} (Type: {1}, JoinDate: {2})", Name, Type, JoinDate);
        }

        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Type.ToString());
        }
    }
}