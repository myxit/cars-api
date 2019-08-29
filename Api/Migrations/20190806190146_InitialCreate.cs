using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Antilopa.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nickname = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    RegistrationNr = table.Column<string>(nullable: true),
                    PicUrl = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");
            
            #region Popuplating initial data
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] {"Id", "Name", "Address"},
                values: new object[] {1, "Glukoza Ltd.", "Stockholm, Sweden, Luntmarksgatan 12"}
                );
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] {"Id", "Name", "Address"},
                values: new object[] {2, "Alpha AB", "Stockholm, Sweden, Kungsgatan, 8"}
            );

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] {"Id", "Model", "RegistrationNr", "OwnerId"},
                values: new object[] {1, "VOLVO XC60", "AI184", 1}
            );
           
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
