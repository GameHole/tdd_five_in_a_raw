using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogSerializer : ASerializer
    {
        public string log;
        public override Message Deserialize(ByteStream stream)
        {
            log = $"Deserialize";
            return null;
        }

        public override void Serialize(Message message, ByteStream stream)
        {
            log = $"Serialize";
        }
    }
}
