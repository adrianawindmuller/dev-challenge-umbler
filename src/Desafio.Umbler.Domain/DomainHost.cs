﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Umbler.Domain
{
    public class DomainHost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string WhoIs { get; set; }
        public int Ttl { get; set; }
        public string HostedAt { get; set; }
    }
}
