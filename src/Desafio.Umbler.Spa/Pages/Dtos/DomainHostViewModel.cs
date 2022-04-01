namespace Desafio.Umbler.Spa.Pages.Dtos
{
    public class DomainHostViewModel
    {
        public string Name { get; set; } = default!;
    
        public string IP { get; set; } = default!;
    
        public List<string> ServerNames { get; set; } = new List<string>();
    
        public string HostedAt { get; set; } = default!;
    }
}