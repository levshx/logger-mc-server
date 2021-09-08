using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCore
{
    public class Triggers: List<Trigger>
    { 
        
    }
    public class Trigger 
    {
        public string name;
        public int type;
        public int minDelaySec;
        public List<string> words;
        public string textMessage;
        public bool trySound;
        public string soundUUID;

        public enum TriggerType
        {
            localChatWords = 1,
            globalChatWords,
            localChatPlayerSay,
            globalChatPlayerSay,

        }
    }
}
