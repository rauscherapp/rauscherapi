# rauscherapi
API Rauscher Traders

## Migrations

Para gerar uma nova migration e aplicá-la no banco de dados execute os comandos abaixo a partir da pasta `api-rauscher`:

```bash
# exemplo para a primeira migration
dotnet ef migrations add InitialCreate --project Data.Migrations --startup-project Api
dotnet ef database update --project Data.Migrations --startup-project Api
```

Antes de rodar os comandos, verifique a string de conexão no arquivo
`api-rauscher/Data/appsettings.json` (ou defina a variável de ambiente
`DefaultConnection`) para que ela aponte para uma instância do SQL Server que
esteja acessível a partir da sua máquina.

O projeto `Data.Migrations` contem uma implementação de `IDesignTimeDbContextFactory<RauscherDbContext>` que habilita o uso das ferramentas de linha de comando.
