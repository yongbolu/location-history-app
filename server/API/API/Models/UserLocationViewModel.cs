﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserLocationViewModel
    {
        public string Id { get; set; }
        public IList<LocationViewModel> Locations { get; set; }
    }
}
