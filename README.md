# 📚 BookCatalog - Sistema de Catálogo de Livros

Sistema completo de gerenciamento de catálogo de livros com backend em .NET 8 e frontend em Angular.

## 🚀 Execução com Docker (Recomendado)

### Pré-requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução
- Git (para clonar o repositório)

### Como executar

1. **Clone o repositório**

   ```bash
   git clone https://github.com/helton-evambi/Desafio-Ajaxti.git
   cd Desafio-Ajaxti
   ```

2. **Execute com Docker Compose**

   ```bash
   docker compose up --build
   ```

3. **Acesse as aplicações**
   - **Frontend (Angular)**: http://localhost:4200
   - **Backend API**: http://localhost:8080
   - **Swagger UI**: http://localhost:8080/swagger
   - **Base de dados**: PostgreSQL na porta 5432

### Comandos úteis do Docker

```bash
# Parar todos os serviços
docker compose down

# Parar e remover volumes (limpar dados)
docker compose down -v

# Ver logs de todos os serviços
docker compose logs -f

# Ver logs específicos
docker compose logs -f catalog.api
docker compose logs -f angular-app
docker compose logs -f catalogdb

# Rebuildar apenas um serviço
docker compose up --build angular-app
docker compose up --build catalog.api

# Executar em background
docker compose up -d --build
```

## 🛠️ Execução Manual (Desenvolvimento)

### Pré-requisitos

#### Backend (.NET 8)

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)

#### Frontend (Angular)

- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli): `npm install -g @angular/cli`

### Configuração da Base de Dados

1. **Instalar PostgreSQL**

   - Criar base de dados: `BookCatalogDb`
   - Usuário: `postgres`
   - Password: `postgres`
   - Porta: `5432`

2. **Configurar connection string**
   ```json
   // backend/src/BookCatalog.API/appsettings.Development.json
   {
     "ConnectionStrings": {
       "Database": "Server=localhost;Port=5432;Database=BookCatalogDb;User Id=postgres;Password=postgres;Include Error Detail=true"
     }
   }
   ```

### Executar Backend (.NET API)

1. **Navegar para a pasta da API**

   ```bash
   cd backend/src/BookCatalog.API
   ```

2. **Restaurar dependências**

   ```bash
   dotnet restore
   ```

3. **Executar a aplicação**

   ```bash
   dotnet run
   ```

4. **Acessar**
   - API: https://localhost:7051 ou http://localhost:5051
   - Swagger: https://localhost:7051/swagger

### Executar Frontend (Angular)

1. **Navegar para a pasta frontend**

   ```bash
   cd frontend
   ```

2. **Instalar dependências**

   ```bash
   npm install
   ```

3. **Configurar endpoint da API**

   ```typescript
   // frontend/src/environments/environment.ts
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:7051/api/v1.0', // Ajustar conforme necessário
   }
   ```

4. **Executar aplicação**

   ```bash
   npm start
   # ou
   ng serve
   ```

5. **Acessar**
   - Frontend: http://localhost:4200

## 🏗️ Arquitetura e Tecnologias

### Stack Tecnológica

#### Backend - .NET 8 (Obrigatório)

- **Framework**: ASP.NET Core Web API
- **Motivo da escolha**: Tecnologia obrigatória definida no projeto
- **Vantagens**:
  - Performance excelente e baixo consumo de memória
  - Ecossistema robusto com Entity Framework Core
  - Suporte nativo para Docker e containerização
  - Documentação automática com Swagger/OpenAPI
  - Arquitetura Clean Architecture implementada

#### Frontend - Angular 18

- **Framework**: Angular com TypeScript
- **Motivos da escolha**:
  - **Estrutura enterprise**: Ideal para aplicações de médio/grande porte
  - **TypeScript nativo**: Tipagem forte reduz erros e melhora manutenibilidade
  - **Arquitetura modular**: Componentes reutilizáveis e organização escalável
  - **Ecossistema maduro**: Vasta biblioteca de componentes (Angular Material, PrimeNG)
  - **CLI poderosa**: Geração automática de código e build otimizado
  - **Testes integrados**: Jasmine e Karma incluídos por padrão
  - **PWA ready**: Suporte nativo para Progressive Web Apps

#### Base de Dados - PostgreSQL 17

- **SGBD**: PostgreSQL (Object-Relational Database)
- **Motivos da escolha**:
  - **Open Source**: Sem custos de licenciamento
  - **Performance superior**: Otimizado para cargas de trabalho complexas
  - **Conformidade ACID**: Transações confiáveis e consistência de dados
  - **Extensibilidade**: Suporte a JSON, arrays, tipos customizados
  - **Escalabilidade**: Excelente para aplicações que crescem
  - **Compatibilidade**: Funciona perfeitamente com Entity Framework Core
  - **Docker-friendly**: Imagens oficiais bem mantidas

### Alternativas Consideradas

#### Frontend

- **React**: Mais flexível, mas requer mais configuração inicial
- **Vue.js**: Curva de aprendizado menor, mas ecossistema menor
- **Svelte**: Performance excelente, mas menos maduro para enterprise

#### Base de Dados

