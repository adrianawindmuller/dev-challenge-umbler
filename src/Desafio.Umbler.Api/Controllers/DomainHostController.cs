﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Application;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Umbler.Api.Controllers
{
    [Route("api/domain")]
    public class DomainHostController : BaseController
    {
        private readonly IDomainHostApplication _domainHostApplication;

        public DomainHostController(IDomainHostApplication domainHostApplication)
        {
            _domainHostApplication = domainHostApplication;
        }

        [HttpGet, Route("{domainName:required:maxlength(100)}")]
        public async Task<IActionResult> GetDomainHostByNameAsync(string domainName)
        {
            var response = await _domainHostApplication.FindDomainHostByNameAsync(domainName);
            return Response(response);
        }
    }
}
