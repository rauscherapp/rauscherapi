# Rauscher Functions API

This project uses `appsettings.json` for configuration. When deploying, set the following environment variables so secrets are not committed in source control:

- `DefaultConnection` – SQL Server connection string.
- `CommoditiesApi__ApiKey` – commodities-api.com API key.
- `StripeApi__ApiKey` – Stripe API key.
- `ApplicationInsights__InstrumentationKey` – Application Insights key.
- `YahooFinanceApi__ApiKey` – Yahoo Finance API key.
- `AZURE_CONNECTION_STRING` – Azure Storage account connection string.
- `AZURE_CONTAINER_NAME` – Azure container for uploaded files.
- `SERILOG_INSTRUMENTATION_KEY` – instrumentation key used by Serilog.

Use `appsettings.json.example` as a starting point for your configuration file.
