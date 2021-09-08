using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCore
{
    public class LogLine
    {
        public enum LineType
        {
            LoggerMessage = 1,
            Trigger,
            PublicChat,
            LocalChat,
            Command,
            Message,
            Death,
            Kick,
            Kill,
            PlayerDepart,
            PlayerReturn,
            Stop,
            Start
        }

        public DateTime datetime;
        public int type;
        public string nick;
        public string nick2;
        public string text; // Для триггеров и сообщений
        public string name; // Для триггеров
        public int triggerType;
         
        public LogLine(DateTime datetime, int type, string nick, string nick2, string text, string name, int triggerType)
        {
            this.datetime = datetime;
            this.type = type;
            this.nick = nick;
            this.nick2 = nick2;
            this.text = text;
            this.name = name;
            this.triggerType = triggerType;
        }
    }
    public class LogLines : List<LogLine>
    { 
           
    }
}
