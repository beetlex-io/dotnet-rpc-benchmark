using System;
using System.Collections.Generic;
using System.Text;

namespace OGreeter.IGrains.Messages
{

    public class Fortune
    {

        public int Id { get; set; }

        public string Message { get; set; }

    }
    public class World
    {

        public int Id { get; set; }

        public int RandomNumber { get; set; }
    }
}
