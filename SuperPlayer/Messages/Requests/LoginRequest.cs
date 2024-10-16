﻿using GameLogic.Types;

namespace SuperPlayer.Messages.Requests
{
    public class LoginRequest(long udid)
    {
        public override string ToString()
        {
            return $"{(int)CommandType.Login} {udid}";
        }
    }
}
