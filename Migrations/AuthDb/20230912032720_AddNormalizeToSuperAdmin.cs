using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie_Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddNormalizeToSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1297941-f38a-43da-9b00-4926cf24b3e0",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06232b0c-7a5f-4a6c-afe3-2521069c46be", "SUPERADMIN@BLOGGIE.COM", "SUPERADMIN@BLOGGIE.COM", "AQAAAAEAACcQAAAAEGLFEytqk7/y2NyGkjdBNTNQvj8BcbWVRO0RNRPYsxkVINaUVM6FPusQ7k/LQQMHTw==", "5aa4a3f5-5755-4267-95a9-d92d3f9928cc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1297941-f38a-43da-9b00-4926cf24b3e0",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31f0a2fb-acdc-4201-b50c-608a133e70ca", null, null, "AQAAAAEAACcQAAAAEO/xn1ND7316KG2p/WlS3VnM78OGmDsAjpD6PW5w3vH84wqq6TP+rhQRzGAdPe5pCA==", "231bad82-a236-4a61-8fa5-dfd8bbcd13fc" });
        }
    }
}
