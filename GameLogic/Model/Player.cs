using GameLogic.Types;

namespace GameLogic.Model
{
    public class Player
    {
        public long Id { get; init; }
        public Dictionary<PlayerResourceType, int> Resources = [];
        public static Player EmptyPlayer => new Player(0);

        public Player(long id)
        {
            Id = id;

            // Initialize resource pool
            Resources = new Dictionary<PlayerResourceType, int>
            {
                { PlayerResourceType.Coins, 0 },
                { PlayerResourceType.Rolls, 0 }
            };
        }

    }
}
