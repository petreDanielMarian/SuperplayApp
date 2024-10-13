﻿using GameLogic.Types;

namespace GameLogic.Messages.Requests
{
    public class LoginRequestMessage(long uuid)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Login} {uuid}";
        }
    }
}
