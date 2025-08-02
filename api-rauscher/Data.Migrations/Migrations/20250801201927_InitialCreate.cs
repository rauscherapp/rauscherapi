using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiCredentials",
                columns: table => new
                {
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    ApiSecretHash = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiCredentials", x => x.ApiKey);
                });

            migrationBuilder.CreateTable(
                name: "AppParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StripeApiClientKey = table.Column<string>(type: "text", nullable: true),
                    StripeApiSecret = table.Column<string>(type: "text", nullable: true),
                    StripeWebhookSecret = table.Column<string>(type: "text", nullable: true),
                    StripeApiPriceId = table.Column<string>(type: "text", nullable: true),
                    StripeTrialPeriod = table.Column<int>(type: "integer", nullable: false),
                    CommoditiesApiKey = table.Column<string>(type: "text", nullable: true),
                    YahooFinanceApiKey = table.Column<string>(type: "text", nullable: true),
                    EmailSender = table.Column<string>(type: "text", nullable: true),
                    EmailReceiver = table.Column<string>(type: "text", nullable: true),
                    EmailPassword = table.Column<string>(type: "text", nullable: true),
                    SmtpServer = table.Column<string>(type: "text", nullable: true),
                    SmtpPort = table.Column<int>(type: "integer", nullable: false),
                    MarketOpeningHour = table.Column<string>(type: "text", nullable: true),
                    MarketClosingHour = table.Column<string>(type: "text", nullable: true),
                    YahooFinanceApiOn = table.Column<bool>(type: "boolean", nullable: false),
                    CommoditiesApiOn = table.Column<bool>(type: "boolean", nullable: false),
                    MinutesIntervalJob = table.Column<int>(type: "integer", nullable: false),
                    WhatsappNumber = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    InstagramUrl = table.Column<string>(type: "text", nullable: true),
                    WebSiteUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommodityOpenHighLowClose",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Base = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PriceOpen = table.Column<decimal>(type: "numeric(18,8)", nullable: false),
                    PriceHigh = table.Column<decimal>(type: "numeric(18,8)", nullable: false),
                    PriceLow = table.Column<decimal>(type: "numeric(18,8)", nullable: false),
                    PriceClose = table.Column<decimal>(type: "numeric(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityOpenHighLowClose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventName = table.Column<string>(type: "text", nullable: true),
                    EventDescription = table.Column<string>(type: "text", nullable: true),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventLocation = table.Column<string>(type: "text", nullable: true),
                    EventLink = table.Column<string>(type: "text", nullable: true),
                    Published = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    TITLE = table.Column<string>(type: "text", nullable: true),
                    SLUG = table.Column<string>(type: "text", nullable: true),
                    ICON = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    TITLE = table.Column<string>(type: "text", nullable: true),
                    CREATEDATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CONTENT = table.Column<string>(type: "text", nullable: true),
                    AUTHOR = table.Column<string>(type: "text", nullable: true),
                    VISIBLE = table.Column<bool>(type: "boolean", nullable: false),
                    PUBLISHEDAT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: true),
                    ImgUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    SYMBOLTYPE = table.Column<string>(type: "text", nullable: true),
                    Vendor = table.Column<string>(type: "text", nullable: true),
                    AppVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.Id);
                    table.UniqueConstraint("AK_Symbols_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CommoditiesRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: true),
                    Base = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SymbolCode = table.Column<string>(type: "text", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    VariationPrice = table.Column<decimal>(type: "numeric(18,4)", nullable: true),
                    VariationPricePercent = table.Column<decimal>(type: "numeric(18,4)", nullable: true),
                    isUp = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommoditiesRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommoditiesRate_Symbols_SymbolCode",
                        column: x => x.SymbolCode,
                        principalTable: "Symbols",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesRate_SymbolCode",
                table: "CommoditiesRate",
                column: "SymbolCode");

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_Code",
                table: "Symbols",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "ApiCredentials");

            migrationBuilder.DropTable(
                name: "AppParameters");

            migrationBuilder.DropTable(
                name: "CommoditiesRate");

            migrationBuilder.DropTable(
                name: "CommodityOpenHighLowClose");

            migrationBuilder.DropTable(
                name: "EventRegistry");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Symbols");
        }
    }
}
