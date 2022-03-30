using Desafio.Umbler.Spa.Pages.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Desafio.Umbler.Spa.Pages
{
    public partial class Index
    {
        private DomainHostNameDto DomainHostName { get; set; } = new DomainHostNameDto();

        private DomainHostViewModel DomainHost { get; set; } = new DomainHostViewModel();

        [Inject]
        private HttpClient Http { get; set; }

        private bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
        }

        private async Task HandleValidSubmit()
        {
            IsLoading = true;
            DomainHost = await Http.GetFromJsonAsync<DomainHostViewModel>($"api/domain/{DomainHostName.Name}");
            IsLoading = false;
        }
    }
}