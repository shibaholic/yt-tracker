# Database documentation

The database is MS SQL Server 2022.

## SQL queries

- [`EXEC_NOTE.sql`](../src/database/EXEC_NOTE.sql) for writing test queries

## Local development

The `mcr.microsoft.com/mssql/server:2022-latest`  docker image is used.

Use Docker Desktop to stop and run the container once created.

### Container creation

```bash
docker run \
    -e "ACCEPT_EULA=Y" \
    -e "MSSQL_SA_PASSWORD=StrongPassword1" \
    -p 1433:1433 --name local_sql --hostname local_sql \
    -d mcr.microsoft.com/mssql/server:2022-latest
```

### Database and schema

Database: sql_dev

Schema: dbo

### Admin user

Username: sa

Password: StrongPassword1

Connect at: localhost:1433

## Database UML diagram

UML diagram will not be drawn for the database, as it is the [`domain entities`](entities%20UML%20diagram.drawio.png) that the database changes to. 