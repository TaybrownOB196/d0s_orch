# d0s_orch

## Orchestration Layer for d0s application

Dotnet generic host service

## Run

```
dotnet build Lists.Processor
dotnet run --project Lists.Processor
```

## Docker (Build)

### Lists Orchestrator Image

```
docker build . -t lists_orch
```

### Mysql Image

Requires following files in mysql/
setup_users.sql
create_tables.sql
insert_data.sql

```
docker build mysql/. -t lists_mysql
```

## Docker (Compose)

```
docker-compose up
```
