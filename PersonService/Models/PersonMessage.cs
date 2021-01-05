using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonService.Models
{
    public class PersonMessage
    {

        public Person Person { get; set; }
        public string Action { get; set; }

        public PersonMessage(Person person, string method)
        {
            Person = person;
            Action = method;
        }

    }
}
