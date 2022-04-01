using Desafio.Umbler.Spa.Pages.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace Desafio.Umbler.Spa.Pages
{
    public partial class Index
    {
        private DomainHostNameDto DomainHostName { get; set; } = new DomainHostNameDto();

        private DomainHostViewModel DomainHost { get; set; } = new DomainHostViewModel();

        [Inject]
        private HttpClient Http { get; set; }

        private bool IsLoading { get; set; }

        public string MessageInvalidURL { get; set; }

        protected override void OnInitialized()
        {
            IsLoading = true;
            MessageInvalidURL = string.Empty;
        }

        private async Task HandleValidSubmit()
        {
            // clear the properties
            IsLoading = true;
            DomainHost = null;
            MessageInvalidURL = string.Empty;

            // validates if the domainName is valid
            // if url address is valid, it goes to the api, if not, issue an error message
            var isURLValid = IsURLValid(DomainHostName.Name);
            if (isURLValid)
            {
                DomainHost = await Http.GetFromJsonAsync<DomainHostViewModel>($"api/domain/{DomainHostName.Name}");
            }
            else
            {
                MessageInvalidURL = "Digite um nome de domínio válido";
            }

            IsLoading = false;
        }


        private static bool IsURLValid(string nameDomain)
        {
            var regex = new Regex(
                @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.IsMatch(nameDomain);
        }
    }
}