- **SQL Server**: Licenciamento caro, mas integração nativa com .NET
- **MySQL**: Popular, mas menos recursos avançados que PostgreSQL
- **SQLite**: Simples para desenvolvimento, mas limitado para produção

### Arquitetura do Sistema

```
┌─────────────────┐    HTTP/REST    ┌─────────────────┐    Entity Framework    ┌─────────────────┐
│   Angular SPA   │ ◄──────────────► │   .NET Web API  │ ◄─────────────────────► │   PostgreSQL    │
│                 │                  │                 │                         │                 │
│ - Components    │                  │ - Controllers   │                         │ - Tables        │
│ - Services      │                  │ - Business Logic│                         │ - Relationships │
│ - Routing       │                  │ - Data Access   │                         │ - Constraints   │
└─────────────────┘                  └─────────────────┘                         └─────────────────┘
```

## 📁 Estrutura do Projeto

```
Desafio-Ajaxti/
├── backend/
│   ├── src/
│   │   ├── BookCatalog.API/          # API principal
│   │   ├── BookCatalog.Application/  # Lógica de aplicação
│   │   ├── BookCatalog.Domain/       # Entidades de domínio
│   │   ├── BookCatalog.Infrastructure/ # Acesso a dados
│   │   └── BookCatalog.Shared/       # Utilitários compartilhados
│   └── Dockerfile
├── frontend/
│   ├── src/
│   │   ├── app/                      # Componentes Angular
│   │   ├── environments/             # Configurações de ambiente
│   │   └── assets/                   # Recursos estáticos
│   ├── Dockerfile
│   └── package.json
├── docker-compose.yml               # Orquestração Docker
└── README.md
```

## 🐛 Resolução de Problemas

### Docker

**Erro de porta ocupada:**

```bash
# Verificar que processos estão usando as portas
netstat -ano | findstr :4200
netstat -ano | findstr :8080
netstat -ano | findstr :5432

# Parar serviços conflituosos ou usar portas diferentes
```

**Problemas de volume PostgreSQL:**

```bash
# Limpar volumes e recomeçar
docker compose down -v
docker volume prune -f
docker compose up --build
```

**Erro de certificado SSL:**

```bash
# Certificado é gerenciado automaticamente no Docker
# Se necessário, force apenas HTTP removendo HTTPS_PORTS
```

### Manual

**Erro de conexão com base de dados:**

- Verificar se PostgreSQL está em execução
- Confirmar credenciais na connection string
- Testar conexão: `psql -h localhost -U postgres -d BookCatalogDb`

**Problemas com Angular:**

- Limpar cache: `npm cache clean --force`
- Reinstalar: `rm -rf node_modules && npm install`
- Verificar versão do Node: `node --version` (deve ser 18+)

**Problemas com .NET:**

- Verificar versão: `dotnet --version` (deve ser 8.0+)
- Limpar e rebuildar: `dotnet clean && dotnet build`
- Verificar se Entity Framework está instalado: `dotnet ef --version`

## 📝 Notas Importantes

- O Docker Compose já configura automaticamente toda a infraestrutura
- Para desenvolvimento, recomenda-se usar Docker para consistência
- O Swagger está disponível apenas em modo Development
- Logs detalhados estão habilitados em Development
- As migrações são executadas automaticamente no Docker

## 💡 Decisões de Design

### Por que Angular?

1. **Enterprise-ready**: Estrutura robusta para aplicações profissionais
2. **Produtividade**: CLI e ferramentas que aceleram desenvolvimento
3. **Manutenibilidade**: TypeScript + arquitetura modular
4. **Ecossistema**: Componentes UI prontos (Angular Material)
5. **Futuro-proof**: Suporte LTS e atualizações regulares

### Por que PostgreSQL?

1. **Custo-benefício**: Open source com recursos enterprise
2. **Performance**: Otimizado para consultas complexas
3. **Confiabilidade**: ACID compliance e backup robusto
4. **Flexibilidade**: Suporte a JSON, arrays, tipos customizados
5. **Comunidade**: Documentação excelente e suporte ativo

### Clean Architecture (.NET)

```
┌─────────────────────────────────────────────────────────────┐
│                    BookCatalog.API                          │
│                  (Controllers, Middleware)                  │
├─────────────────────────────────────────────────────────────┤
│                BookCatalog.Application                      │
│            (Use Cases, DTOs, Interfaces)                    │
├─────────────────────────────────────────────────────────────┤
│                  BookCatalog.Domain                         │
│              (Entities, Value Objects)                      │
├─────────────────────────────────────────────────────────────┤
│               BookCatalog.Infrastructure                    │
│            (Data Access, External Services)                 │
└─────────────────────────────────────────────────────────────┘
```

## 🎯 Endpoints Principais

### API (.NET)

- `GET /api/v1.0/authors` - Listar autores
- `GET /api/v1.0/books` - Listar livros
- `GET /api/v1.0/genres` - Listar géneros
- Documentação completa: `/swagger`

### Frontend (Angular)

- `/` - Dashboard principal
- `/authors` - Gestão de autores
- `/books` - Gestão de livros
- `/genres` - Gestão de géneros
