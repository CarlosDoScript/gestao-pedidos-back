# 📺 Gestão de Pedidos - Backend (.NET 8 | Clean Architecture | CQRS | DDD)

Este projeto foi desenvolvido como parte de um desafio técnico com foco em **.NET**, aplicando padrões de arquitetura moderna como **Clean Architecture**, **CQRS**, **DDD**, e persistência em **SQL Server (EF Core)** e **MongoDB (para leitura)**.

---

## 🔧 Tecnologias e Padrões Utilizados

- ✅ .NET 8 (ASP.NET Core)
- ✅ Entity Framework Core (SQL Server)
- ✅ MongoDB.Driver
- ✅ CQRS (Command Query Responsibility Segregation)
- ✅ Clean Architecture
- ✅ DDD (Domain-Driven Design)
- ✅ Value Objects
- ✅ Repository Pattern
- ✅ Fluent Mapping com EntityTypeConfiguration
- ✅ MediatR (Commands e Queries desacoplados)
- ✅ FluentValidation
- ✅ Swagger
- ✅ Middleware de Exceções Global
- ✅ Docker e Docker Compose

---

## 🧱 Boas Práticas Aplicadas

- 📆 Sepação de camadas com Clean Architecture
- 🔠 DDD: Entidades com regras, Value Objects, Agregados
- 🔀 Repositórios com implementações específicas
- ⚙️ Leitura (queries) separada da escrita (commands)
- 📊 MongoDB como store de leitura otimizado
- 🚫 Middleware global de tratamento de exceções
- 🗒️ Respostas padronizadas com `Resultado<T>`
- 🤍 Fluent Mapping das entidades com `IEntityTypeConfiguration`

---

## 📁 Estrutura de Pastas

```text
├── Docker-Compose.yml             # Yaml do projeto configurado
├── Gestao.Pedidos.API             # Apresentação (Controllers, Middlewares, Swagger)
├── Gestao.Pedidos.Application     # CQRS (Commands, Queries, Responsibility, Segregation), Fluent Validation, View Model, Input Model
├── Gestao.Pedidos.Core            # Domínio (Entidades, VOs, Regras, Enums, Interfaces)
├── Gestao.Pedidos.Infrastructure  # EF Core, MongoDB, SQL Server, Repositórios, Contextos
├── Gestao.Pedidos.SharedKernel    # Tipos genéricos e utilitários como Resultado<T>
```

---

## 🗃️ Banco de Dados

### SQL Server (Persistência)

- Customer, Product, Order, OrderItem

### MongoDB (Leitura)

- Order, Customer e Product para consultas performáticas

---

## 📌 Entidades Relacionais

### `Customer`

- `Id`, `Name`, `Email`, `Phone`

### `Product`

- `Id`, `Name`, `Price`

### `Order`

- `Id`, `CustomerId`, `OrderDate`, `TotalAmount`, `Status`

### `OrderItem`

- `Id`, `OrderId`, `ProductId`, `ProductName`, `Quantity`, `UnitPrice`, `TotalPrice`

---

## ✅ Funcionalidades da API

- 📅 Adicionar Pedido
- ❌ Remover Pedido
- 📜 Atualizar Pedido
- 📄 Listar Pedidos (paginado)
- 🔍 Detalhar Pedido com itens

---

##  Executando com Docker Compose

Este projeto já vem com um `docker-compose.yml` configurado com:

- SQL Server
- MongoDB
- Api Configurada

### 📦 Passos para subir o ambiente completo:

```bash
# 1. Clone o repositório
git clone https://github.com/CarlosDoScript/gestao-pedidos-back.git
cd gestao-pedidos-back

# 2. Suba o ambiente (SQL Server + MongoDB)
docker-compose up -d

# 3. Restaure pacotes e atualize a base SQL
dotnet restore
dotnet ef database update -p Gestao.Pedidos.Infrastructure -s Gestao.Pedidos.API

# 4. Execute a aplicação
dotnet run --project Gestao.Pedidos.API
```

---

## Swagger - Documentação da API

Acesse a documentação interativa:

```
http://localhost:8080/swagger/index.html
```

---

> Projeto criado com foco técnico para demonstrar domínio em arquitetura moderna, boas práticas e escalabilidade.

