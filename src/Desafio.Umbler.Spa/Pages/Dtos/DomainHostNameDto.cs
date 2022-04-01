using System.ComponentModel.DataAnnotations;

namespace Desafio.Umbler.Spa.Pages.Dtos
{
    public class DomainHostNameDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
