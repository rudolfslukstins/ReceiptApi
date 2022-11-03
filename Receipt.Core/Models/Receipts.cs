using System;
using System.Collections.Generic;

namespace Receipt.Core.Models
{
    public class Receipts : Entity
    {
        public DateTime CreatedOn { get; set; }
        public List<Items> ItemsList { get; set; }
    }
}
