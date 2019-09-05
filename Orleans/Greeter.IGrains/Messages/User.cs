using System;
using System.Collections.Generic;
using System.Text;

namespace OGreeter.IGrains.Messages
{


    public class User
    {

        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public DateTime CreateTime { get; set; }
    }

}
