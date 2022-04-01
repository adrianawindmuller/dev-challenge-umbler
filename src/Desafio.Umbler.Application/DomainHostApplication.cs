using Desafio.Umbler.Application.Common;
using Desafio.Umbler.Domain;
using Desafio.Umbler.Infrastructure.Data;
using DnsClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            // find the Whois data by domainName
            var response = await WhoIs(domainName);
            if (!response.Sucess)
                return Result.NotFound($"Error: {response.Raw}");

            // find the DomainHost by id
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

            // if UpdatedAt is greater than Ttl, edit the domainHost data
            if (DateTime.Now.Subtract(domainHost.UpdatedAt).TotalMinutes > domainHost.Ttl)
            {
                domainHost.EditDomainHost(
                    domainName,
                    response.IP,
                    response.Raw,
                    response.OrganizationName,
                    response.Ttl);
            }

            // persist data on DB
            await _db.SaveChangesAsync();

            // returns the result
            return Result.Ok(new DomainHostViewModel
            {
                Name = domainHost.Name,
                Ip = domainHost.Ip,
                HostedAt = domainHost.HostedAt,
                ServerNames = WhoisRawToServerNames(domainHost.WhoIs)
            });
        }

        /// <summary>
        /// Find the Whois data by domainName
        /// </summary>
        private async Task<WhoisDto> WhoIs(string domainName)
        {
            // find whois data
            var responseWhois = await _whoisClient.QueryAsync(domainName);
            if (responseWhois.OrganizationName == null)
                return (new WhoisDto(responseWhois.Raw));

            // find DNS
            var responseLookup = await _lookupClient.QueryAsync(domainName, QueryType.ANY);

            var record = responseLookup.Answers.ARecords().FirstOrDefault();
            var ip = record?.Address?.ToString();

            var hostResponse = await _whoisClient.QueryAsync(ip);

            return new WhoisDto(ip, responseWhois.Raw, record?.TimeToLive ?? 0, hostResponse.OrganizationName);
        }

        /// <summary>
        /// Extract the DNSs from the Whois raw
        /// </summary>
        private static List<string> WhoisRawToServerNames(string raw)
        {
            var serverNamesList = new List<string>();
            var regex = new Regex("nserver:[^\n]+|Name Server:[^\nDNSSEC]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
