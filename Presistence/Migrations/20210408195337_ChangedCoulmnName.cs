using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class ChangedCoulmnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListSubjectiveAnswerQuestionId",
                table: "checkListSubjectiveQuestion_Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_userDetails_UserDetailsId",
                table: "checkListSubjectiveQuestion_Answers");

            migrationBuilder.RenameColumn(
                name: "UserDetailsId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CheckListSubjectiveAnswerQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "CheckListQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_UserDetailsId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "IX_checkListSubjectiveQuestion_Answers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_CheckListSubjectiveAnswerQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "IX_checkListSubjectiveQuestion_Answers_CheckListQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "CheckListQuestionId",
                principalTable: "checkListSubjectiveAnswerQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_userDetails_UserId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "UserId",
                principalTable: "userDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListQuestionId",
                table: "checkListSubjectiveQuestion_Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_userDetails_UserId",
                table: "checkListSubjectiveQuestion_Answers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "UserDetailsId");

            migrationBuilder.RenameColumn(
                name: "CheckListQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "CheckListSubjectiveAnswerQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_UserId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "IX_checkListSubjectiveQuestion_Answers_UserDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_checkListSubjectiveQuestion_Answers_CheckListQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                newName: "IX_checkListSubjectiveQuestion_Answers_CheckListSubjectiveAnswerQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListSubjectiveAnswerQuestionId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "CheckListSubjectiveAnswerQuestionId",
                principalTable: "checkListSubjectiveAnswerQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_checkListSubjectiveQuestion_Answers_userDetails_UserDetailsId",
                table: "checkListSubjectiveQuestion_Answers",
                column: "UserDetailsId",
                principalTable: "userDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
