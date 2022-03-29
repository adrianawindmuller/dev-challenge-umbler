using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Umbler.Application
{
    public class WhoisDto
    {
        public WhoisDto(string iP, string raw, int ttl, string organizationName)
        {
            IP = iP;
            Raw = raw;
            Ttl = ttl;
            OrganizationName = organizationName;
            Sucess = true;
        }

        public WhoisDto(string raw)
        {
            Raw = raw;
            Sucess = false;
        }


        public string IP { get; }

        public string Raw { get; }

        public int Ttl { get;}

        public string OrganizationName { get; }

        public bool Sucess { get; }
    }
}
