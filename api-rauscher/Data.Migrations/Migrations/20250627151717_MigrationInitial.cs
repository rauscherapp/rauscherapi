using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations.Migrations
{
    public partial class MigrationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiCredentials",
                columns: table => new
                {
                    ApiKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiSecretHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiCredentials", x => x.ApiKey);
                });

            migrationBuilder.CreateTable(
                name: "AppParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeApiClientKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeApiSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeWebhookSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeApiPriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripeTrialPeriod = table.Column<int>(type: "int", nullable: false),
                    CommoditiesApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YahooFinanceApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    MarketOpeningHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketClosingHour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YahooFinanceApiOn = table.Column<bool>(type: "bit", nullable: false),
                    CommoditiesApiOn = table.Column<bool>(type: "bit", nullable: false),
                    MinutesIntervalJob = table.Column<int>(type: "int", nullable: false),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommodityOpenHighLowClose",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Base = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PriceOpen = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PriceHigh = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PriceLow = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    PriceClose = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityOpenHighLowClose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLUG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICON = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AUTHOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VISIBLE = table.Column<bool>(type: "bit", nullable: false),
                    PUBLISHEDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SYMBOLTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppVisible = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: true),
                    Base = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SymbolCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    VariationPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    VariationPricePercent = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    isUp = table.Column<bool>(type: "bit", nullable: false)
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
