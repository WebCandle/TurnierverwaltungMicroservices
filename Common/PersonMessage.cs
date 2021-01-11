using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class PersonMessage : IMessage
    {
        //GET,POST,PUT ,DELETE
        public string Action { get { return this.Action.ToUpper(); } set { } }
        public IPerson Person { get; set; }
    }
}
