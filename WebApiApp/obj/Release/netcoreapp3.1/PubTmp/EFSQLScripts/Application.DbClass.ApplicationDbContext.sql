IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [bubbleDetails] (
        [Id] int NOT NULL IDENTITY,
        [BubbleName] nvarchar(max) NULL,
        [BubbleSize] nvarchar(max) NULL,
        [BubbleDescription] nvarchar(max) NULL,
        [BubbleZipCode] nvarchar(max) NULL,
        [BubbleType] int NOT NULL,
        [BubbleValidity] datetime2 NOT NULL,
        [IsOtherCountyMemberAllowed] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListDetails] (
        [Id] int NOT NULL IDENTITY,
        [CheckListTypeId] int NOT NULL,
        [CheckListTypeChildId] int NOT NULL,
        [ChecklistName] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListQuestionTypes] (
        [Id] int NOT NULL IDENTITY,
        [QuestionTypeTitle] nvarchar(max) NULL,
        [QuestionTypeDescription] nvarchar(max) NULL,
        [CheckListTypeId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListQuestionTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [counties] (
        [Id] int NOT NULL IDENTITY,
        [CountyName] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_counties] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [otpHistory] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Otp] nvarchar(max) NULL,
        [OtpTimeStamp] datetime2 NOT NULL,
        [OtpStatus] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_otpHistory] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [podDetails] (
        [Id] int NOT NULL IDENTITY,
        [PODName] nvarchar(max) NULL,
        [PODBubbleType] int NOT NULL,
        [PODSize] int NOT NULL,
        [PODDescription] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_podDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [userDetails] (
        [Id] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NULL,
        [PhoneNo] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        [ProfilePicUrl] nvarchar(max) NULL,
        [County] nvarchar(max) NULL,
        [Longitute] nvarchar(max) NULL,
        [Latitude] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_userDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [bubbleMeetDetails] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [MeetDescription] nvarchar(max) NULL,
        [MeetTiming] datetime2 NOT NULL,
        [MeetPlace] nvarchar(max) NULL,
        [ZipCode] nvarchar(max) NULL,
        [AllowChat] bit NOT NULL,
        [UserPermission] int NOT NULL,
        [BubbleId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleMeetDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_bubbleMeetDetails_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListMultipleAnswerQuestion] (
        [Id] int NOT NULL IDENTITY,
        [CheckListTypeId] int NOT NULL,
        [QuestionTitle] nvarchar(max) NULL,
        [QuestionDescription] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [QuestionTypeId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListMultipleAnswerQuestion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListMultipleAnswerQuestion_checkListQuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [checkListQuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListQuestionOption] (
        [Id] int NOT NULL IDENTITY,
        [AnswerOption] nvarchar(max) NULL,
        [QuestionId] int NOT NULL,
        [QuestionTypeId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListQuestionOption] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListQuestionOption_checkListQuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [checkListQuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListSingleAnswerQuestion] (
        [Id] int NOT NULL IDENTITY,
        [CheckListTypeId] int NOT NULL,
        [QuestionTitle] nvarchar(max) NULL,
        [QuestionDescription] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [QuestionTypeId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListSingleAnswerQuestion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListSingleAnswerQuestion_checkListQuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [checkListQuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListSubjectiveAnswerQuestion] (
        [Id] int NOT NULL IDENTITY,
        [CheckListTypeId] int NOT NULL,
        [QuestionTitle] nvarchar(max) NULL,
        [QuestionDescription] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [QuestionTypeId] int NOT NULL,
        [ChecklistId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListSubjectiveAnswerQuestion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListSubjectiveAnswerQuestion_checkListDetails_ChecklistId] FOREIGN KEY ([ChecklistId]) REFERENCES [checkListDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_checkListSubjectiveAnswerQuestion_checkListQuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [checkListQuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListYesNoAnswerQuestion] (
        [Id] int NOT NULL IDENTITY,
        [CheckListTypeId] int NOT NULL,
        [QuestionTitle] nvarchar(max) NULL,
        [QuestionDescription] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [QuestionTypeId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListYesNoAnswerQuestion] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListYesNoAnswerQuestion_checkListQuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [checkListQuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [podMembers] (
        [Id] int NOT NULL IDENTITY,
        [PODId] int NOT NULL,
        [BubbleId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_podMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_podMembers_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_podMembers_podDetails_PODId] FOREIGN KEY ([PODId]) REFERENCES [podDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [bubbleMembers] (
        [Id] int NOT NULL IDENTITY,
        [BubbleId] int NOT NULL,
        [UserId] int NOT NULL,
        [MemberBubbleType] int NOT NULL,
        [IsBubbleAdmin] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_bubbleMembers_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_bubbleMembers_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListMultipleAnswerQuestion_Answers] (
        [Id] int NOT NULL IDENTITY,
        [Answer] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [CheckListYesNoAnswerQuestionId] int NOT NULL,
        [UserDetailsId] int NOT NULL,
        [CheckListMultipleAnswerQuestionId] int NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListMultipleAnswerQuestion_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListMultipleAnswerQuestion_Answers_checkListMultipleAnswerQuestion_CheckListMultipleAnswerQuestionId] FOREIGN KEY ([CheckListMultipleAnswerQuestionId]) REFERENCES [checkListMultipleAnswerQuestion] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_checkListMultipleAnswerQuestion_Answers_userDetails_UserDetailsId] FOREIGN KEY ([UserDetailsId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListSingleAnswerQuestion_Answers] (
        [Id] int NOT NULL IDENTITY,
        [SingleAnswer] nvarchar(max) NULL,
        [SingleAnswerDescription] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [CheckListSingleAnswerQuestionId] int NOT NULL,
        [UserDetailsId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListSingleAnswerQuestion_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListSingleAnswerQuestion_Answers_checkListSingleAnswerQuestion_CheckListSingleAnswerQuestionId] FOREIGN KEY ([CheckListSingleAnswerQuestionId]) REFERENCES [checkListSingleAnswerQuestion] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_checkListSingleAnswerQuestion_Answers_userDetails_UserDetailsId] FOREIGN KEY ([UserDetailsId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [checkListYesNoAnswerQuestion_Answers] (
        [Id] int NOT NULL IDENTITY,
        [YesNoAnswer] bit NOT NULL,
        [CheckListTypeChildId] int NOT NULL,
        [CheckListYesNoAnswerQuestionId] int NOT NULL,
        [UserDetailsId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListYesNoAnswerQuestion_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListYesNoAnswerQuestion_Answers_checkListYesNoAnswerQuestion_CheckListYesNoAnswerQuestionId] FOREIGN KEY ([CheckListYesNoAnswerQuestionId]) REFERENCES [checkListYesNoAnswerQuestion] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_checkListYesNoAnswerQuestion_Answers_userDetails_UserDetailsId] FOREIGN KEY ([UserDetailsId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE TABLE [podBubbleMembers] (
        [Id] int NOT NULL IDENTITY,
        [PODId] int NOT NULL,
        [BubbleId] int NOT NULL,
        [BubbleMemberId] int NOT NULL,
        [PODBubbleMemberId] int NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_podBubbleMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_podBubbleMembers_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_podBubbleMembers_bubbleMembers_PODBubbleMemberId] FOREIGN KEY ([PODBubbleMemberId]) REFERENCES [bubbleMembers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_podBubbleMembers_podDetails_PODId] FOREIGN KEY ([PODId]) REFERENCES [podDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_bubbleMeetDetails_BubbleId] ON [bubbleMeetDetails] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_bubbleMembers_BubbleId] ON [bubbleMembers] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_bubbleMembers_UserId] ON [bubbleMembers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListMultipleAnswerQuestion_QuestionTypeId] ON [checkListMultipleAnswerQuestion] ([QuestionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListMultipleAnswerQuestion_Answers_CheckListMultipleAnswerQuestionId] ON [checkListMultipleAnswerQuestion_Answers] ([CheckListMultipleAnswerQuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListMultipleAnswerQuestion_Answers_UserDetailsId] ON [checkListMultipleAnswerQuestion_Answers] ([UserDetailsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListQuestionOption_QuestionTypeId] ON [checkListQuestionOption] ([QuestionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListSingleAnswerQuestion_QuestionTypeId] ON [checkListSingleAnswerQuestion] ([QuestionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListSingleAnswerQuestion_Answers_CheckListSingleAnswerQuestionId] ON [checkListSingleAnswerQuestion_Answers] ([CheckListSingleAnswerQuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListSingleAnswerQuestion_Answers_UserDetailsId] ON [checkListSingleAnswerQuestion_Answers] ([UserDetailsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListSubjectiveAnswerQuestion_ChecklistId] ON [checkListSubjectiveAnswerQuestion] ([ChecklistId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListSubjectiveAnswerQuestion_QuestionTypeId] ON [checkListSubjectiveAnswerQuestion] ([QuestionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListYesNoAnswerQuestion_QuestionTypeId] ON [checkListYesNoAnswerQuestion] ([QuestionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListYesNoAnswerQuestion_Answers_CheckListYesNoAnswerQuestionId] ON [checkListYesNoAnswerQuestion_Answers] ([CheckListYesNoAnswerQuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_checkListYesNoAnswerQuestion_Answers_UserDetailsId] ON [checkListYesNoAnswerQuestion_Answers] ([UserDetailsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_podBubbleMembers_BubbleId] ON [podBubbleMembers] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_podBubbleMembers_PODBubbleMemberId] ON [podBubbleMembers] ([PODBubbleMemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_podBubbleMembers_PODId] ON [podBubbleMembers] ([PODId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_podMembers_BubbleId] ON [podMembers] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    CREATE INDEX [IX_podMembers_PODId] ON [podMembers] ([PODId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402205316_RecreatedDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210402205316_RecreatedDb', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405143434_AddedIsOptionAllowedField')
BEGIN
    ALTER TABLE [checkListQuestionTypes] ADD [IsOptionsAllowed] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405143434_AddedIsOptionAllowedField')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405143434_AddedIsOptionAllowedField', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    ALTER TABLE [bubbleMeetDetails] DROP CONSTRAINT [FK_bubbleMeetDetails_bubbleDetails_BubbleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    DROP INDEX [IX_bubbleMeetDetails_BubbleId] ON [bubbleMeetDetails];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleMeetDetails]') AND [c].[name] = N'BubbleId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [bubbleMeetDetails] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [bubbleMeetDetails] DROP COLUMN [BubbleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    EXEC sp_rename N'[bubbleMeetDetails].[ZipCode]', N'County', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    EXEC sp_rename N'[bubbleMeetDetails].[AllowChat]', N'IsChatAllowed', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    ALTER TABLE [bubbleMeetDetails] ADD [MeetDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405210226_CreatedBubbleMeetMembersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405210226_CreatedBubbleMeetMembersTable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405212537_UserPermissionColumn')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleMeetDetails]') AND [c].[name] = N'UserPermission');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [bubbleMeetDetails] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [bubbleMeetDetails] DROP COLUMN [UserPermission];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405212537_UserPermissionColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405212537_UserPermissionColumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE TABLE [bubbleMeetMemberPermissions] (
        [Id] int NOT NULL IDENTITY,
        [BubbleMeetId] int NOT NULL,
        [UserId] int NOT NULL,
        [UserPermission] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleMeetMemberPermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_bubbleMeetMemberPermissions_bubbleMeetDetails_BubbleMeetId] FOREIGN KEY ([BubbleMeetId]) REFERENCES [bubbleMeetDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_bubbleMeetMemberPermissions_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE TABLE [bubbleMeetMembers] (
        [Id] int NOT NULL IDENTITY,
        [BubbleId] int NOT NULL,
        [BubbleMeetId] int NOT NULL,
        [UserId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleMeetMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_bubbleMeetMembers_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_bubbleMeetMembers_bubbleMeetDetails_BubbleMeetId] FOREIGN KEY ([BubbleMeetId]) REFERENCES [bubbleMeetDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_bubbleMeetMembers_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE INDEX [IX_bubbleMeetMemberPermissions_BubbleMeetId] ON [bubbleMeetMemberPermissions] ([BubbleMeetId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE INDEX [IX_bubbleMeetMemberPermissions_UserId] ON [bubbleMeetMemberPermissions] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE INDEX [IX_bubbleMeetMembers_BubbleId] ON [bubbleMeetMembers] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE INDEX [IX_bubbleMeetMembers_BubbleMeetId] ON [bubbleMeetMembers] ([BubbleMeetId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    CREATE INDEX [IX_bubbleMeetMembers_UserId] ON [bubbleMeetMembers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214120_AddedUserPermissionTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405214120_AddedUserPermissionTable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214916_AddedIsMeetMemberadmin')
BEGIN
    ALTER TABLE [bubbleMeetMembers] ADD [IsBubbleMeetAdmin] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405214916_AddedIsMeetMemberadmin')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405214916_AddedIsMeetMemberadmin', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406100711_RemoveredForeignKeyBubbleId')
BEGIN
    ALTER TABLE [bubbleMeetMembers] DROP CONSTRAINT [FK_bubbleMeetMembers_bubbleDetails_BubbleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406100711_RemoveredForeignKeyBubbleId')
BEGIN
    DROP INDEX [IX_bubbleMeetMembers_BubbleId] ON [bubbleMeetMembers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406100711_RemoveredForeignKeyBubbleId')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleMeetMembers]') AND [c].[name] = N'BubbleId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [bubbleMeetMembers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [bubbleMeetMembers] DROP COLUMN [BubbleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406100711_RemoveredForeignKeyBubbleId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406100711_RemoveredForeignKeyBubbleId', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406142537_UpdatedCountiesTableFields')
BEGIN
    ALTER TABLE [counties] ADD [Country] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406142537_UpdatedCountiesTableFields')
BEGIN
    ALTER TABLE [counties] ADD [Fips] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406142537_UpdatedCountiesTableFields')
BEGIN
    ALTER TABLE [counties] ADD [State] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406142537_UpdatedCountiesTableFields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406142537_UpdatedCountiesTableFields', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406145042_UpdatedCountiesTableFieldsChanges')
BEGIN
    EXEC sp_rename N'[counties].[Country]', N'Countryy', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406145042_UpdatedCountiesTableFieldsChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406145042_UpdatedCountiesTableFieldsChanges', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406145113_UpdatedCountiesTableFieldsChangesReverted')
BEGIN
    EXEC sp_rename N'[counties].[Countryy]', N'Country', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406145113_UpdatedCountiesTableFieldsChangesReverted')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406145113_UpdatedCountiesTableFieldsChangesReverted', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406150550_UpdatedCountiesTableFieldsChangedType')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[counties]') AND [c].[name] = N'Fips');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [counties] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [counties] ALTER COLUMN [Fips] int NOT NULL;
    ALTER TABLE [counties] ADD DEFAULT 0 FOR [Fips];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406150550_UpdatedCountiesTableFieldsChangedType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406150550_UpdatedCountiesTableFieldsChangedType', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406195900_AddedNewColumnInPermissionsTable')
BEGIN
    EXEC sp_rename N'[bubbleMeetMemberPermissions].[UserPermission]', N'UserPermissionTypeId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406195900_AddedNewColumnInPermissionsTable')
BEGIN
    ALTER TABLE [bubbleMeetMemberPermissions] ADD [UserPermissionStatus] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406195900_AddedNewColumnInPermissionsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406195900_AddedNewColumnInPermissionsTable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406220745_removedColumnFromBubbleMeetMembers')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleMeetMembers]') AND [c].[name] = N'IsBubbleMeetAdmin');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [bubbleMeetMembers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [bubbleMeetMembers] DROP COLUMN [IsBubbleMeetAdmin];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406220745_removedColumnFromBubbleMeetMembers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210406220745_removedColumnFromBubbleMeetMembers', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListMultipleAnswerQuestion_Answers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListSingleAnswerQuestion_Answers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListYesNoAnswerQuestion_Answers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListMultipleAnswerQuestion];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListSingleAnswerQuestion];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    DROP TABLE [checkListYesNoAnswerQuestion];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408192745_removedFewTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210408192745_removedFewTables', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408194710_AddedSubjectiveAsnwersTable')
BEGIN
    CREATE TABLE [checkListSubjectiveQuestion_Answers] (
        [Id] int NOT NULL IDENTITY,
        [SingleAnswer] nvarchar(max) NULL,
        [CheckListTypeChildId] int NOT NULL,
        [ChecklistId] int NOT NULL,
        [CheckListSubjectiveAnswerQuestionId] int NOT NULL,
        [UserDetailsId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_checkListSubjectiveQuestion_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListSubjectiveAnswerQuestionId] FOREIGN KEY ([CheckListSubjectiveAnswerQuestionId]) REFERENCES [checkListSubjectiveAnswerQuestion] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_userDetails_UserDetailsId] FOREIGN KEY ([UserDetailsId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408194710_AddedSubjectiveAsnwersTable')
BEGIN
    CREATE INDEX [IX_checkListSubjectiveQuestion_Answers_CheckListSubjectiveAnswerQuestionId] ON [checkListSubjectiveQuestion_Answers] ([CheckListSubjectiveAnswerQuestionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408194710_AddedSubjectiveAsnwersTable')
BEGIN
    CREATE INDEX [IX_checkListSubjectiveQuestion_Answers_UserDetailsId] ON [checkListSubjectiveQuestion_Answers] ([UserDetailsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408194710_AddedSubjectiveAsnwersTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210408194710_AddedSubjectiveAsnwersTable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    ALTER TABLE [checkListSubjectiveQuestion_Answers] DROP CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListSubjectiveAnswerQuestionId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    ALTER TABLE [checkListSubjectiveQuestion_Answers] DROP CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_userDetails_UserDetailsId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    EXEC sp_rename N'[checkListSubjectiveQuestion_Answers].[UserDetailsId]', N'UserId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    EXEC sp_rename N'[checkListSubjectiveQuestion_Answers].[CheckListSubjectiveAnswerQuestionId]', N'CheckListQuestionId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    EXEC sp_rename N'[checkListSubjectiveQuestion_Answers].[IX_checkListSubjectiveQuestion_Answers_UserDetailsId]', N'IX_checkListSubjectiveQuestion_Answers_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    EXEC sp_rename N'[checkListSubjectiveQuestion_Answers].[IX_checkListSubjectiveQuestion_Answers_CheckListSubjectiveAnswerQuestionId]', N'IX_checkListSubjectiveQuestion_Answers_CheckListQuestionId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    ALTER TABLE [checkListSubjectiveQuestion_Answers] ADD CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_checkListSubjectiveAnswerQuestion_CheckListQuestionId] FOREIGN KEY ([CheckListQuestionId]) REFERENCES [checkListSubjectiveAnswerQuestion] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    ALTER TABLE [checkListSubjectiveQuestion_Answers] ADD CONSTRAINT [FK_checkListSubjectiveQuestion_Answers_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408195337_ChangedCoulmnName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210408195337_ChangedCoulmnName', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210409093625_AddedNewColumnQuestionTypePrimeId')
BEGIN
    ALTER TABLE [checkListQuestionTypes] ADD [QuestionTypePirmeId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210409093625_AddedNewColumnQuestionTypePrimeId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210409093625_AddedNewColumnQuestionTypePrimeId', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210409110344_AddedNewColumnAnswerOptionId')
BEGIN
    ALTER TABLE [checkListSubjectiveQuestion_Answers] ADD [AnswerOptionId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210409110344_AddedNewColumnAnswerOptionId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210409110344_AddedNewColumnAnswerOptionId', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414194710_removedFK')
BEGIN
    ALTER TABLE [podBubbleMembers] DROP CONSTRAINT [FK_podBubbleMembers_bubbleMembers_PODBubbleMemberId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414194710_removedFK')
BEGIN
    DROP INDEX [IX_podBubbleMembers_PODBubbleMemberId] ON [podBubbleMembers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414194710_removedFK')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[podBubbleMembers]') AND [c].[name] = N'PODBubbleMemberId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [podBubbleMembers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [podBubbleMembers] DROP COLUMN [PODBubbleMemberId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414194710_removedFK')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210414194710_removedFK', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145605_AddedPodMeetTables')
BEGIN
    ALTER TABLE [bubbleMeetMemberPermissions] ADD [MeetTypeId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145605_AddedPodMeetTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210419145605_AddedPodMeetTables', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE TABLE [podMeetDetails] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [MeetDescription] nvarchar(max) NULL,
        [MeetTiming] datetime2 NOT NULL,
        [MeetDate] datetime2 NOT NULL,
        [MeetPlace] nvarchar(max) NULL,
        [County] nvarchar(max) NULL,
        [IsChatAllowed] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_podMeetDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE TABLE [podMeetMembers] (
        [Id] int NOT NULL IDENTITY,
        [PODMeetId] int NOT NULL,
        [BubbleId] int NOT NULL,
        [UserId] int NOT NULL,
        [PODId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_podMeetMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_podMeetMembers_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_podMeetMembers_podDetails_PODId] FOREIGN KEY ([PODId]) REFERENCES [podDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_podMeetMembers_podMeetDetails_PODMeetId] FOREIGN KEY ([PODMeetId]) REFERENCES [podMeetDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_podMeetMembers_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE INDEX [IX_podMeetMembers_BubbleId] ON [podMeetMembers] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE INDEX [IX_podMeetMembers_PODId] ON [podMeetMembers] ([PODId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE INDEX [IX_podMeetMembers_PODMeetId] ON [podMeetMembers] ([PODMeetId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    CREATE INDEX [IX_podMeetMembers_UserId] ON [podMeetMembers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419145959_AddedPodMeetTablesNew')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210419145959_AddedPodMeetTablesNew', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210420135747_removedFKConstraint')
BEGIN
    ALTER TABLE [bubbleMeetMemberPermissions] DROP CONSTRAINT [FK_bubbleMeetMemberPermissions_bubbleMeetDetails_BubbleMeetId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210420135747_removedFKConstraint')
BEGIN
    DROP INDEX [IX_bubbleMeetMemberPermissions_BubbleMeetId] ON [bubbleMeetMemberPermissions];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210420135747_removedFKConstraint')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210420135747_removedFKConstraint', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427112050_UserdeviceDetailsTable')
BEGIN
    CREATE TABLE [userDeviceDetails] (
        [Id] int NOT NULL IDENTITY,
        [DeviceToken] nvarchar(max) NULL,
        [DeviceTypeId] int NOT NULL,
        [UserId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_userDeviceDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_userDeviceDetails_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427112050_UserdeviceDetailsTable')
BEGIN
    CREATE INDEX [IX_userDeviceDetails_UserId] ON [userDeviceDetails] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427112050_UserdeviceDetailsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210427112050_UserdeviceDetailsTable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427150433_AddedNotificationTables')
BEGIN
    CREATE TABLE [notifications] (
        [Id] int NOT NULL IDENTITY,
        [NotificationTypeIdId] int NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_notifications_notifications_NotificationTypeIdId] FOREIGN KEY ([NotificationTypeIdId]) REFERENCES [notifications] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427150433_AddedNotificationTables')
BEGIN
    CREATE TABLE [notificationsHistory] (
        [Id] int NOT NULL IDENTITY,
        [NotificationId] int NOT NULL,
        [UserId] int NOT NULL,
        [PODBubbleChildId] int NOT NULL,
        [NotificationTypeChild] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_notificationsHistory] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427150433_AddedNotificationTables')
BEGIN
    CREATE INDEX [IX_notifications_NotificationTypeIdId] ON [notifications] ([NotificationTypeIdId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427150433_AddedNotificationTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210427150433_AddedNotificationTables', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427152644_changedcolumnname')
BEGIN
    ALTER TABLE [notifications] DROP CONSTRAINT [FK_notifications_notifications_NotificationTypeIdId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427152644_changedcolumnname')
BEGIN
    DROP INDEX [IX_notifications_NotificationTypeIdId] ON [notifications];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427152644_changedcolumnname')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[notifications]') AND [c].[name] = N'NotificationTypeIdId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [notifications] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [notifications] DROP COLUMN [NotificationTypeIdId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427152644_changedcolumnname')
BEGIN
    ALTER TABLE [notifications] ADD [NotificationTypeId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427152644_changedcolumnname')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210427152644_changedcolumnname', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    EXEC sp_rename N'[notificationsHistory].[PODBubbleChildId]', N'UserDeviceId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    ALTER TABLE [notificationsHistory] ADD [PODBubbleId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    ALTER TABLE [notifications] ADD [NotificationImage] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    CREATE INDEX [IX_notificationsHistory_NotificationId] ON [notificationsHistory] ([NotificationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    CREATE INDEX [IX_notificationsHistory_UserId] ON [notificationsHistory] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    ALTER TABLE [notificationsHistory] ADD CONSTRAINT [FK_notificationsHistory_notifications_NotificationId] FOREIGN KEY ([NotificationId]) REFERENCES [notifications] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    ALTER TABLE [notificationsHistory] ADD CONSTRAINT [FK_notificationsHistory_userDetails_UserId] FOREIGN KEY ([UserId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428104939_AddedNewColumnsInNotificationHistory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210428104939_AddedNewColumnsInNotificationHistory', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428124839_addedAnoutherColumn')
BEGIN
    ALTER TABLE [notificationsHistory] ADD [NotificationUserTitle] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210428124839_addedAnoutherColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210428124839_addedAnoutherColumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE TABLE [chatDetails] (
        [Id] int NOT NULL IDENTITY,
        [ChatTypeId] int NOT NULL,
        [ChatParentTypeId] int NOT NULL,
        [ChatParentId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_chatDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE TABLE [chatHistory] (
        [Id] int NOT NULL IDENTITY,
        [ChatId] int NOT NULL,
        [ChatMessageSenderId] int NOT NULL,
        [ChatMessage] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_chatHistory] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_chatHistory_chatDetails_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [chatDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_chatHistory_userDetails_ChatMessageSenderId] FOREIGN KEY ([ChatMessageSenderId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE TABLE [chatMembers] (
        [Id] int NOT NULL IDENTITY,
        [ChatId] int NOT NULL,
        [ChatMemberId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_chatMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_chatMembers_chatDetails_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [chatDetails] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_chatMembers_userDetails_ChatMemberId] FOREIGN KEY ([ChatMemberId]) REFERENCES [userDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE INDEX [IX_chatHistory_ChatId] ON [chatHistory] ([ChatId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE INDEX [IX_chatHistory_ChatMessageSenderId] ON [chatHistory] ([ChatMessageSenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE INDEX [IX_chatMembers_ChatId] ON [chatMembers] ([ChatId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    CREATE INDEX [IX_chatMembers_ChatMemberId] ON [chatMembers] ([ChatMemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430184401_AddedChatTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210430184401_AddedChatTables', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210503075309_addedStatusColumn')
BEGIN
    ALTER TABLE [chatMembers] ADD [ChatMemberStatus] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210503075309_addedStatusColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210503075309_addedStatusColumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210503081230_addedChatStatus')
BEGIN
    ALTER TABLE [chatDetails] ADD [ChatStatus] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210503081230_addedChatStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210503081230_addedChatStatus', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210504192936_AddedNewColumnInNotificationHistory')
BEGIN
    ALTER TABLE [notificationsHistory] ADD [NotificationCategoryId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210504192936_AddedNewColumnInNotificationHistory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210504192936_AddedNewColumnInNotificationHistory', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210506193113_AddedDescriptionColumnInNotificationHistory')
BEGIN
    ALTER TABLE [notificationsHistory] ADD [NotificationUserDescription] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210506193113_AddedDescriptionColumnInNotificationHistory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210506193113_AddedDescriptionColumnInNotificationHistory', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174503_addedTwoNewTablesForBubbleSafty')
BEGIN
    CREATE TABLE [bubbleSafetyDetails] (
        [Id] int NOT NULL IDENTITY,
        [BubbleSaftyTypeId] int NOT NULL,
        [BubbleSaftyValue] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleSafetyDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174503_addedTwoNewTablesForBubbleSafty')
BEGIN
    CREATE TABLE [bubbleSafetyWights] (
        [Id] int NOT NULL IDENTITY,
        [BubbleWightFiled] nvarchar(max) NULL,
        [BubbleWightValue] nvarchar(max) NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleSafetyWights] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174503_addedTwoNewTablesForBubbleSafty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210519174503_addedTwoNewTablesForBubbleSafty', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174855_AddedNewColumnInSafty')
BEGIN
    ALTER TABLE [bubbleSafetyDetails] ADD [BubbleId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174855_AddedNewColumnInSafty')
BEGIN
    CREATE INDEX [IX_bubbleSafetyDetails_BubbleId] ON [bubbleSafetyDetails] ([BubbleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174855_AddedNewColumnInSafty')
BEGIN
    ALTER TABLE [bubbleSafetyDetails] ADD CONSTRAINT [FK_bubbleSafetyDetails_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519174855_AddedNewColumnInSafty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210519174855_AddedNewColumnInSafty', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520083345_AddedNewTableBubbleSafetyCalculationFields')
BEGIN
    CREATE TABLE [bubbleSafetyWightsCalculationFields] (
        [Id] int NOT NULL IDENTITY,
        [Population] float NOT NULL,
        [TestPositivityRatio] float NOT NULL,
        [CaseDensity] float NOT NULL,
        [InfectionRate] float NOT NULL,
        [InfectionRateCI90] float NOT NULL,
        [ActualCases] float NOT NULL,
        [ActualDeaths] float NOT NULL,
        [ActualVaccineCompleted] float NOT NULL,
        [CreatedBy] int NOT NULL,
        [UpdatedBy] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [UpdatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_bubbleSafetyWightsCalculationFields] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520083345_AddedNewTableBubbleSafetyCalculationFields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520083345_AddedNewTableBubbleSafetyCalculationFields', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520084015_AddedNewColumnBubbleSafetyCalculationFields')
BEGIN
    ALTER TABLE [bubbleSafetyWightsCalculationFields] ADD [Fips] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520084015_AddedNewColumnBubbleSafetyCalculationFields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520084015_AddedNewColumnBubbleSafetyCalculationFields', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520084309_UpdatedNewColumnBubbleSafetyCalculationFields')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleSafetyWightsCalculationFields]') AND [c].[name] = N'Fips');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [bubbleSafetyWightsCalculationFields] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [bubbleSafetyWightsCalculationFields] ALTER COLUMN [Fips] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520084309_UpdatedNewColumnBubbleSafetyCalculationFields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520084309_UpdatedNewColumnBubbleSafetyCalculationFields', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520085322_AddedThreeNewColumnBubbleSafetyCalculationFields')
BEGIN
    ALTER TABLE [bubbleSafetyWightsCalculationFields] ADD [CasesToPopulationRatio] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520085322_AddedThreeNewColumnBubbleSafetyCalculationFields')
BEGIN
    ALTER TABLE [bubbleSafetyWightsCalculationFields] ADD [DeathToPopulationRatio] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520085322_AddedThreeNewColumnBubbleSafetyCalculationFields')
BEGIN
    ALTER TABLE [bubbleSafetyWightsCalculationFields] ADD [VaccineToPopulationRatio] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520085322_AddedThreeNewColumnBubbleSafetyCalculationFields')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520085322_AddedThreeNewColumnBubbleSafetyCalculationFields', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520115927_ChangedColumnTypeBubbleSafetyWights')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleSafetyWights]') AND [c].[name] = N'BubbleWightFiled');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [bubbleSafetyWights] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [bubbleSafetyWights] DROP COLUMN [BubbleWightFiled];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520115927_ChangedColumnTypeBubbleSafetyWights')
BEGIN
    ALTER TABLE [bubbleSafetyWights] ADD [BubbleWightFiledTypeId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520115927_ChangedColumnTypeBubbleSafetyWights')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520115927_ChangedColumnTypeBubbleSafetyWights', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520163953_changedDatatype')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleSafetyWights]') AND [c].[name] = N'BubbleWightValue');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [bubbleSafetyWights] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [bubbleSafetyWights] ALTER COLUMN [BubbleWightValue] float NOT NULL;
    ALTER TABLE [bubbleSafetyWights] ADD DEFAULT 0.0E0 FOR [BubbleWightValue];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520163953_changedDatatype')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520163953_changedDatatype', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520184450_changedDatatypeofCountyCloumn')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[userDetails]') AND [c].[name] = N'County');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [userDetails] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [userDetails] ALTER COLUMN [County] int NOT NULL;
    ALTER TABLE [userDetails] ADD DEFAULT 0 FOR [County];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520184450_changedDatatypeofCountyCloumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520184450_changedDatatypeofCountyCloumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520195109_changedDatatypeofCountyCloumnBubbleMeet')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleMeetDetails]') AND [c].[name] = N'County');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [bubbleMeetDetails] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [bubbleMeetDetails] ALTER COLUMN [County] int NOT NULL;
    ALTER TABLE [bubbleMeetDetails] ADD DEFAULT 0 FOR [County];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520195109_changedDatatypeofCountyCloumnBubbleMeet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520195109_changedDatatypeofCountyCloumnBubbleMeet', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    ALTER TABLE [bubbleSafetyDetails] DROP CONSTRAINT [FK_bubbleSafetyDetails_bubbleDetails_BubbleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleSafetyDetails]') AND [c].[name] = N'BubbleSaftyValue');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [bubbleSafetyDetails] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [bubbleSafetyDetails] ALTER COLUMN [BubbleSaftyValue] float NOT NULL;
    ALTER TABLE [bubbleSafetyDetails] ADD DEFAULT 0.0E0 FOR [BubbleSaftyValue];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[bubbleSafetyDetails]') AND [c].[name] = N'BubbleId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [bubbleSafetyDetails] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [bubbleSafetyDetails] ALTER COLUMN [BubbleId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    ALTER TABLE [bubbleSafetyDetails] ADD [BubblePODId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    ALTER TABLE [bubbleSafetyDetails] ADD CONSTRAINT [FK_bubbleSafetyDetails_bubbleDetails_BubbleId] FOREIGN KEY ([BubbleId]) REFERENCES [bubbleDetails] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603173813_addedNewColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210603173813_addedNewColumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603202909_ChangedCountyStringToInt')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[podMeetDetails]') AND [c].[name] = N'County');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [podMeetDetails] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [podMeetDetails] ALTER COLUMN [County] int NOT NULL;
    ALTER TABLE [podMeetDetails] ADD DEFAULT 0 FOR [County];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603202909_ChangedCountyStringToInt')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210603202909_ChangedCountyStringToInt', N'5.0.3');
END;
GO

COMMIT;
GO

