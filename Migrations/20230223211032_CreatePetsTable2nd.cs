using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_bakery.Migrations
{
    public partial class CreatePetsTable2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_ownedById",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "ownedById",
                table: "Pets",
                newName: "petOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_ownedById",
                table: "Pets",
                newName: "IX_Pets_petOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_petOwnerId",
                table: "Pets",
                column: "petOwnerId",
                principalTable: "PetOwners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_petOwnerId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "petOwnerId",
                table: "Pets",
                newName: "ownedById");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_petOwnerId",
                table: "Pets",
                newName: "IX_Pets_ownedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_ownedById",
                table: "Pets",
                column: "ownedById",
                principalTable: "PetOwners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
