using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class updatetablefavouriteJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "favourite_Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R1_Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R2_Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R3_Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favourite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite_Jobs", x => x.Id);
                });    
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropTable(
                name: "favourite_Jobs");
        }
    }
}
