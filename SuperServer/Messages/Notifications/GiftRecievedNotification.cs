﻿using GameLogic.Types;
using SuperServer.Messages.Responses;

namespace SuperServer.Messages.Notifications
{
    /// <summary>
    /// Response made to be sent as notification to the gifted player (if online)
    /// </summary>
    /// <param name="coins"></param>
    /// <param name="rolls"></param>
    public class GiftRecievedNotification(int coins, int rolls) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerNotification} {coins} {rolls}";
        }
    }
}
