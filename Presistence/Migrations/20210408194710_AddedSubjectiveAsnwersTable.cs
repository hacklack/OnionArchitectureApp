using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class AddedSubjectiveAsnwersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkListSubjectiveQuestion_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingleAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: false),
                    CheckListSubjectiveAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListSubjectiveQuestion_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListSubjectiveAnswerQuestionId",
                        column: x => x.CheckListSubjectiveAnswerQuestionId,
                        principalTable: "checkListSubjectiveAnswerQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkListSubjectiveQuestion_Answers_userDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_CheckListSubjectiveAnswerQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "CheckListSubjectiveAnswerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_UserDetailsId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "UserDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkListSubjectiveQuestion_Answers");
        }
    }
}
