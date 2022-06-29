using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presistence.Migrations
{
    public partial class RecreatedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bubbleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BubbleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BubbleSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BubbleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BubbleZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BubbleType = table.Column<int>(type: "int", nullable: false),
                    BubbleValidity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOtherCountyMemberAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "checkListDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    ChecklistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "checkListQuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListQuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "counties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "otpHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtpTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtpStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otpHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "podDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PODName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PODBubbleType = table.Column<int>(type: "int", nullable: false),
                    PODSize = table.Column<int>(type: "int", nullable: false),
                    PODDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bubbleMeetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetTiming = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowChat = table.Column<bool>(type: "bit", nullable: false),
                    UserPermission = table.Column<int>(type: "int", nullable: false),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleMeetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bubbleMeetDetails_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkListMultipleAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "checkListQuestionOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListQuestionOption_checkListQuestionTypes_QuestionTypeId",
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
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "checkListSubjectiveAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkListSubjectiveAnswerQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkListSubjectiveAnswerQuestion_checkListDetails_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "checkListDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_checkListSubjectiveAnswerQuestion_checkListQuestionTypes_QuestionTypeId",
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
                    CheckListTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "podMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PODId = table.Column<int>(type: "int", nullable: false),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podMembers_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podMembers_podDetails_PODId",
                        column: x => x.PODId,
                        principalTable: "podDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bubbleMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MemberBubbleType = table.Column<int>(type: "int", nullable: false),
                    IsBubbleAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bubbleMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bubbleMembers_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bubbleMembers_userDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "userDetails",
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
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListYesNoAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    CheckListMultipleAnswerQuestionId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    SingleAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleAnswerDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListSingleAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    YesNoAnswer = table.Column<bool>(type: "bit", nullable: false),
                    CheckListTypeChildId = table.Column<int>(type: "int", nullable: false),
                    CheckListYesNoAnswerQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "podBubbleMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PODId = table.Column<int>(type: "int", nullable: false),
                    BubbleId = table.Column<int>(type: "int", nullable: false),
                    BubbleMemberId = table.Column<int>(type: "int", nullable: false),
                    PODBubbleMemberId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_podBubbleMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_podBubbleMembers_bubbleDetails_BubbleId",
                        column: x => x.BubbleId,
                        principalTable: "bubbleDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_podBubbleMembers_bubbleMembers_PODBubbleMemberId",
                        column: x => x.PODBubbleMemberId,
                        principalTable: "bubbleMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_podBubbleMembers_podDetails_PODId",
                        column: x => x.PODId,
                        principalTable: "podDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMeetDetails_BubbleId",
                table: "bubbleMeetDetails",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMembers_BubbleId",
                table: "bubbleMembers",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_bubbleMembers_UserId",
                table: "bubbleMembers",
                column: "UserId");

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
                name: "IX_checkListQuestionOption_QuestionTypeId",
                table: "checkListQuestionOption",
                column: "QuestionTypeId");

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
                name: "IX_checkListSubjectiveAnswerQuestion_ChecklistId",
                table: "checkListSubjectiveAnswerQuestion",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_checkListSubjectiveAnswerQuestion_QuestionTypeId",
                table: "checkListSubjectiveAnswerQuestion",
                column: "QuestionTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_podBubbleMembers_BubbleId",
                table: "podBubbleMembers",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_podBubbleMembers_PODBubbleMemberId",
                table: "podBubbleMembers",
                column: "PODBubbleMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_podBubbleMembers_PODId",
                table: "podBubbleMembers",
                column: "PODId");

            migrationBuilder.CreateIndex(
                name: "IX_podMembers_BubbleId",
                table: "podMembers",
                column: "BubbleId");

            migrationBuilder.CreateIndex(
                name: "IX_podMembers_PODId",
                table: "podMembers",
                column: "PODId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bubbleMeetDetails");

            migrationBuilder.DropTable(
                name: "checkListMultipleAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "checkListQuestionOption");

            migrationBuilder.DropTable(
                name: "checkListSingleAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "checkListSubjectiveAnswerQuestion");

            migrationBuilder.DropTable(
                name: "checkListYesNoAnswerQuestion_Answers");

            migrationBuilder.DropTable(
                name: "counties");

            migrationBuilder.DropTable(
                name: "otpHistory");

            migrationBuilder.DropTable(
                name: "podBubbleMembers");

            migrationBuilder.DropTable(
                name: "podMembers");

            migrationBuilder.DropTable(
                name: "checkListMultipleAnswerQuestion");

            migrationBuilder.DropTable(
                name: "checkListSingleAnswerQuestion");

            migrationBuilder.DropTable(
                name: "checkListDetails");

            migrationBuilder.DropTable(
                name: "checkListYesNoAnswerQuestion");

            migrationBuilder.DropTable(
                name: "bubbleMembers");

            migrationBuilder.DropTable(
                name: "podDetails");

            migrationBuilder.DropTable(
                name: "checkListQuestionTypes");

            migrationBuilder.DropTable(
                name: "bubbleDetails");

            migrationBuilder.DropTable(
                name: "userDetails");
        }
    }
}
