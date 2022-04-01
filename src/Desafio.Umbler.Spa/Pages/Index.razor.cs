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
            MessageInvalidURL = "";
        }

        private async Task HandleValidSubmit()
        {
            // clear the properties
            MessageInvalidURL = "";
            IsLoading = true;
            DomainHost = null;

            // validates if domainName is valid
            // if url address is a valid it goes to the api, if not i issue an error message
            var isURLValid = IsURLValid(DomainHostName.Name);
            if (isURLValid == true)
            {
                DomainHost = await Http.GetFromJsonAsync<DomainHostViewModel>($"api/domain/{DomainHostName.Name}");
            }
            else
            {
                MessageInvalidURL = "Digite uma nome de dominio valido";
            }

            IsLoading = false;
        }


        private bool IsURLValid(string nameDomain)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(nameDomain);
        }
    }
}