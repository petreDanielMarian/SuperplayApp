using GameLogic.Messages.Responses;
using GameLogic.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Messages.Notifications
{
    public class GiftRecievedNotification(int coins, int rolls) : BaseResponse
    {
        public override string ToString()
        {
            return $"{(int)MessageType.ServerNotification} {coins} {rolls}";
        }
    }
}
