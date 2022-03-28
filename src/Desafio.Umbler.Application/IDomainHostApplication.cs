using Desafio.Umbler.Application.Common;

namespace Desafio.Umbler.Application
{
    public interface IDomainHostApplication
    {
        public Task<Result> FindDomainHostByNameAsync(string domainName);
    }
}
