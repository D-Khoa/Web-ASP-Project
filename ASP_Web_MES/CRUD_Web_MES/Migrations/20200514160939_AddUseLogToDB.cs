using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Web_MES.Migrations
{
    public partial class AddUseLogToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "u20000",
                columns: table => new
                {
                    u20001 = table.Column<string>(nullable: false),
                    u20002 = table.Column<string>(nullable: true),
                    u20003 = table.Column<string>(nullable: true),
                    u20004 = table.Column<string>(nullable: true),
                    u20005 = table.Column<string>(nullable: true),
                    u20006 = table.Column<string>(nullable: true),
                    u20007 = table.Column<string>(nullable: true),
                    u20008 = table.Column<string>(nullable: true),
                    u20009 = table.Column<string>(nullable: true),
                    u20010 = table.Column<string>(nullable: true),
                    u20011 = table.Column<int>(nullable: false),
                    u20012 = table.Column<int>(nullable: false),
                    u20013 = table.Column<int>(nullable: false),
                    u20014 = table.Column<int>(nullable: false),
                    u20015 = table.Column<int>(nullable: false),
                    u20016 = table.Column<double>(nullable: false),
                    u20017 = table.Column<double>(nullable: false),
                    u20018 = table.Column<double>(nullable: false),
                    u20019 = table.Column<double>(nullable: false),
                    u20020 = table.Column<double>(nullable: false),
                    u20021 = table.Column<bool>(nullable: false),
                    u20022 = table.Column<bool>(nullable: false),
                    u20023 = table.Column<bool>(nullable: false),
                    u20024 = table.Column<bool>(nullable: false),
                    u20025 = table.Column<bool>(nullable: false),
                    u20026 = table.Column<DateTime>(nullable: false),
                    u20027 = table.Column<DateTime>(nullable: false),
                    u20028 = table.Column<DateTime>(nullable: false),
                    u20029 = table.Column<DateTime>(nullable: false),
                    u20030 = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u20000", x => x.u20001);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u20000");
        }
    }
}
