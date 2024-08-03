using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerManagement.Migrations
{
    public partial class EditCustomerAddressEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Districts_DistrictId",
                table: "CustomerAddresss");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Provinces_ProvinceId",
                table: "CustomerAddresss");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Wards_WardId",
                table: "CustomerAddresss");

            migrationBuilder.RenameColumn(
                name: "WardId",
                table: "CustomerAddresss",
                newName: "WardCode");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "CustomerAddresss",
                newName: "ProvinceCode");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "CustomerAddresss",
                newName: "DistrictCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_WardId",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_WardCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_ProvinceId",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_ProvinceCode");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_DistrictId",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_DistrictCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Districts_DistrictCode",
                table: "CustomerAddresss",
                column: "DistrictCode",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Provinces_ProvinceCode",
                table: "CustomerAddresss",
                column: "ProvinceCode",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Wards_WardCode",
                table: "CustomerAddresss",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Districts_DistrictCode",
                table: "CustomerAddresss");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Provinces_ProvinceCode",
                table: "CustomerAddresss");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresss_Wards_WardCode",
                table: "CustomerAddresss");

            migrationBuilder.RenameColumn(
                name: "WardCode",
                table: "CustomerAddresss",
                newName: "WardId");

            migrationBuilder.RenameColumn(
                name: "ProvinceCode",
                table: "CustomerAddresss",
                newName: "ProvinceId");

            migrationBuilder.RenameColumn(
                name: "DistrictCode",
                table: "CustomerAddresss",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_WardCode",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_WardId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_ProvinceCode",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresss_DistrictCode",
                table: "CustomerAddresss",
                newName: "IX_CustomerAddresss_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Districts_DistrictId",
                table: "CustomerAddresss",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Provinces_ProvinceId",
                table: "CustomerAddresss",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresss_Wards_WardId",
                table: "CustomerAddresss",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Code");
        }
    }
}
