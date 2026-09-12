# InovaGAB Backend

Backend desenvolvido em C# com .NET 8 Web API para a Sprint 2 do Challenge FIAP - Grupo Águia Branca.

O sistema fornece APIs para autenticação por nível de acesso, gerenciamento de diretrizes estratégicas, ideias de inovação, projetos, equipes, resultados alcançados e consulta de indicadores no dashboard executivo.

## Tecnologias Utilizadas

- C#
- .NET 8
- ASP.NET Core Web API
- MongoDB
- JWT (JSON Web Token)
- Swagger / OpenAPI
- Docker
- Git e GitHub

## Funcionalidades Principais

- Autenticação de usuários com JWT
- Controle de acesso por perfil de usuário
- Gerenciamento de usuários
- Gerenciamento de diretrizes estratégicas
- Gerenciamento e priorização de ideias de inovação
- Gerenciamento de projetos
- Registro de equipes e engajamento
- Cadastro de indicadores estratégicos
- Registro de resultados alcançados
- Dashboard executivo com indicadores consolidados
- Indicadores financeiros de investimento, retorno, lucro e ROI
- Indicadores de andamento e conclusão dos projetos
- Indicadores de engajamento e aumento de produtividade

## Perfis de Acesso

A API utiliza autenticação JWT e controle de autorização baseado em perfis de usuário.

- **Operador:** participação no processo de inovação e submissão de ideias.
- **Gestor:** gerenciamento operacional de ideias, projetos, equipes e resultados.
- **Liderança:** acesso aos indicadores consolidados e ao dashboard executivo.

## Como Executar o Projeto

### Pré-requisitos

Para executar a aplicação, é necessário possuir:

- .NET 8 SDK
- MongoDB
- Git

### Clonar o repositório

```bash
git clone https://github.com/rafavanzele/InovaGAB_Backend.git
```

Acesse a pasta do projeto:

```bash
cd InovaGAB_Backend
```

### Restaurar as dependências

```bash
dotnet restore
```

### Executar a API

```bash
dotnet run --project InovaGAB.Api
```

Após iniciar a aplicação, utilize o endereço exibido no terminal para acessar o Swagger.

## Autenticação

A API utiliza autenticação baseada em JWT (JSON Web Token).

Para acessar endpoints protegidos pelo Swagger:

1. Execute a API e acesse o Swagger.
2. Utilize o endpoint de login para autenticar um usuário.
3. Copie o token JWT retornado pela API.
4. Clique no botão **Authorize** no Swagger.
5. Informe o token no campo de autorização.
6. Após a autenticação, execute os endpoints permitidos para o perfil do usuário.

O sistema possui controle de autorização por perfil, portanto determinados endpoints podem retornar **403 Forbidden** quando o usuário autenticado não possui permissão para executar a operação.

## Principais Endpoints

A API está organizada nos seguintes recursos:

- **Autenticação:** login e geração do token JWT.
- **Usuários:** gerenciamento dos usuários da aplicação.
- **Diretrizes Estratégicas:** cadastro e consulta das diretrizes estratégicas.
- **Ideias:** cadastro, consulta, atualização e priorização de ideias.
- **Projetos:** gerenciamento e acompanhamento dos projetos de inovação.
- **Equipes:** gerenciamento das equipes vinculadas ao processo de inovação.
- **Engajamento das Equipes:** registro e acompanhamento dos níveis de engajamento.
- **Indicadores Estratégicos:** cadastro e acompanhamento dos indicadores.
- **Resultados Alcançados:** registro dos resultados obtidos pelos projetos.
- **Relatórios Executivos:** consolidação de indicadores estratégicos, financeiros, projetos, engajamento e produtividade.

A documentação completa dos endpoints, parâmetros, corpos das requisições e respostas pode ser consultada diretamente pelo Swagger durante a execução da API.



## Configuração do Banco de Dados

A aplicação utiliza MongoDB como banco de dados NoSQL.

Antes de executar a API, verifique as configurações de conexão presentes no arquivo:

```text
InovaGAB.Api/appsettings.json

```

Certifique-se de que a string de conexão e o nome do banco correspondem ao ambiente MongoDB utilizado.

O MongoDB deve estar em execução e acessível pela aplicação antes de iniciar a API.

## Estrutura do Projeto

O backend está organizado em camadas, separando responsabilidades entre controllers, serviços, repositórios, modelos e DTOs.

```text

InovaGAB.Api/
├── Controllers/
├── Data/
├── DTOs/
├── Models/
├── Repositories/
├── Services/
├── appsettings.json
├── Dockerfile
├── InovaGAB.Api.http
└── Program.cs

```

## Execução com Docker

O projeto possui um `Dockerfile` que permite executar a API em um container Docker.

Na raiz do projeto da API, execute:

```bash
docker build -t inovagab-api -f InovaGAB.Api/Dockerfile .
```

Após a criação da imagem, execute o container:

```bash
docker run -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Development -e Jwt__Key="SUA_CHAVE_JWT" -e MongoDbSettings__ConnectionString="mongodb://host.docker.internal:27017" inovagab-api
```

A API ficará disponível em:

```text
http://localhost:8080
```

Para utilizar todas as funcionalidades da aplicação, certifique-se de que o MongoDB configurado no `appsettings.json` esteja acessível pelo container.

## Testando a API

Após iniciar a aplicação, os endpoints podem ser testados diretamente pelo Swagger.

Acesse:

```text
https://localhost:<porta>/swagger
```

Também é possível utilizar o arquivo `InovaGAB.Api.http`, disponível no projeto, para realizar requisições diretamente pelo Visual Studio.

Para testar endpoints protegidos, primeiro realize o login e utilize o token JWT retornado na autenticação.

## Segurança

A API utiliza autenticação baseada em JWT (JSON Web Token) e controle de acesso por perfis de usuário.

As senhas dos usuários são armazenadas de forma segura utilizando hash, não sendo persistidas em texto puro.

Os endpoints protegidos utilizam autorização baseada em roles, garantindo que cada funcionalidade seja acessível apenas pelos perfis permitidos.

Informações sensíveis, como chaves JWT e strings de conexão, devem ser configuradas de forma adequada ao ambiente de execução e não devem ser expostas publicamente.

## Logs e Monitoramento

A aplicação utiliza os recursos de logging nativos do ASP.NET Core, com níveis de log configurados no arquivo `appsettings.json`.

Os logs da aplicação podem ser utilizados para acompanhar sua execução e auxiliar na identificação de erros e no diagnóstico do ambiente.

Mecanismos adicionais de observabilidade, como métricas e auditoria de operações, podem ser incorporados em evoluções futuras da aplicação.

## Considerações Finais

O backend do InovaGAB foi desenvolvido para atender aos requisitos da Sprint 2 do Challenge FIAP em parceria com o Grupo Águia Branca.

A solução fornece uma API REST estruturada para autenticação e controle de acesso, gerenciamento de estratégias, ideias e projetos, além da disponibilização de dados consolidados para o dashboard.

A arquitetura em camadas, a utilização do MongoDB, a autenticação JWT e os recursos de logging contribuem para uma solução organizada, segura e preparada para integração com o aplicativo desenvolvido na Sprint 1.

