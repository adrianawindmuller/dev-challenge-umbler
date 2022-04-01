using Desafio.Umbler.Api.Controllers;
using Desafio.Umbler.Application;
using Desafio.Umbler.Application.Common;
using Desafio.Umbler.Domain;
using Desafio.Umbler.Infrastructure.Data;
using DnsClient;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Test
{
    [TestClass]
    public class ControllersTest
    {
        [TestMethod]
        public void AddDomainHost_WhenDomainHostDoesntExistsInDatabaseYet_Sucess()
        {
            // ARRANGE 
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Find_searches_url")
                .Options;

            var domain = new DomainHost("umbler.com", "187.84.237.146", "Ns.umbler.com", "umbler.corp", 60);

            // ACT
            // Insert seed data into the database using one instance of the context
            using (var db = new DatabaseContext(options))
            {
                db.DomainHost.Add(domain);
                db.SaveChanges();
            }

            DomainHostViewModel vm;

            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(options))
            {
                var lookupClient = CreateLookputClientMock("umbler.com", "187.84.237.146");
                var whoisClient = CreateWhoisClientMock("Ns.umbler.com", "umbler.corp");

                var application = new DomainHostApplication(db, lookupClient.Object, whoisClient.Object);
                var controller = new DomainHostController(application);

                var response = controller.GetDomainHostByNameAsync("umbler.com");
                var result = response.Result as OkObjectResult;
                vm = result.Value as DomainHostViewModel;
            }

            // ASSERT
            Assert.AreEqual("umbler.com", vm.Name);
            Assert.AreEqual("187.84.237.146", vm.Ip);
            Assert.AreEqual("Ns.umbler.com", vm.ServerNames.First());
            Assert.AreEqual("umbler.corp", vm.HostedAt);
        }

        [TestMethod]
        public void EditDomainHost_WhenTTLisOutdated_Sucess()
        {
            // ARRANGE 
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Find_searches_url")
                .Options;

            // Insert seed data into the database using one instance of the context
            var domain = new DomainHost("umbler.com", "187.84.237.146", "Ns.umbler.com", "umbler.corp", 0);
            using (var db = new DatabaseContext(options))
            {
                db.DomainHost.Add(domain);
                db.SaveChanges();
            }

            DomainHostViewModel vm;

            // Use a clean instance of the context to run the test
            using (var db = new DatabaseContext(options))
            {
                var lookupClient = CreateLookputClientMock("terra.com.br", "208.70.188.57");
                var whoisClient = CreateWhoisClientMock("Ns.terra.com.br", "terra.corp");

                var application = new DomainHostApplication(db, lookupClient.Object, whoisClient.Object);
                var controller = new DomainHostController(application);

                // ACT
                var response = controller.GetDomainHostByNameAsync("terra.com.br");
                var result = response.Result as OkObjectResult;
                vm = result.Value as DomainHostViewModel;
            }

            // ASSERT
            Assert.AreEqual("terra.com.br", vm.Name);
            Assert.AreEqual("208.70.188.57", vm.Ip);
            Assert.AreEqual("Ns.terra.com.br", vm.ServerNames.First());
            Assert.AreEqual("terra.corp", vm.HostedAt);
        }

        [TestMethod]
        public void TryAddDomainHost_WhenWhoisDomainNameDoesntExists_Fail()
        {
            // ARRANGE 
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "Find_searches_url")
                .Options;

            var db = new DatabaseContext(options);
            var lookupClient = CreateLookputClientMock("umbler.com", "187.84.237.146");
            var whoisClient = CreateWhoisClientMock("Ns.umbler.com", organizationName: null);
            var application = new DomainHostApplication(db, lookupClient.Object, whoisClient.Object);
            var controller = new DomainHostController(application);

            // ACT
            var response = controller.GetDomainHostByNameAsync("umbler.com");
            var result = response.Result as NotFoundObjectResult;

            // ASSERT
            Assert.AreEqual(404, result.StatusCode);
        }

        private Mock<ILookupClient> CreateLookputClientMock(string domainName, string ip)
        {
            var aRecord = new ARecord(new ResourceRecordInfo
                (domainName, ResourceRecordType.A, QueryClass.IN, 0, 0), IPAddress.Parse(ip));

            var dnsResponseMock = new Mock<IDnsQueryResponse>();
            dnsResponseMock
                .Setup(p => p.Answers)
                    .Returns(new DnsResourceRecord[] { aRecord });

            var lookupMock = new Mock<ILookupClient>();
            lookupMock.Setup(f => f.QueryAsync(It.IsAny<string>(), It.IsAny<QueryType>(), It.IsAny<QueryClass>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(dnsResponseMock.Object));

            return lookupMock;
        }

        private Mock<IWhoisClient> CreateWhoisClientMock(string raw, string organizationName)
        {
            var whoisClient = new Mock<IWhoisClient>();
            whoisClient.Setup(l => l.QueryAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<Encoding>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>())
            ).Returns(Task.FromResult(new WhoisResponse { Raw = raw, OrganizationName = organizationName }));

            return whoisClient;
        }
    }
}