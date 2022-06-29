using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class removedFewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkListMultipleAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "checkListSingleAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "checkListYesNoAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "checkListMultipleAnswerQuestion");

            migrationBuilder.DropTable(
                name: "checkListSingleAnswerQuestion");

            migrationBuilder.DropTable(
                name: "checkListYesNoAnswerQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkListMultipleAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListMultipleAnswerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListMultipleAnswerQuestion_checkListQuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "checkListQuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListSingleAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListSingleAnswerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListSingleAnswerQuestion_checkListQuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "checkListQuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListYesNoAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListYesNoAnswerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListYesNoAnswerQuestion_checkListQuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "checkListQuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListMultipleAnswerQuestion_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListMultipleAnswerQuestionId = table.Column<int>(type: "int", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListYesNoAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListMultipleAnswerQuestion_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListMultipleAnswerQuestion_Answers_checkListMultipleAnswerQuestion_CheckListMultipleAnswerQuestionId",
                        column: x => x.CheckListMultipleAnswerQuestionId,
                        principalTable: "checkListMultipleAnswerQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkListMultipleAnswerQuestion_Answers_userDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListSingleAnswerQuestion_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListSingleAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SingleAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleAnswerDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListSingleAnswerQuestion_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListSingleAnswerQuestion_Answers_checkListSingleAnswerQuestion_CheckListSingleAnswerQuestionId",
                        column: x => x.CheckListSingleAnswerQuestionId,
                        principalTable: "checkListSingleAnswerQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkListSingleAnswerQuestion_Answers_userDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListYesNoAnswerQuestion_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListYesNoAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    YesNoAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListYesNoAnswerQuestion_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListYesNoAnswerQuestion_Answers_checkListYesNoAnswerQuestion_CheckListYesNoAnswerQuestionId",
                        column: x => x.CheckListYesNoAnswerQuestionId,
                        principalTable: "checkListYesNoAnswerQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkListYesNoAnswerQuestion_Answers_userDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "userDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkListMultipleAnswerQuestion_QuestionTypeId",
                table: "checkListMultipleAnswerQuestion",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListMultipleAnswerQuestion_Answers_CheckListMultipleAnswerQuestionId",
                table: "checkListMultipleAnswerQuestion_Answers",
                column: "CheckListMultipleAnswerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListMultipleAnswerQuestion_Answers_UserDetailsId",
                table: "checkListMultipleAnswerQuestion_Answers",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListSingleAnswerQuestion_QuestionTypeId",
                table: "checkListSingleAnswerQuestion",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListSingleAnswerQuestion_Answers_CheckListSingleAnswerQuestionId",
                table: "checkListSingleAnswerQuestion_Answers",
                column: "CheckListSingleAnswerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListSingleAnswerQuestion_Answers_UserDetailsId",
                table: "checkListSingleAnswerQuestion_Answers",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListYesNoAnswerQuestion_QuestionTypeId",
                table: "checkListYesNoAnswerQuestion",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListYesNoAnswerQuestion_Answers_CheckListYesNoAnswerQuestionId",
                table: "checkListYesNoAnswerQuestion_Answers",
                column: "CheckListYesNoAnswerQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListYesNoAnswerQuestion_Answers_UserDetailsId",
                table: "checkListYesNoAnswerQuestion_Answers",
                column: "UserDetailsId");
        }
    }
}
