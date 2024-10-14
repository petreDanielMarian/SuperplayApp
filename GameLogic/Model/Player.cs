﻿using GameLogic.Types;

namespace GameLogic.Model
{
    /// <summary>
    /// Player model that has an id and a list of resources
    /// </summary>
    public class Player
    {
        public long Id { get; init; }
        public Dictionary<PlayerResourceType, int> Resources = [];
        public static Player EmptyPlayer => new Player(0);

        public Player(long id)
        {
            Id = id;

            // Initialize resources
            Resources = new Dictionary<PlayerResourceType, int>
            {
                { PlayerResourceType.Coins, 0 },
                { PlayerResourceType.Rolls, 0 }
            };
        }

    }
}
