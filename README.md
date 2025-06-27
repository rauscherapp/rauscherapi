# rauscherapi
API Rauscher Traders

## Migrations

Para gerar uma nova migration e aplicá-la no banco de dados execute os comandos abaixo a partir da pasta `api-rauscher`:

```bash
dotnet ef migrations add <NomeDaMigration> --project Data.Migrations --startup-project Api
dotnet ef database update --project Data.Migrations --startup-project Api
```

O projeto `Data.Migrations` contem uma implementação de `IDesignTimeDbContextFactory<RauscherDbContext>` que habilita o uso das ferramentas de linha de comando.
