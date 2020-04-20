using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SoldierInfo.Data.Migrations
{
    public partial class relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soldiers_Battles_BattleId",
                table: "Soldiers");

            migrationBuilder.DropIndex(
                name: "IX_Soldiers_BattleId",
                table: "Soldiers");

            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "Soldiers");

            migrationBuilder.CreateTable(
                name: "SecretIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RealName = table.Column<string>(nullable: true),
                    SoldierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecretIdentity_Soldiers_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoldierBattle",
                columns: table => new
                {
                    SoldierId = table.Column<int>(nullable: false),
                    BattleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierBattle", x => new { x.SoldierId, x.BattleId });
                    table.ForeignKey(
                        name: "FK_SoldierBattle_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoldierBattle_Soldiers_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecretIdentity_SoldierId",
                table: "SecretIdentity",
                column: "SoldierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldierBattle_BattleId",
                table: "SoldierBattle",
                column: "BattleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecretIdentity");

            migrationBuilder.DropTable(
                name: "SoldierBattle");

            migrationBuilder.AddColumn<int>(
                name: "BattleId",
                table: "Soldiers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Soldiers_BattleId",
                table: "Soldiers",
                column: "BattleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soldiers_Battles_BattleId",
                table: "Soldiers",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
