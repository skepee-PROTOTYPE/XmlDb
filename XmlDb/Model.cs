using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XmlDataBase
{

    public class Security
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }



}
