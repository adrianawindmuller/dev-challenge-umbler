using Desafio.Umbler.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Desafio.Umbler.Test
{
    [TestClass]
    public class DomainHostTests
    {
        [TestMethod]
        public void NewDomainHost_ValidConstrutor_DomainHostValid()
        {
            // arrange - act
            var domainHost = new DomainHost("teste.com.br", "548.545.122.956", "descrição whois", "Teste LTDA", 50);

            // assert
            Assert.AreEqual("teste.com.br", domainHost.Name);
            Assert.AreEqual("548.545.122.956", domainHost.Ip);
            Assert.AreEqual("descrição whois", domainHost.WhoIs);
            Assert.AreEqual("Teste LTDA", domainHost.HostedAt);
            Assert.AreEqual(50, domainHost.Ttl);
        }

        [TestMethod]
        public void NewDomainHost_InvalidConstrutor_DomainHostInvalid()
        {
            // test if name is not null
            var ex1 = Assert.ThrowsException<ArgumentException>(() => new DomainHost(null, "548.545.122.956", "descrição whois", "Teste LTDA", 50));
            Assert.AreEqual("Nome obrigatório! (Parameter 'name')", ex1.Message);

            // test if the name does not exceed 100 characters
            var ex2 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("nome de dominio invalido nome de dominio invalido nome de dominio invalido nome de dominio invalido nome", "548.545.122.956", "descrição whois", "Teste LTDA", 50));
            Assert.AreEqual("O nome deve ter menos de 100 caracteres. (Parameter 'name')", ex2.Message);

            // test if the IP is not null
            var ex3 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("teste.com", null, "descrição whois", "Teste LTDA", 50));
            Assert.AreEqual("IP obrigatório! (Parameter 'ip')", ex3.Message);

            // test if the IP is not longer than 15 characters
            var ex4 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("teste.com", "999.666.444.555.555.55", "descrição whois", "Teste LTDA", 50));
            Assert.AreEqual("O IP deve ter menos de 15 caracteres. (Parameter 'ip')", ex4.Message);

            // test if Whois is not null
            var ex5 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("teste.com", "999.666.444.555", null, "Teste LTDA", 50));
            Assert.AreEqual("WhoIs obrigatório! (Parameter 'whois')", ex5.Message);

            // tests if HostAt is not null
            var ex6 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("teste.com", "999.666.444.555", "descrição whois", null, 50));
            Assert.AreEqual("'Hospedado em' obrigatório! (Parameter 'hostedAt')", ex6.Message);

            // tests if HostAt is not longer than 200 characters
            var ex7 = Assert.ThrowsException<ArgumentException>(() => new DomainHost("teste.com", "999.666.444.555", "descrição whois longa", "descrição whois longa descrição whois longa descrição whois longa descrição whois longa descrição whois longa descrição whois longadescrição whois longadescrição whois longadescrição whois longa descrição whois longa", 50));
            Assert.AreEqual("'Hospedado em' deve ter menos de 200 caracteres. (Parameter 'hostedAt')", ex7.Message);

        }

        [TestMethod]
        public void EditDomainHost_ValidEdit_ValidDomainHost()
        {
            // arrange
            var domainHost = new DomainHost("teste.com.br", "548.545.122.956", "descrição whois", "Teste", 50);

            // act
            domainHost.EditDomainHost("terra.com.br", "222.666.444.99", "descrição whois alterada", "Terra", 100);

            // assert
            Assert.AreEqual("terra.com.br", domainHost.Name);
            Assert.AreEqual("222.666.444.99", domainHost.Ip);
            Assert.AreEqual("descrição whois alterada", domainHost.WhoIs);
            Assert.AreEqual("Terra", domainHost.HostedAt);
            Assert.AreEqual(100, domainHost.Ttl);
        }

        [TestMethod]
        public void EditDomainHost_InvalidEdit_InvalidDomainHost()
        {
            // arrange
            var domainHost = new DomainHost("teste.com.br", "548.545.122.956", "descrição whois", "Teste", 50);

            // assert
            var ex1 = Assert.ThrowsException<ArgumentException>(() => domainHost.EditDomainHost(null, "999.888.555.555", "descrição", "Teste", 0));
            Assert.AreEqual("Nome obrigatório! (Parameter 'name')", ex1.Message);

            // return ThrowsException of IP
            var ex2 = Assert.ThrowsException<ArgumentException>(() => domainHost.EditDomainHost("google.com", "999.888.555.555.888", "descrição", "Teste", 0));
            Assert.AreEqual("O IP deve ter menos de 15 caracteres. (Parameter 'ip')", ex2.Message);
        }
    }
}
