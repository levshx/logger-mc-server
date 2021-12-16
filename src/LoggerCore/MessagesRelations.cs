using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerCore
{
    public class MessagesRelations : List<MessagesRelation>
    {
    }

    public class MessagesRelation
    {
        public string sender;
        public string receiver;
    }

}
