using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "BottomCategoryId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MiddleCategoryId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TopCategoryId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiddleCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TopCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiddleCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MiddleCategory_TopCategory_TopCategoryId",
                        column: x => x.TopCategoryId,
                        principalTable: "TopCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BottomCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MiddleCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BottomCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BottomCategory_MiddleCategory_MiddleCategoryId",
                        column: x => x.MiddleCategoryId,
                        principalTable: "MiddleCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BottomCategoryId",
                table: "Categories",
                column: "BottomCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MiddleCategoryId",
                table: "Categories",
                column: "MiddleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TopCategoryId",
                table: "Categories",
                column: "TopCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BottomCategory_MiddleCategoryId",
                table: "BottomCategory",
                column: "MiddleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MiddleCategory_TopCategoryId",
                table: "MiddleCategory",
                column: "TopCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_BottomCategory_BottomCategoryId",
                table: "Categories",
                column: "BottomCategoryId",
                principalTable: "BottomCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MiddleCategory_MiddleCategoryId",
                table: "Categories",
                column: "MiddleCategoryId",
                principalTable: "MiddleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_TopCategory_TopCategoryId",
                table: "Categories",
                column: "TopCategoryId",
                principalTable: "TopCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_BottomCategory_BottomCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MiddleCategory_MiddleCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_TopCategory_TopCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "BottomCategory");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "MiddleCategory");

            migrationBuilder.DropTable(
                name: "TopCategory");

            migrationBuilder.DropIndex(
                name: "IX_Categories_BottomCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MiddleCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TopCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BottomCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MiddleCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TopCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
