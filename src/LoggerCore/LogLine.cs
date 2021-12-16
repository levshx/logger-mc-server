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
            GlobalChat,
            LocalChat,
            Command,
            Message,
            Death,
            Kick,
            Kill,
            PlayerDisconnect,
            PlayerConnect,
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

        public static void LineСonversion(string currentLogLine, Settings settings)
        {
            DateTime datetime = DateTime.ParseExact(currentLogLine.Substring(1, 8) + "/" + settings.currentLog.date.ToString("dd-MM-yyyy"),
                                    "HH:mm:ss/dd-MM-yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture);
            int type;
            string nick = null;
            string nick2 = null;
            string text = null;
            string name = null;
            int triggerType = 0;

            if (currentLogLine.Substring(11, 1) == "[")
                if (currentLogLine.Substring(12, 1) == "G")
                {
                    // Глобал чат
                    type = (int)LogLine.LineType.GlobalChat;
                    nick = currentLogLine.Substring(15, currentLogLine.Length - 15);
                    nick = nick.Substring(0, nick.IndexOf(":"));
                    text = currentLogLine.Substring(18 + nick.Length, (currentLogLine.Length - 18) - nick.Length);


                }
                else
                {
                    // Локал чат
                    type = (int)LogLine.LineType.LocalChat;
                    nick = currentLogLine.Substring(15, currentLogLine.Length - 15);
                    nick = nick.Substring(0, nick.IndexOf(":"));
                    text = currentLogLine.Substring(18 + nick.Length, (currentLogLine.Length - 18) - nick.Length);

                }
            else if (currentLogLine.Substring(11, 1) == "С")
                if (currentLogLine.Substring(19, 1) == "ы")
                {
                    // Сервер выключился
                    type = (int)LogLine.LineType.Stop;


                }
                else
                {
                    // Сервер запустился
                    type = (int)LogLine.LineType.Start;


                }
            else
            {
                nick = currentLogLine.Substring(11, currentLogLine.Length - 11);
                nick = nick.Substring(0, nick.IndexOf(" "));

                switch (currentLogLine.Substring(15 + nick.Length, 2))
                {
                    case " к":
                        //БЫЛ КИКНУТ
                        type = (int)LogLine.LineType.Kick;



                        break;
                    case "ёл":
                        //ЗАШЁЛ
                        type = (int)LogLine.LineType.PlayerConnect;

                        break;
                    case "ел":
                        //ВЫШЕЛ
                        type = (int)LogLine.LineType.PlayerDisconnect;

                        break;
                    case "р ":
                        //УМЕР
                        type = (int)LogLine.LineType.Death;

                        break;
                    case "л ":
                        //УБИЛ КОГО-ТО
                        type = (int)LogLine.LineType.Kill;


                        break;
                    case "ue":
                        string command = currentLogLine.Substring(35 + nick.Length, currentLogLine.Length - (35 + nick.Length));
                        if (
                            command.ToLower().StartsWith("/m ") ||
                            command.ToLower().StartsWith("/w ") ||
                            command.ToLower().StartsWith("/r ") ||
                            command.ToLower().StartsWith("/tell ") ||
                            command.ToLower().StartsWith("/msg ")
                            )
                        {
                            // Написал сообщение
                            type = (int)LogLine.LineType.Message;
                            if (
                            command.ToLower().StartsWith("/m ") ||
                            command.ToLower().StartsWith("/w ") ||
                            command.ToLower().StartsWith("/tell ") ||
                            command.ToLower().StartsWith("/msg ")
                            )
                            {
                                // обработка /m
                                var tmp_command = command;
                                tmp_command = tmp_command.Remove(0, command.IndexOf(' ') + 1);
                                if (tmp_command.Length>0)
                                {
                                    nick2 = tmp_command.Split()[0];
                                    text = tmp_command.Remove(0, nick2.Length);
                                    if (settings.msgRelations.Exists(x => x.sender == nick.ToLower()))
                                    {
                                        settings.msgRelations.Find(x => x.sender.Contains(nick.ToLower())).receiver = nick2;
                                        if (settings.msgRelations.Exists(x => x.sender == nick2.ToLower()))
                                        {
                                            settings.msgRelations.Find(x => x.sender.Contains(nick2.ToLower())).receiver = nick;

                                        }
                                        else
                                        {
                                            settings.msgRelations.Add(new MessagesRelation() { sender = nick2.ToLower(), receiver = nick });
                                        }
                                    }
                                    else
                                    {
                                        settings.msgRelations.Add(new MessagesRelation() { sender = nick.ToLower(), receiver = nick2 });
                                        if (settings.msgRelations.Exists(x => x.sender == nick2.ToLower()))
                                        {
                                            settings.msgRelations.Find(x => x.sender.Contains(nick2.ToLower())).receiver = nick;

                                        }
                                        else
                                        {
                                            settings.msgRelations.Add(new MessagesRelation() { sender = nick2.ToLower(), receiver = nick });
                                        }
                                    } 
                                    
                                    if (text.Length>0)
                                    {
                                        text = text.Remove(0, 1);
                                    }
                                }
                                else
                                {
                                    nick2 = "UNKNOWN";
                                }
                            }
                            else
                            {
                                text = command.Remove(0,3);
                                // Обработка /r
                                if (settings.msgRelations.Exists(x => x.sender == nick.ToLower()))
                                {
                                    nick2 = settings.msgRelations.Find(x => x.sender.Contains(nick.ToLower())).receiver;
                                }
                                else
                                {
                                    nick2 = "UNKNOWN";
                                }
                            }
                        }
                        else
                        {
                            //ВВЁЛ КОМАНДУ
                            type = (int)LogLine.LineType.Command;
                            text = currentLogLine.Substring(35 + nick.Length, currentLogLine.Length - (35 + nick.Length));
                        }

                        break;
                    default:
                        // Неизвестная команда
                        type = (int)LogLine.LineType.LoggerMessage;
                        text = "Неизвестная команда: \n" + currentLogLine;

                        break;
                }
            }
            settings.currentLogLines.Add( new LogLine(datetime, type, nick, nick2, text, name, triggerType));
        }

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
