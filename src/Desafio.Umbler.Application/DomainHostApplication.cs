using Desafio.Umbler.Application.Common;
using Desafio.Umbler.Domain;
using Desafio.Umbler.Infrastructure.Data;
using DnsClient;
using DnsClient.Protocol;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Whois.NET;

namespace Desafio.Umbler.Application
{
    public class DomainHostApplication : IDomainHostApplication
    {
        private readonly DatabaseContext _db;
        private readonly ILookupClient _lookupClient;
        private readonly IWhoisClient _whoisClient;

        public DomainHostApplication(DatabaseContext db, ILookupClient lookupClient, IWhoisClient whoisClient)
        {
            _db = db;
            _lookupClient = lookupClient;
            _whoisClient = whoisClient;
        }

        public async Task<Result> FindDomainHostByNameAsync(string domainName)
        {
            // search the Whois data
            var response = await WhoIs(domainName);
            if (!response.Sucess)
                return Result.NotFound($"Error: {response.Raw}");

            var domainHost = await _db.DomainHost.FirstOrDefaultAsync(d => d.Name == domainName);

            // if the DomainHost does not exist yet, create a new one
            if (domainHost == null)
            {
                domainHost = new DomainHost(
                    domainName, 
                    response.IP, 
                    response.Raw, 
                    response.OrganizationName, 
                    response.Ttl);

                _db.DomainHost.Add(domainHost);
            }

            // if DomainHost already exists, query if update date is greater than Ttl updates data
            if (DateTime.Now.Subtract(domainHost.UpdatedAt).TotalMinutes > domainHost.Ttl)
            {
                domainHost.EditDomainHost(
                    domainName, 
                    response.IP,
                    response.Raw,
                    response.OrganizationName,
                    response.Ttl);
            }

            await _db.SaveChangesAsync();

            return Result.Ok(new DomainHostViewModel
            {
                Name = domainHost.Name,
                Ip = domainHost.Ip,
                HostedAt = domainHost.HostedAt,
                ServerNames = WhoisRawToServerNames(domainHost.WhoIs)
            });
        }

        private async Task<WhoisDto> WhoIs(string domainName)
        {
            // search whois
            var responseWhois = await _whoisClient.QueryAsync(domainName);
            if (responseWhois.OrganizationName == null)
                return (new WhoisDto(responseWhois.Raw));

            // search DNS
            var responseLookup = await _lookupClient.QueryAsync(domainName, QueryType.ANY);

            var record = responseLookup.Answers.ARecords().FirstOrDefault();
            var ip = record?.Address?.ToString();

            var hostResponse = await _whoisClient.QueryAsync(ip);

            return new WhoisDto(ip, responseWhois.Raw, record?.TimeToLive ?? 0, responseWhois.OrganizationName);
        }

        private static List<string> WhoisRawToServerNames(string raw)
        {
            var serverNamesList = new List<string>();
            var regex = new Regex("nserver:[^\n]+|Name Server:[^\nDNSSEC]+");
            var serverNames = regex.Matches(raw);

            if (regex.IsMatch(raw))
            {
                foreach (var serverName in serverNames)
                {
                    serverNamesList.Add(serverName.ToString().Trim());
                }
            }
            else
            {
                serverNamesList.Add(raw);
            }

            return serverNamesList;
        }
    }
}
