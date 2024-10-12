using GameLogic.Types;

namespace GameLogic.Model
{
    public class Player
    {
        public long Id { get; init; }
        public Dictionary<PlayerResourceType, int> Resources { get; set; } = new Dictionary<PlayerResourceType, int>();

        public static Player EmptyPlayer => new Player(0);

        public Player(long id)
        {
            Id = id;
        }
    }
}
