﻿// <auto-generated />
using System;
using Application.DbClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Presistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210406220745_removedColumnFromBubbleMeetMembers")]
    partial class removedColumnFromBubbleMeetMembers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.BubbleDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BubbleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BubbleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BubbleSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BubbleType")
                        .HasColumnType("int");

                    b.Property<DateTime>("BubbleValidity")
                        .HasColumnType("datetime2");

                    b.Property<string>("BubbleZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOtherCountyMemberAllowed")
                        .HasColumnType("bit");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("bubbleDetails");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMeetDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsChatAllowed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MeetDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MeetDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MeetTiming")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("bubbleMeetDetails");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMeetMemberPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BubbleMeetId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("UserPermissionStatus")
                        .HasColumnType("bit");

                    b.Property<int>("UserPermissionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BubbleMeetId");

                    b.HasIndex("UserId");

                    b.ToTable("bubbleMeetMemberPermissions");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMeetMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BubbleMeetId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BubbleMeetId");

                    b.HasIndex("UserId");

                    b.ToTable("bubbleMeetMembers");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BubbleId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBubbleAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("MemberBubbleType")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BubbleId");

                    b.HasIndex("UserId");

                    b.ToTable("bubbleMembers");
                });

            modelBuilder.Entity("Domain.Entities.CheckListDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ChecklistName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("checkListDetails");
                });

            modelBuilder.Entity("Domain.Entities.CheckListMultipleAnswerQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("checkListMultipleAnswerQuestion");
                });

            modelBuilder.Entity("Domain.Entities.CheckListMultipleAnswerQuestion_Answers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CheckListMultipleAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListYesNoAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CheckListMultipleAnswerQuestionId");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("checkListMultipleAnswerQuestion_Answers");
                });

            modelBuilder.Entity("Domain.Entities.CheckListQuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerOption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("checkListQuestionOption");
                });

            modelBuilder.Entity("Domain.Entities.CheckListQuestionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOptionsAllowed")
                        .HasColumnType("bit");

                    b.Property<string>("QuestionTypeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTypeTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("checkListQuestionTypes");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSingleAnswerQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("checkListSingleAnswerQuestion");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSingleAnswerQuestion_Answers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListSingleAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("SingleAnswer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SingleAnswerDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CheckListSingleAnswerQuestionId");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("checkListSingleAnswerQuestion_Answers");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSubjectiveAnswerQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ChecklistId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("checkListSubjectiveAnswerQuestion");
                });

            modelBuilder.Entity("Domain.Entities.CheckListYesNoAnswerQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.ToTable("checkListYesNoAnswerQuestion");
                });

            modelBuilder.Entity("Domain.Entities.CheckListYesNoAnswerQuestion_Answers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckListTypeChildId")
                        .HasColumnType("int");

                    b.Property<int>("CheckListYesNoAnswerQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserDetailsId")
                        .HasColumnType("int");

                    b.Property<bool>("YesNoAnswer")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CheckListYesNoAnswerQuestionId");

                    b.HasIndex("UserDetailsId");

                    b.ToTable("checkListYesNoAnswerQuestion_Answers");
                });

            modelBuilder.Entity("Domain.Entities.Counties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Fips")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("counties");
                });

            modelBuilder.Entity("Domain.Entities.OtpHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Otp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OtpStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OtpTimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("otpHistory");
                });

            modelBuilder.Entity("Domain.Entities.PODBubbleMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BubbleId")
                        .HasColumnType("int");

                    b.Property<int>("BubbleMemberId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PODBubbleMemberId")
                        .HasColumnType("int");

                    b.Property<int>("PODId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BubbleId");

                    b.HasIndex("PODBubbleMemberId");

                    b.HasIndex("PODId");

                    b.ToTable("podBubbleMembers");
                });

            modelBuilder.Entity("Domain.Entities.PODMembers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BubbleId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PODId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BubbleId");

                    b.HasIndex("PODId");

                    b.ToTable("podMembers");
                });

            modelBuilder.Entity("Domain.Entities.PodDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PODBubbleType")
                        .HasColumnType("int");

                    b.Property<string>("PODDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PODName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PODSize")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("podDetails");
                });

            modelBuilder.Entity("Domain.Entities.UserDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitute")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("userDetails");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMeetMemberPermissions", b =>
                {
                    b.HasOne("Domain.Entities.BubbleMeetDetails", "BubbleMeetDetails")
                        .WithMany()
                        .HasForeignKey("BubbleMeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BubbleMeetDetails");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMeetMembers", b =>
                {
                    b.HasOne("Domain.Entities.BubbleMeetDetails", "BubbleMeetDetails")
                        .WithMany()
                        .HasForeignKey("BubbleMeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BubbleMeetDetails");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.BubbleMembers", b =>
                {
                    b.HasOne("Domain.Entities.BubbleDetails", "BubbleDetails")
                        .WithMany()
                        .HasForeignKey("BubbleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BubbleDetails");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.CheckListMultipleAnswerQuestion", b =>
                {
                    b.HasOne("Domain.Entities.CheckListQuestionType", "CheckListQuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListQuestionType");
                });

            modelBuilder.Entity("Domain.Entities.CheckListMultipleAnswerQuestion_Answers", b =>
                {
                    b.HasOne("Domain.Entities.CheckListMultipleAnswerQuestion", "CheckListMultipleAnswerQuestion")
                        .WithMany()
                        .HasForeignKey("CheckListMultipleAnswerQuestionId");

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListMultipleAnswerQuestion");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.CheckListQuestionOption", b =>
                {
                    b.HasOne("Domain.Entities.CheckListQuestionType", "CheckListQuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListQuestionType");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSingleAnswerQuestion", b =>
                {
                    b.HasOne("Domain.Entities.CheckListQuestionType", "CheckListQuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListQuestionType");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSingleAnswerQuestion_Answers", b =>
                {
                    b.HasOne("Domain.Entities.CheckListSingleAnswerQuestion", "CheckListSingleAnswerQuestion")
                        .WithMany()
                        .HasForeignKey("CheckListSingleAnswerQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListSingleAnswerQuestion");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.CheckListSubjectiveAnswerQuestion", b =>
                {
                    b.HasOne("Domain.Entities.CheckListDetails", "CheckListDetails")
                        .WithMany()
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.CheckListQuestionType", "CheckListQuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListDetails");

                    b.Navigation("CheckListQuestionType");
                });

            modelBuilder.Entity("Domain.Entities.CheckListYesNoAnswerQuestion", b =>
                {
                    b.HasOne("Domain.Entities.CheckListQuestionType", "CheckListQuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListQuestionType");
                });

            modelBuilder.Entity("Domain.Entities.CheckListYesNoAnswerQuestion_Answers", b =>
                {
                    b.HasOne("Domain.Entities.CheckListYesNoAnswerQuestion", "CheckListYesNoAnswerQuestion")
                        .WithMany()
                        .HasForeignKey("CheckListYesNoAnswerQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserDetails", "UserDetails")
                        .WithMany()
                        .HasForeignKey("UserDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckListYesNoAnswerQuestion");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Domain.Entities.PODBubbleMembers", b =>
                {
                    b.HasOne("Domain.Entities.BubbleDetails", "BubbleDetails")
                        .WithMany()
                        .HasForeignKey("BubbleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.BubbleMembers", "BubbleMembers")
                        .WithMany()
                        .HasForeignKey("PODBubbleMemberId");

                    b.HasOne("Domain.Entities.PodDetails", "PODDetails")
                        .WithMany()
                        .HasForeignKey("PODId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BubbleDetails");

                    b.Navigation("BubbleMembers");

                    b.Navigation("PODDetails");
                });

            modelBuilder.Entity("Domain.Entities.PODMembers", b =>
                {
                    b.HasOne("Domain.Entities.BubbleDetails", "BubbleDetails")
                        .WithMany()
                        .HasForeignKey("BubbleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PodDetails", "PODDetails")
                        .WithMany()
                        .HasForeignKey("PODId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BubbleDetails");

                    b.Navigation("PODDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
