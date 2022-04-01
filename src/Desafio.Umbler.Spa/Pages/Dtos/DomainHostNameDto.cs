using System.ComponentModel.DataAnnotations;

namespace Desafio.Umbler.Spa.Pages.Dtos
{
    public class DomainHostNameDto
    {

        [Required(ErrorMessage = "Nome obrigatório!")]
        [MaxLength(100, ErrorMessage = "O nome deve ter menos de 100 caracteres.")]
        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "Digite um nome de domínio válido")]
        public string Name { get; set; }
    }
}
