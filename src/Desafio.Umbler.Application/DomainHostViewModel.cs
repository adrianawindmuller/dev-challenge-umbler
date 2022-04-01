namespace Desafio.Umbler.Application
{
    public class DomainHostViewModel
    {
        public string Name { get; set; }

        public string Ip { get; set; }

        public List<string> ServerNames { get; set; } = new List<string>();

        public string HostedAt { get; set; }
    }
}
