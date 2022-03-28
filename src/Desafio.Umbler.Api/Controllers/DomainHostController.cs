using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Application;

namespace Desafio.Umbler.Api.Controllers
{
    [Route("api")]
    public class DomainHostController : BaseController
    {
        private readonly IDomainHostApplication _domainHostApplication;

        public DomainHostController(IDomainHostApplication domainHostApplication)
        {
            _domainHostApplication = domainHostApplication;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> GetDomainHostByNameAsync(string domainName)
        {
            var response = await _domainHostApplication.FindDomainHostByNameAsync(domainName);
            return Response(response);
        }
    }
}
