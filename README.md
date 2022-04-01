# Desafio Umbler

Esta é uma aplicação web que recebe um domínio e mostra suas informações de DNS.

# Modificações Realizadas:

## 📌 **BackEnd**

- Criado uma arquitetura em camadas para separar as responsabilidades.

- Alterado o nome Domain para DomainHost pois havia conflito de namespace com o projeto Domain.

- Criando o mapeamento para a DomainHost, utilizando Code First e Fluent API.

- Movido a lógica do código da DomainHostController para a Application, deixando mais limpa e diminuindo as responsabilidades da API.

- Para comunicar a API com a Applicatiom foi criado uma interface, fazendo que uma não dependa da outra diretamente.

- Criado a BaseController para tratar o response de cada chamada HTTP com seu receptivo ResultType, data, ou mensagem.

- Refatoração da Application, criando métodos para código duplicado e testando possíveis retornos que evitam erros.

- Refatoração do Domínio, encapsulando e validando as propriedades, deixando o domínio mais rico.

- Adicionado o Swagger UI na API, o que nos auxilia no consumo e visualização da API REST.

- Desacoplado o LookupClient e o WhoisClient da Application, tornando o código mais fácil de manter e testar.

- Criando método Application que extrai do Whois os ServerNames com Regex.

- Habilitei os analizadores de código default, para melhorar a qualidade do código.

## 📌 **FrontEnd**

- Criado o projeto SPA para o FrontEnd utilizando o Framework Blazor.

- Utilizado DataAnnotations para validar Input;

- Criado spinner para loading inicial.

- Adicionado validação no DomainName utilizando Regex, impedindo que um nome de domínio sem extensão seja enviado para a API.

- Responsivo para mobile

## 📌 **Teste**

- Criado teste unitários para o DominaHost.

- Refatoração dos testes Unitários da DomainHostController, criando mock dos dados do LookupClient e do WhoisClient.

**API**
![Swegger](src/Desafio.Umbler.Spa/wwwroot/img/swagger.png)

**FrontEnd**
![Front Returning Data](src/Desafio.Umbler.Spa/wwwroot/img/front-returning-data.png)
