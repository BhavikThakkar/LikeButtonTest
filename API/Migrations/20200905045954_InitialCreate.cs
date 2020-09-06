using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LikeButton.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    DatePublished = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "postlike",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostId = table.Column<int>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    UserLike = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postlike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postlike_post_PostId",
                        column: x => x.PostId,
                        principalTable: "post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_postlike_PostId",
                table: "postlike",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postlike");

            migrationBuilder.DropTable(
                name: "post");
        }
    }
}
