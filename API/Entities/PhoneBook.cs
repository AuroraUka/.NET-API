using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class PhoneBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; }
    }
}