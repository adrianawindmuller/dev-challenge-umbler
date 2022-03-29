using Desafio.Umbler.Application.Common;
using Desafio.Umbler.Domain;
using Desafio.Umbler.Infrastructure.Data;
using DnsClient;
using DnsClient.Protocol;
using Microsoft.EntityFrameworkCore;
using Whois.NET;

namespace Desafio.Umbler.Application
{
    public class DomainHostApplication : IDomainHostApplication
    {
        private readonly DatabaseContext _db;

        public DomainHostApplication(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Result> FindDomainHostByNameAsync(string domainName)
        {
            // 1. search the Whois data and if not found, returns a NotFound.
            var response = await WhoIs(domainName);
            if (response.Sucess == false)
                return Result.NotFound($"Error: {response.Raw}");

            // 2. identifies if a DomainHost with this name already exists in the db
            var domainHost = await _db.DomainHost.FirstOrDefaultAsync(d => d.Name == domainName);

            // 3. if the DomainHost does not exist vi create a new record in the database
            if (domainHost == null)
            {
                // 3.1 create a new DomainHost object
                domainHost = new DomainHost(
                    domainName, 
                    response.IP, 
                    response.Raw, 
                    response.OrganizationName, 
                    response.Ttl);

                // 3.2 added to the database the DomainHost
                _db.DomainHost.Add(domainHost);
            }

            // 4 if DomainHost already exists, query if update date is greater than Ttl updates data
            if (DateTime.Now.Subtract(domainHost.UpdatedAt).TotalMinutes > domainHost.Ttl)
            {
                // 4.1 update DomainHost data
                domainHost.EditDomainHost(
                    domainName, 
                    response.IP,
                    response.Raw,
                    response.OrganizationName,
                    response.Ttl);
            }

            // 5. save all changes in db
            await _db.SaveChangesAsync();

            // 6. return the DomainHostViewModel with the data
            return Result.Ok(new DomainHostViewModel
            {
                Name = domainHost.Name,
                Ip = domainHost.Ip,
                HostedAt = domainHost.HostedAt,
                WhoIs = domainHost.WhoIs
            });
        }

        private async Task<WhoisDto> WhoIs(string domainName)
        {
            // 1. search contact information and DNS address via domainName
            var response = await WhoisClient.QueryAsync(domainName);

            // 2. if response is null retorn Sucess = false and Raw with the error information
            if (response.OrganizationName == null)
                return (new WhoisDto(response.Raw));

            // 3. search DNS
            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(domainName, QueryType.ANY);

            var record = result.Answers.ARecords().FirstOrDefault();
            var ip = record?.Address?.ToString();

            // 3. search again contact information and DNS address via IP
            var hostResponse = await WhoisClient.QueryAsync(ip);

            // 4. return data
            return new WhoisDto(ip, response.Raw, record?.TimeToLive ?? 0, hostResponse.OrganizationName);
        }
    }
}
