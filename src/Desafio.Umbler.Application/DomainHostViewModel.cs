using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Umbler.Application
{
    public class DomainHostViewModel
    {
        public string Name { get; set; } = default!;

        public string Ip { get; set; } = default!;

        public List<string> ServerNames { get; set; } = new List<string>();

        public string HostedAt { get; set; } = default!;
    }
}
