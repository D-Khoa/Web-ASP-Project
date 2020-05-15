using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Web_MES.Migrations
{
    public partial class AddUserProfileToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "u10000",
                columns: table => new
                {
                    u10001 = table.Column<string>(nullable: false),
                    u10002 = table.Column<string>(nullable: true),
                    u10003 = table.Column<string>(nullable: true),
                    u10004 = table.Column<string>(nullable: true),
                    u10005 = table.Column<string>(nullable: true),
                    u10006 = table.Column<string>(nullable: true),
                    u10007 = table.Column<string>(nullable: true),
                    u10008 = table.Column<string>(nullable: true),
                    u10009 = table.Column<string>(nullable: true),
                    u10010 = table.Column<string>(nullable: true),
                    u10011 = table.Column<int>(nullable: false),
                    u10012 = table.Column<int>(nullable: false),
                    u10013 = table.Column<int>(nullable: false),
                    u10014 = table.Column<int>(nullable: false),
                    u10015 = table.Column<int>(nullable: false),
                    u10016 = table.Column<double>(nullable: false),
                    u10017 = table.Column<double>(nullable: false),
                    u10018 = table.Column<double>(nullable: false),
                    u10019 = table.Column<double>(nullable: false),
                    u10020 = table.Column<double>(nullable: false),
                    u10021 = table.Column<bool>(nullable: false),
                    u10022 = table.Column<bool>(nullable: false),
                    u10023 = table.Column<bool>(nullable: false),
                    u10024 = table.Column<bool>(nullable: false),
                    u10025 = table.Column<bool>(nullable: false),
                    u10026 = table.Column<DateTime>(nullable: false),
                    u10027 = table.Column<DateTime>(nullable: false),
                    u10028 = table.Column<DateTime>(nullable: false),
                    u10029 = table.Column<DateTime>(nullable: false),
                    u10030 = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u10000", x => x.u10001);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u10000");
        }
    }
}
