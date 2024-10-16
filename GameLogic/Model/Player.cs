using GameLogic.Types;

namespace GameLogic.Model
{
    /// <summary>
    /// Player model that has an id and a list of resources
    /// </summary>
    public class Player
    {
        public long Id { get; set; }
        public Dictionary<PlayerResourceType, int> Resources = [];
        public static Player EmptyPlayer => new Player(0);

        public Player(long id, int coins = 0, int rolls = 0)
        {
            Id = id;

            // Initialize resources
            Resources = new Dictionary<PlayerResourceType, int>
            {
                { PlayerResourceType.Coins, coins },
                { PlayerResourceType.Rolls, rolls }
            };
        }

    }
}
