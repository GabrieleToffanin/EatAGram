﻿namespace Eatagram.SDK.Models.Requests
{
    public class MessageRequest
    {
        public string Message { get; set; }
        public string ToUser { get; set; }
        public string? GroupName { get; set; }

    }
}
