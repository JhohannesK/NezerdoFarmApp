using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NezerdoFarmApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdForFarmOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FarmOwner",
                table: "Farms",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FarmOwner",
                table: "Farms",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
