namespace Desafio.Umbler.Spa.Pages.Dtos
{
    public class DomainHostViewModel
    {
        public string Name { get; set; }

        public string IP { get; set; }

        public List<string> ServerNames { get; set; } = new List<string>();

        public string HostedAt { get; set; }
    }
}