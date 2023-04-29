using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafkaConsumer.Migrations
{
    /// <inheritdoc />
    public partial class updateMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FactoryId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_FactoryId",
                table: "Users",
                column: "FactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Factorys_FactoryId",
                table: "Users",
                column: "FactoryId",
                principalTable: "Factorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Factorys_FactoryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FactoryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FactoryId",
                table: "Users");
        }
    }
}
