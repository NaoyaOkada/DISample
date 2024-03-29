﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppDBAccessLib.Models
{
    public class FullOrder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
