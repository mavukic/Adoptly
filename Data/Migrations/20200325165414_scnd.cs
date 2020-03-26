using Microsoft.EntityFrameworkCore.Migrations;

namespace Adoptly.Data.Migrations
{
    public partial class scnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SklonisteId",
                table: "Post",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Skloniste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    SkraceniNaziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    Grad = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skloniste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ljubimac",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SklonisteId = table.Column<int>(nullable: true),
                    Vrsta = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Godine = table.Column<int>(nullable: false),
                    PosvajteljId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ljubimac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ljubimac_Skloniste_SklonisteId",
                        column: x => x.SklonisteId,
                        principalTable: "Skloniste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posvajatelj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    OIB = table.Column<string>(nullable: true),
                    BrMob = table.Column<string>(nullable: true),
                    LjubimacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posvajatelj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posvajatelj_Ljubimac_LjubimacId",
                        column: x => x.LjubimacId,
                        principalTable: "Ljubimac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_SklonisteId",
                table: "Post",
                column: "SklonisteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ljubimac_SklonisteId",
                table: "Ljubimac",
                column: "SklonisteId");

            migrationBuilder.CreateIndex(
                name: "IX_Posvajatelj_LjubimacId",
                table: "Posvajatelj",
                column: "LjubimacId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Skloniste_SklonisteId",
                table: "Post",
                column: "SklonisteId",
                principalTable: "Skloniste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Skloniste_SklonisteId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "Posvajatelj");

            migrationBuilder.DropTable(
                name: "Ljubimac");

            migrationBuilder.DropTable(
                name: "Skloniste");

            migrationBuilder.DropIndex(
                name: "IX_Post_SklonisteId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "SklonisteId",
                table: "Post");
        }
    }
}
