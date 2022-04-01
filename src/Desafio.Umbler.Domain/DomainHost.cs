using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Umbler.Domain
{
    public class DomainHost
    {
        public DomainHost(string name, string ip, string whois, string hostedAt, int ttl)
        {
            Validate(name, ip, whois, hostedAt);
            Name = name;
            Ip = ip;
            WhoIs = whois;
            Ttl = ttl;
            HostedAt = hostedAt;
            UpdatedAt = DateTime.Now;
        }

        private DomainHost() { }

        public int Id { get; }
        public string Name { get; private set; }
        public string Ip { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string WhoIs { get; private set; }
        public int Ttl { get; private set; }
        public string HostedAt { get; private set; }

        public void EditDomainHost(string name, string ip, string whois, string hostedAt, int ttl)
        {
            Validate(name, ip, whois, hostedAt);
            Name = name;
            Ip = ip;
            WhoIs = whois;
            HostedAt = hostedAt;
            Ttl = ttl;
            UpdatedAt = DateTime.Now;
        }

        private static void Validate(string name, string ip, string whois, string hostedAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome obrigatório!", nameof(name));
            
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("Ip obrigatório!", nameof(ip));

            if(string.IsNullOrWhiteSpace(whois))
                throw new ArgumentException("WhoIs obrigatório!", nameof(whois));

            if(string.IsNullOrWhiteSpace(hostedAt))
                throw new ArgumentException("HostAt obrigatório!", nameof(hostedAt));
                
            if(name.Length > 100)
                throw new ArgumentException("Insira menos de 100 caracteres.", nameof(name));

            if(ip.Length > 15)
                throw new ArgumentException("Insira menos de 15 caracteres.", nameof(ip));

            if(hostedAt.Length > 200)
                throw new ArgumentException("Insira menos de 200 caracteres.", nameof(hostedAt));

        }
    }
}