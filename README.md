# TEcomerc - Teste Técnico de E-commerce

Um backend minimalista de e-commerce implementado em .NET 10, voltado para teste técnico. Este repositório inclui uma API, persistência com SQLite, testes unitários e de ponta a ponta (E2E), além de um exemplo de autenticação utilizando JWT.

## Funcionalidades do projeto

- API REST para gerenciamento de pedidos (criar, ler, listar, atualizar)
- Endpoint de autenticação que emite tokens JWT
- Implementações de repositório baseadas em SQLite (EF Core)
- Testes de ponta a ponta usando `Microsoft.AspNetCore.Mvc.Testing`
- Separação clara entre as camadas de Aplicação (Application), Domínio (Domain) e Infraestrutura (Infrastructure)

## Tecnologias utilizadas

- .NET 10
- C#
- ASP.NET Core Web API
- Entity Framework Core com SQLite
- xUnit para testes
- Microsoft.AspNetCore.Mvc.Testing para testes E2E

## Como começar (Visual Studio 2022)

1. Abra a solução no Visual Studio 2022.
2. Certifique-se de ter o SDK do .NET 10 instalado.
3. Compilar a solução: __Compilação > Compilar Solução__ ou execute o comando __BuildSolution__.
4. Defina o projeto da API como o projeto de inicialização e execute (F5) ou execute sem depurar (Ctrl+F5).

## Instruções de linha de comando (CLI)

A partir da raiz do repositório:

- Restaurar e compilar

    ```bash
    dotnet restore
    dotnet build
    ```

- Executar a API

    ```bash
    dotnet run --project src/Api/Api.csproj
    ```

- Executar os testes

    ```bash
    dotnet test
    ```

## Configuração e Banco de Dados

- A persistência padrão utiliza o SQLite localizado conforme as configurações da aplicação. O caminho do arquivo do banco de dados e as migrações (se houver) estão configurados no projeto de infraestrutura.
- Para redefinir o banco de dados, exclua o arquivo SQLite utilizado pela aplicação (caminho configurável) e reinicie-a. Na primeira execução, a aplicação irá inicializar o esquema.

## Autenticação

- A API expõe um endpoint de autenticação utilizado pelos testes E2E (veja `tests/E2E/%auth%login/POST.cs`).
- Credenciais de cliente utilizadas nos testes:
  - **Usuário:** `dev@martech.com`
  - **Senha:** `Senha@123`
- Um login bem-sucedido retorna uma resposta em JSON contendo a string do token `JWT`. Utilize esse token em `Authorization: Bearer <token>` ao chamar endpoints protegidos.

## Exemplo de requisição (login)

**POST** `/auth/login`

```http
POST /auth/login HTTP/1.1
Content-Type: application/json

{ "user": "dev@martech.com", "password": "Senha@123" }
```

## API de Pedidos (visão geral)

Funcionalidades típicas (verifique os controllers para rotas e dados completos):

- Criar pedido
- Ler pedido por ID
- Listar pedidos com paginação e inclusão opcional de itens
- Atualizar campos do pedido (status, customerId, etc.)

A infraestrutura inclui o `SqliteOrderRepository`, que persiste os pedidos e suporta paginação e inclusão de itens.

## Testes

- Testes unitários e testes E2E estão incluídos. Os testes E2E utilizam um servidor de testes em memória criado com `WebApplicationFactory<Program>` e têm como alvo os fluxos de autenticação e pedidos.
- Execute todos os testes com `dotnet test`.

Este projeto é fornecido para fins de avaliação e testes técnicos. Verifique a licença ou política do repositório na raiz, caso esteja presente.
