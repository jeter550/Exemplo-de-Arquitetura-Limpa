# Exemplo de Arquitetura Limpa com .NET
Este projeto foi criado para demostrar o uso de princípios do **SOLID** e **Arquitetura Limpa**, utilizando **CQRS** com **Mediator** e garantindo **idempotência** para evitar efeitos colaterais em operações repetidas.

## O que este projeto faz

Este projeto é uma API para **gestão de movimentação bancária**, permitindo criar movimentações financeiras e consultar saldos de contas correntes. Para garantir a segurança e consistência dos dados, o sistema implementa **idempotência**, evitando a duplicidade de operações.

- **Criação de Movimentação Bancária**:

  - Valida a conta e o tipo de transação (Crédito/Débito).
  - Garante que o valor da transação é positivo.
  - Usa um repositório de idempotência para evitar processamentos repetidos.
  - Registra a movimentação e atualiza o saldo da conta.

- **Consulta de Saldo**:

  - Valida se a conta existe e está ativa.
  - Verifica a idempotência para evitar consultas repetitivas.
  - Retorna o saldo da conta corrente com informações adicionais.

## Tecnologias Utilizadas

- .NET 8 (ASP.NET Core)
- CQRS (Command Query Responsibility Segregation)
- Mediator (MediatR)
- Padrão Repository
- Dapper
- Testes Unitários e de Integração

## Estrutura do Projeto

```
Projeto/
│── Application/
│   ├── Handlers/         # Manipuladores de comandos e queries
│   ├── Services/         # Serviços e casos de uso (Não ésta sendo uitlizada. Nesse projeto foi criado apenas para explicar o uso.)
│   ├── Interfaces/       # Contratos para comunicação entre camadas
│
│── Domain/
│   ├── Entities/         # Entidades do domínio
│   ├── Enumerators/      # Enums do domínio
│   ├── Interfaces/       # Repositórios e abstrações do domínio
│
│── Infrastructure/
│   ├── Database/		  # Inicialilização de banco de dados
│   ├── Repositories/     # Implementações de repositório e banco de dados 
│   ├── Services/         # Integração com serviços externos (Não ésta sendo uitlizada. Nesse projeto foi criado apenas para explicar o uso)
│
│── Tests/Application/    # Testes unitários e de integração da camada Application
│
│── WebAPI/
│   ├── Controllers/      # Controladores REST para interação com a API
```

## Principais Conceitos Implementados

### 🔹 CQRS (Command Query Responsibility Segregation)

A arquitetura **CQRS** separa **comandos** (operações de escrita) de **queries** (operações de leitura), melhorando a escalabilidade e organização do código.

### 🔹 Mediator

Utilizamos o **MediatR** para desacoplar os controladores dos casos de uso. Isso melhora a manutenção e facilita testes unitários.

### 🔹 Idempotência

Garantimos que operações críticas sejam executadas apenas uma vez, mesmo que recebam chamadas repetidas. Isso é feito através de **tokens de idempotência** ou controle no banco de dados.

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/jeter550/Exemplo-de-Arquitetura-Limpa.git
   ```
2. Acesse a pasta do projeto:
   ```bash
   cd src
   ```
3. Instale as dependências:
   ```bash
   dotnet restore
   ```
4. Execute a API:
   ```bash
   dotnet run
   ```

## Como Rodar os Testes

Para rodar os testes unitários e de integração, use:

```bash
dotnet test
```

## Massa de teste
- **Contas ativas**:
B6BAFC09 -6967-ED11-A567-055DFA4A16C9
FA99D033-7067-ED11-96C6-7C5DFA4A16C9
382D323D-7067-ED11-8866-7D5DFA4A16C9

- **Contas inativas**:

F475F943-7067-ED11-A06B-7E5DFA4A16C9
BCDACA4A-7067-ED11-AF81-825DFA4A16C9
D2E02051-7067-ED11-94C0-835DFA4A16C9

## Contribuição

Fique à vontade para abrir **Issues** e **Pull Requests** para melhorias!