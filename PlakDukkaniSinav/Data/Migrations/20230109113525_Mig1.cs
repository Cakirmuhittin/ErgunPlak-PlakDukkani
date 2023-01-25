using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlakDukkaniSinav.Data.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albumler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sanatci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CikisTarihi = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndirimOrani = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatisDevamMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumler", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Albumler",
                columns: new[] { "Id", "AlbumAdi", "CikisTarihi", "Fiyat", "IndirimOrani", "Sanatci", "SatisDevamMi" },
                values: new object[,]
                {
                    { 1, "Back In Black", 1980, 600m, 0m, "AC/DC", true },
                    { 2, "Hurt", 2002, 400m, 0.12m, "Johhny Cash", false },
                    { 3, "Hotel California", 1976, 599m, 0.1m, "Eagles", true },
                    { 4, "Zeytin Yağlı Yaprak Dolması", 1995, 200m, 0m, "Grup Vitamin", false },
                    { 5, "Neden Saçların Beyazlamış Arkadaş", 2005, 250m, 0.2m, "Adnan Şenses", false },
                    { 6, "Bombabomba.com", 2006, 100m, 0.25m, "İsmail YK", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminler");

            migrationBuilder.DropTable(
                name: "Albumler");
        }
    }
}
