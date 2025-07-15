# 🩺 SAE - Sistema de Agendamento de Exames

Este projeto é um sistema simplificado de agendamento de exames, construído com .NET 8 e RabbitMQ, com arquitetura inspirada em DDD (Domain-Driven Design) e separado por responsabilidades entre API, mensageria e persistência.

---

## 🧱 Estrutura do Projeto

```
SAE/
├── SAE.API           # Camada de entrada - WebAPI para envio de agendamentos
├── SAE.Processor     # Consumer RabbitMQ que grava os agendamentos no SQLite
├── SAE.Shared        # Classes compartilhadas (DTOs, enums)
├── docker-compose.yml
```

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [SQLite](https://www.sqlite.org/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [GitHub Actions](https://github.com/features/actions)

---

## 📦 Como rodar localmente

### 1. Subir o RabbitMQ com Docker

```bash
docker compose up -d
```

Acesse a interface do RabbitMQ: [http://localhost:15672](http://localhost:15672)  
Usuário: `guest` | Senha: `guest`

---

### 2. Aplicar migrations no banco SQLite

```bash
cd SAE.Processor
dotnet ef database update
```

---

### 3. Rodar os projetos

#### API

```bash
dotnet run --project SAE.API
```

#### Processor (consumidor de mensagens)

```bash
dotnet run --project SAE.Processor
```

---

### 4. Testar envio de agendamento

Você pode testar com [Postman](https://www.postman.com/) ou `curl`:

```bash
curl -X POST http://localhost:5000/api/agendamento   -H "Content-Type: application/json"   -d '{
        "nomePaciente": "João da Silva",
        "emailPaciente": "joao@email.com",
        "tipoExame": "HEMOGRAMA_COMPLETO",
        "dataDesejada": "2025-07-21"
      }'
```

---

## 🤖 Integração Contínua com GitHub Actions

O pipeline executa automaticamente:

- Subida do RabbitMQ
- Aplicação de migrations
- Execução da API e do Processor
- Teste de envio de agendamento via `curl`
- Exibição dos logs no console

Arquivo do workflow: `.github/workflows/sae-ci.yml`

---

## 📂 Banco de Dados

O banco de dados usado é SQLite (arquivo `.db` local) para facilitar o estudo. Os dados são persistidos enquanto o arquivo `agendamentos.db` existir.

---

## ✍️ Autor

Desenvolvido por Emmanuel Martins – projeto de estudo de mensageria com RabbitMQ e arquitetura em .NET. Será usado como base para projeto final de Pós-graduação. 

---

## 📜 Licença

Projeto de estudo com uso livre para fins educacionais.
