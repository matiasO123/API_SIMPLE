using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Rest_Simple.Migrations
{
    /// <inheritdoc />
    public partial class professional_exp_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professional_exp",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Personid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professional_exp", x => x.id);
                    table.ForeignKey(
                        name: "FK_Professional_exp_persons_Personid",
                        column: x => x.Personid,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professional_exp_Personid",
                table: "Professional_exp",
                column: "Personid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Professional_exp");
        }
    }
}
