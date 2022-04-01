using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Application.Common
{
    public class WhoisClientWrapper : IWhoisClient
    {
        public Task<WhoisResponse> QueryAsync(
            string query, 
            string server = null, 
            int port = 43, 
            Encoding encoding = null, 
            int timeout = 600, 
            int retries = 10, 
            CancellationToken token = default)
        {
            return WhoisClient.QueryAsync(query, server, port, encoding, timeout, retries, token);
        }
    }
}
