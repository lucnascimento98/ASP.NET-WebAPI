using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoPizza.Migrations
{
    public partial class RemovingClaimModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Claims_ClaimId",
                table: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaims_ClaimId",
                table: "RoleClaims");

            migrationBuilder.RenameColumn(
                name: "ClaimId",
                table: "RoleClaims",
                newName: "Claim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Claim",
                table: "RoleClaims",
                newName: "ClaimId");

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_ClaimId",
                table: "RoleClaims",
                column: "ClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Claims_ClaimId",
                table: "RoleClaims",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
