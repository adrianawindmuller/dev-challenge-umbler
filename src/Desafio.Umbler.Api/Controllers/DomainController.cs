using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Whois.NET;
using Microsoft.EntityFrameworkCore;
using DnsClient;
using Desafio.Umbler.Infrastructure.Data;
using Desafio.Umbler.Domain;

namespace Desafio.Umbler.Api.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        private readonly DatabaseContext _db;

        public DomainController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            // 1. identifica se já existe um dominio com este nome no banco
            var domain = await _db.DomainHost.FirstOrDefaultAsync(d => d.Name == domainName);

            // 2. se o dominio não existe vi criar um novo registro no banco
            if (domain == null)
            {
                // 2.1 consulta informações de contato e os DNS do endereço via nome
                var response = await WhoisClient.QueryAsync(domainName);

                // 2.2 consulta os DNS
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domainName, QueryType.ANY);
                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                // 2.3 consulta novamente as informações de contato e os DNS do endereço via IP
                var hostResponse = await WhoisClient.QueryAsync(ip);

                // 2.4 monta a novo objeto de Domain
                domain = new DomainHost
                {
                    Name = domainName,
                    Ip = ip,
                    UpdatedAt = DateTime.Now,
                    WhoIs = response.Raw,
                    Ttl = record?.TimeToLive ?? 0,
                    HostedAt = hostResponse.OrganizationName
                };

                // 2.5 add no bd o domain
                _db.DomainHost.Add(domain);
            }

            //  consulta se a data de atualização é maior que o Ttl, se não vai atualizar o registro no banco
            if (DateTime.Now.Subtract(domain.UpdatedAt).TotalMinutes > domain.Ttl)
            {
                // consulta os dados de contato e DNS
                var response = await WhoisClient.QueryAsync(domainName);

                // consulta os DNs
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domainName, QueryType.ANY);
                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                // consulta novamente os dados de contato e DNS via IP
                var hostResponse = await WhoisClient.QueryAsync(ip);
                
                // atualiza os dados de Domain
                domain.Name = domainName;
                domain.Ip = ip;
                domain.UpdatedAt = DateTime.Now;
                domain.WhoIs = response.Raw;
                domain.Ttl = record?.TimeToLive ?? 0;
                domain.HostedAt = hostResponse.OrganizationName;
            }

            await _db.SaveChangesAsync();

            return Ok(domain);
        }
    }
}
