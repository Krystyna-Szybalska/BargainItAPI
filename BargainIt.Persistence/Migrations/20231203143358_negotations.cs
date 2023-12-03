using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BargainIt.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class negotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NegotiationEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProposedPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegotiationEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegotiationEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationEntity_ProductId",
                table: "NegotiationEntity",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegotiationEntity");
        }
    }
}
