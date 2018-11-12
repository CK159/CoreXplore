using System;

namespace Db
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime DateCreated { get; set; }
    }
}