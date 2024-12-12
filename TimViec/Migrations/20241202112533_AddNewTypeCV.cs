using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTypeCV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "typeId",
                table: "Template",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeCV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCV", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_typeId",
                table: "Template",
                column: "typeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_TypeCV_typeId",
                table: "Template",
                column: "typeId",
                principalTable: "TypeCV",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_TypeCV_typeId",
                table: "Template");

            migrationBuilder.DropTable(
                name: "TypeCV");

            migrationBuilder.DropIndex(
                name: "IX_Template_typeId",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "typeId",
                table: "Template");
        }
    }
}
