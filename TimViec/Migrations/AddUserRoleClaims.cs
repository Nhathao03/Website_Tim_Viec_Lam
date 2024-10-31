using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimViec.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
			 name: "AspNetRoleClaims",
			 columns: table => new
			 {
				 Id = table.Column<int>(type: "int", nullable: false)
					 .Annotation("SqlServer:Identity", "1, 1"),
				 RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				 ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				 ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			 },
			 constraints: table =>
			 {
				 table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				 table.ForeignKey(
					 name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					 column: x => x.RoleId,
					 principalTable: "AspNetRoles",
					 principalColumn: "Id",
					 onDelete: ReferentialAction.Cascade);
			 });
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusViewModel");
        }
    }
}
