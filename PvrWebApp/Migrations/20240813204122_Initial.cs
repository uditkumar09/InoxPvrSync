using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PvrWebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PvrUsers",
                columns: table => new
                {
                    PvrUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Phone = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PvrUsers", x => x.PvrUserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PvrUsers");
        }
    }
}
