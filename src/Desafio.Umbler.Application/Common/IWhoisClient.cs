using System.Text;
using Whois.NET;

namespace Desafio.Umbler.Application.Common
{
    public interface IWhoisClient
    {
        public Task<WhoisResponse> QueryAsync(
            string query, 
            string server = null,
            int port = 43,
            Encoding encoding = null,
            int timeout = 600,
            int retries = 10,
            CancellationToken token = default(CancellationToken));
    }
}
