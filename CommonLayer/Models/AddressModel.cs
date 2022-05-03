﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddressModel
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string  City { get; set; }
        public string State { get; set; }
        public int AddressTypeId { get; set; }
    }
}
