﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WFMarketTool
{
    public class Credentials
    {
        public string? Password { get; set; }
        public string? Email { get; set; }

        public Credentials() { }
    }
}