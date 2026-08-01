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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] uniqueidentifier NOT NULL,
        [Age] int NULL,
        [ImageUrl] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] uniqueidentifier NOT NULL,
        [CourseId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ParentId] uniqueidentifier NULL,
        [RootCommentId] uniqueidentifier NOT NULL,
        [Path] nvarchar(max) NOT NULL,
        [Level] int NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [ExamCategories] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_ExamCategories] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [FlashCards] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_FlashCards] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [InboxState] (
        [Id] bigint NOT NULL IDENTITY,
        [MessageId] uniqueidentifier NOT NULL,
        [ConsumerId] uniqueidentifier NOT NULL,
        [LockId] uniqueidentifier NOT NULL,
        [RowVersion] rowversion NULL,
        [Received] datetime2 NOT NULL,
        [ReceiveCount] int NOT NULL,
        [ExpirationTime] datetime2 NULL,
        [Consumed] datetime2 NULL,
        [Delivered] datetime2 NULL,
        [LastSequenceNumber] bigint NULL,
        CONSTRAINT [PK_InboxState] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_InboxState_MessageId_ConsumerId] UNIQUE ([MessageId], [ConsumerId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [OutboxState] (
        [OutboxId] uniqueidentifier NOT NULL,
        [LockId] uniqueidentifier NOT NULL,
        [RowVersion] rowversion NULL,
        [Created] datetime2 NOT NULL,
        [Delivered] datetime2 NULL,
        [LastSequenceNumber] bigint NULL,
        CONSTRAINT [PK_OutboxState] PRIMARY KEY ([OutboxId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [QuestionGroup] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NULL,
        [AudioUrl] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_QuestionGroup] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Topics] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Topics] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Exams] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [DurationInMinutes] int NOT NULL,
        [ExamCategoryId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Exams] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Exams_ExamCategories_ExamCategoryId] FOREIGN KEY ([ExamCategoryId]) REFERENCES [ExamCategories] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Words] (
        [Id] uniqueidentifier NOT NULL,
        [Text] nvarchar(max) NOT NULL,
        [Meaning] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsMemorized] bit NOT NULL,
        [FlashCardId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Words] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Words_FlashCards_FlashCardId] FOREIGN KEY ([FlashCardId]) REFERENCES [FlashCards] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [OutboxMessage] (
        [SequenceNumber] bigint NOT NULL IDENTITY,
        [EnqueueTime] datetime2 NULL,
        [SentTime] datetime2 NOT NULL,
        [Headers] nvarchar(max) NULL,
        [Properties] nvarchar(max) NULL,
        [InboxMessageId] uniqueidentifier NULL,
        [InboxConsumerId] uniqueidentifier NULL,
        [OutboxId] uniqueidentifier NULL,
        [MessageId] uniqueidentifier NOT NULL,
        [ContentType] nvarchar(256) NOT NULL,
        [MessageType] nvarchar(max) NOT NULL,
        [Body] nvarchar(max) NOT NULL,
        [ConversationId] uniqueidentifier NULL,
        [CorrelationId] uniqueidentifier NULL,
        [InitiatorId] uniqueidentifier NULL,
        [RequestId] uniqueidentifier NULL,
        [SourceAddress] nvarchar(256) NULL,
        [DestinationAddress] nvarchar(256) NULL,
        [ResponseAddress] nvarchar(256) NULL,
        [FaultAddress] nvarchar(256) NULL,
        [ExpirationTime] datetime2 NULL,
        CONSTRAINT [PK_OutboxMessage] PRIMARY KEY ([SequenceNumber]),
        CONSTRAINT [FK_OutboxMessage_InboxState_InboxMessageId_InboxConsumerId] FOREIGN KEY ([InboxMessageId], [InboxConsumerId]) REFERENCES [InboxState] ([MessageId], [ConsumerId]),
        CONSTRAINT [FK_OutboxMessage_OutboxState_OutboxId] FOREIGN KEY ([OutboxId]) REFERENCES [OutboxState] ([OutboxId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Courses] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Courses_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Practices] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Practices] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Practices_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Questions] (
        [Id] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [QuestionTypes] int NOT NULL,
        [Explanation] nvarchar(max) NULL,
        [ImageUrl] nvarchar(max) NULL,
        [AudioUrl] nvarchar(max) NULL,
        [TopicId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        [QuestionGroupId] uniqueidentifier NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Questions_QuestionGroup_QuestionGroupId] FOREIGN KEY ([QuestionGroupId]) REFERENCES [QuestionGroup] ([Id]),
        CONSTRAINT [FK_Questions_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [ExamResults] (
        [Id] uniqueidentifier NOT NULL,
        [CompleteAt] datetime2 NOT NULL,
        [ExamId] uniqueidentifier NOT NULL,
        [Score] float NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ExamResults] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ExamResults_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ExamResults_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [Answers] (
        [Id] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [IsCorrect] bit NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [ExamDetails] (
        [ExamId] uniqueidentifier NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        [Score] float NOT NULL,
        CONSTRAINT [PK_ExamDetails] PRIMARY KEY ([ExamId], [QuestionId]),
        CONSTRAINT [FK_ExamDetails_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ExamDetails_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [PracticeDetails] (
        [PracticeId] uniqueidentifier NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PracticeDetails] PRIMARY KEY ([PracticeId], [QuestionId]),
        CONSTRAINT [FK_PracticeDetails_Practices_PracticeId] FOREIGN KEY ([PracticeId]) REFERENCES [Practices] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PracticeDetails_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE TABLE [AnswersHistories] (
        [ExamResultId] uniqueidentifier NOT NULL,
        [QuestionId] uniqueidentifier NOT NULL,
        [Id] uniqueidentifier NOT NULL,
        [UserAnswer] nvarchar(max) NOT NULL,
        [IsCorrect] bit NOT NULL,
        [Score] float NOT NULL,
        CONSTRAINT [PK_AnswersHistories] PRIMARY KEY ([ExamResultId], [QuestionId]),
        CONSTRAINT [FK_AnswersHistories_ExamResults_ExamResultId] FOREIGN KEY ([ExamResultId]) REFERENCES [ExamResults] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AnswersHistories_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (''05f2400b-5471-466a-8b7e-27752367e4d6'', NULL, N''User'', N''USER''),
    (''10f2400b-5471-466a-8b7e-27752367e4d6'', NULL, N''Admin'', N''ADMIN'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Age', N'ConcurrencyStamp', N'CreatedAt', N'Email', N'EmailConfirmed', N'ImageUrl', N'IsActive', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UpdatedAt', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [Age], [ConcurrencyStamp], [CreatedAt], [Email], [EmailConfirmed], [ImageUrl], [IsActive], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UpdatedAt], [UserName])
    VALUES (''8d581a98-361e-4333-a651-74e88ef572a4'', 0, NULL, N''f67e2437-61a2-4458-ac14-de7ab48158b6'', ''0001-01-01T00:00:00.0000000'', N''user@gmail.com'', CAST(1 AS bit), NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, N''USER@GMAIL.COM'', N''USER'', N''AQAAAAIAAYagAAAAEN8TWXW9pNZ+VVyeftOLixsSfyDOtPTZpv84QtbFESyzd6kZ0i70eIPvnvNBKX0Q9Q=='', NULL, CAST(0 AS bit), N''DF7GIIY7UNBVCVLZD73QO6PGSVQXBSTW'', CAST(0 AS bit), ''0001-01-01T00:00:00.0000000'', N''user''),
    (''9ae1058d-b602-4025-ab1d-74e7bced8f3b'', 0, NULL, N''6e66d8c1-89da-46df-bc24-ec54c7e7e7cf'', ''0001-01-01T00:00:00.0000000'', N''admin@gmail.com'', CAST(1 AS bit), NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, N''ADMIN@GMAIL.COM'', N''ADMIN'', N''AQAAAAIAAYagAAAAEFY87mzNg88TIJtuXRcRIeT0MXYto4NkcukxwFGpl+p5IHBJVqlPbyFx9UJIOmu7eA=='', NULL, CAST(0 AS bit), N''3XVVZIW5RPRWT7MKN3Y6VRNTHXY2JGK5'', CAST(0 AS bit), ''0001-01-01T00:00:00.0000000'', N''admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'Age', N'ConcurrencyStamp', N'CreatedAt', N'Email', N'EmailConfirmed', N'ImageUrl', N'IsActive', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UpdatedAt', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'ImageUrl', N'IsActive', N'IsDeleted', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ExamCategories]'))
        SET IDENTITY_INSERT [ExamCategories] ON;
    EXEC(N'INSERT INTO [ExamCategories] ([Id], [CreatedAt], [Description], [ImageUrl], [IsActive], [IsDeleted], [Name], [UpdatedAt])
    VALUES (''11111111-1111-1111-1111-111111111111'', ''2026-01-01T00:00:00.0000000'', N''Grammar examination category'', N''images/category_img.jpg'', CAST(1 AS bit), CAST(0 AS bit), N''Grammar'', ''0001-01-01T00:00:00.0000000''),
    (''2af67565-75f7-4511-9b67-3762e917c173'', ''2026-01-01T00:00:00.0000000'', N''Vocabulary exam'', N''images/category_img.jpg'', CAST(1 AS bit), CAST(0 AS bit), N''Vocabulary'', ''0001-01-01T00:00:00.0000000''),
    (''48b31fd9-e2a2-4b6a-9884-e2b6c664715b'', ''2026-01-01T00:00:00.0000000'', N''Listening exam'', N''images/category_img.jpg'', CAST(1 AS bit), CAST(0 AS bit), N''Listening'', ''0001-01-01T00:00:00.0000000''),
    (''c5f9dd20-276f-4a4a-bbb1-26b795a8514c'', ''2026-01-01T00:00:00.0000000'', N''Reading'', N''images/category_img.jpg'', CAST(1 AS bit), CAST(0 AS bit), N''Reading'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'ImageUrl', N'IsActive', N'IsDeleted', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[ExamCategories]'))
        SET IDENTITY_INSERT [ExamCategories] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'IsDeleted', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Topics]'))
        SET IDENTITY_INSERT [Topics] ON;
    EXEC(N'INSERT INTO [Topics] ([Id], [CreatedAt], [Description], [IsActive], [IsDeleted], [Name], [UpdatedAt])
    VALUES (''22222222-2222-2222-2222-222222222222'', ''2026-01-01T00:00:00.0000000'', N''12 thì cơ bản trong tiếng anh.'', CAST(1 AS bit), CAST(0 AS bit), N''12 Thì Trong Tiếng Anh'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'IsActive', N'IsDeleted', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Topics]'))
        SET IDENTITY_INSERT [Topics] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (''05f2400b-5471-466a-8b7e-27752367e4d6'', ''8d581a98-361e-4333-a651-74e88ef572a4''),
    (''10f2400b-5471-466a-8b7e-27752367e4d6'', ''9ae1058d-b602-4025-ab1d-74e7bced8f3b'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedAt', N'Description', N'ImageUrl', N'IsActive', N'IsDeleted', N'Name', N'TopicId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Courses]'))
        SET IDENTITY_INSERT [Courses] ON;
    EXEC(N'INSERT INTO [Courses] ([Id], [Content], [CreatedAt], [Description], [ImageUrl], [IsActive], [IsDeleted], [Name], [TopicId], [UpdatedAt])
    VALUES (''2b82c46a-209b-4c86-b917-9ee78a51efeb'', CONCAT(CAST(N''<h1>12 Th&igrave; Trong Tiếng Anh</h1>'' AS nvarchar(max)), nchar(13), nchar(10), N''<h2>Giới thiệu</h2>'', nchar(13), nchar(10), N''<p>Th&igrave; (Tense) l&agrave; một trong những phần ngữ ph&aacute;p quan trọng nhất trong tiếng Anh. Việc sử dụng đ&uacute;ng th&igrave; gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian, trạng th&aacute;i v&agrave; qu&aacute; tr&igrave;nh của h&agrave;nh động. Hệ thống ngữ ph&aacute;p tiếng Anh bao gồm 12 th&igrave; cơ bản, được chia th&agrave;nh ba mốc thời gian ch&iacute;nh: hiện tại, qu&aacute; khứ v&agrave; tương lai. Mỗi mốc thời gian lại c&oacute; bốn dạng: đơn, tiếp diễn, ho&agrave;n th&agrave;nh v&agrave; ho&agrave;n th&agrave;nh tiếp diễn.</p>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h1>I. C&aacute;c th&igrave; hiện tại</h1>'', nchar(13), nchar(10), N''<h2>1. Hiện tại đơn (Simple Present)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Khẳng định: S + V(s/es)</p></li>'', nchar(13), nchar(10), N''<li><p>Phủ định: S + do/does not + V</p></li>'', nchar(13), nchar(10), N''<li><p>Nghi vấn: Do/Does + S + V?</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả th&oacute;i quen, sở th&iacute;ch.</p></li>'', nchar(13), nchar(10), N''<li><p>Diễn tả sự thật hiển nhi&ecirc;n.</p></li>'', nchar(13), nchar(10), N''<li><p>Diễn tả lịch tr&igrave;nh, thời gian biểu.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I go to school every day.</p></li>'', nchar(13), nchar(10), N''<li><p>The sun rises in the east.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>2. Hiện tại tiếp diễn (Present Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + am/is/are + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động đang diễn ra tại thời điểm n&oacute;i.</p></li>'', nchar(13), nchar(10), N''<li><p>Diễn tả kế hoạch trong tương lai gần.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>She is studying English now.</p></li>'', nchar(13), nchar(10), N''<li><p>We are meeting our teacher tomorrow.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>3. Hiện tại ho&agrave;n th&agrave;nh (Present Perfect)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + have/has + V3/ed</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động xảy ra trong qu&aacute; khứ nhưng c&ograve;n li&ecirc;n quan đến hiện tại.</p></li>'', nchar(13), nchar(10), N''<li><p>Diễn tả kinh nghiệm hoặc trải nghiệm.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I have finished my homework.</p></li>'', nchar(13), nchar(10), N''<li><p>She has visited Japan twice.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>4. Hiện tại ho&agrave;n th&agrave;nh tiếp diễn (Present Perfect Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + have/has been + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động bắt đầu trong qu&aacute; khứ v&agrave; vẫn tiếp tục đến hiện tại.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>They have been learning English for three years.</p></li>'', nchar(13), nchar(10), N''<li><p>I have been waiting for an hour.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h1>II. C&aacute;c th&igrave; qu&aacute; khứ</h1>'', nchar(13), nchar(10), N''<h2>5. Qu&aacute; khứ đơn (Simple Past)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + V2/ed</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động đ&atilde; xảy ra v&agrave; kết th&uacute;c trong qu&aacute; khứ.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I visited my grandparents last weekend.</p></li>'', nchar(13), nchar(10), N''<li><p>She bought a new laptop yesterday.</p></li>'', CONCAT(CAST(nchar(13) AS nvarchar(max)), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>6. Qu&aacute; khứ tiếp diễn (Past Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + was/were + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động đang diễn ra tại một thời điểm trong qu&aacute; khứ.</p></li>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động bị h&agrave;nh động kh&aacute;c xen v&agrave;o.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I was reading when he called.</p></li>'', nchar(13), nchar(10), N''<li><p>They were playing football at 5 p.m.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>7. Qu&aacute; khứ ho&agrave;n th&agrave;nh (Past Perfect)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + had + V3/ed</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động xảy ra trước một h&agrave;nh động kh&aacute;c trong qu&aacute; khứ.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>She had left before I arrived.</p></li>'', nchar(13), nchar(10), N''<li><p>They had finished dinner when we came.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>8. Qu&aacute; khứ ho&agrave;n th&agrave;nh tiếp diễn (Past Perfect Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + had been + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động k&eacute;o d&agrave;i trước một thời điểm hoặc h&agrave;nh động trong qu&aacute; khứ.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>He had been working for five hours before taking a break.</p></li>'', nchar(13), nchar(10), N''<li><p>They had been waiting for a long time before the bus arrived.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h1>III. C&aacute;c th&igrave; tương lai</h1>'', nchar(13), nchar(10), N''<h2>9. Tương lai đơn (Simple Future)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + will + V</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả quyết định tức thời.</p></li>'', nchar(13), nchar(10), N''<li><p>Dự đo&aacute;n hoặc lời hứa.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I will help you.</p></li>'', nchar(13), nchar(10), N''<li><p>It will rain tomorrow.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>10. Tương lai tiếp diễn (Future Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + will be + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động sẽ đang diễn ra tại một thời điểm trong tương lai.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>This time tomorrow, I will be studying.</p></li>'', nchar(13), nchar(10), N''<li><p>They will be traveling next week.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>11. Tương lai ho&agrave;n th&agrave;nh (Future Perfect)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + will have + V3/ed</p></li>'', nchar(13), CONCAT(CAST(nchar(10) AS nvarchar(max)), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Diễn tả h&agrave;nh động sẽ ho&agrave;n th&agrave;nh trước một thời điểm trong tương lai.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>I will have graduated by next year.</p></li>'', nchar(13), nchar(10), N''<li><p>She will have completed the project before Friday.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h2>12. Tương lai ho&agrave;n th&agrave;nh tiếp diễn (Future Perfect Continuous)</h2>'', nchar(13), nchar(10), N''<h3>C&ocirc;ng thức</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>S + will have been + V-ing</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>C&aacute;ch d&ugrave;ng</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>Nhấn mạnh khoảng thời gian một h&agrave;nh động k&eacute;o d&agrave;i đến một thời điểm trong tương lai.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<h3>V&iacute; dụ</h3>'', nchar(13), nchar(10), N''<ul>'', nchar(13), nchar(10), N''<li><p>By next month, I will have been working here for two years.</p></li>'', nchar(13), nchar(10), N''<li><p>They will have been studying for six hours by midnight.</p></li>'', nchar(13), nchar(10), N''</ul>'', nchar(13), nchar(10), N''<hr>'', nchar(13), nchar(10), N''<h1>Kết luận</h1>'', nchar(13), nchar(10), N''<p>Mười hai th&igrave; trong tiếng Anh gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian v&agrave; trạng th&aacute;i của h&agrave;nh động. Để sử dụng th&agrave;nh thạo, cần nắm vững c&ocirc;ng thức, dấu hiệu nhận biết v&agrave; c&aacute;ch d&ugrave;ng của từng th&igrave;. Việc luyện tập thường xuy&ecirc;n th&ocirc;ng qua n&oacute;i, viết v&agrave; l&agrave;m b&agrave;i tập sẽ gi&uacute;p người học sử dụng c&aacute;c th&igrave; một c&aacute;ch tự nhi&ecirc;n v&agrave; ch&iacute;nh x&aacute;c hơn trong giao tiếp cũng như trong học tập.</p>''))), ''2026-06-01T14:17:35.0000000'', N''Cách dùng và công thức của 12 Thì Trong Tiếng Anh'', N''images/fd75ef51-c277-4856-8f5f-a70515953e2d_Screenshot 2026-06-01 210443.png'', CAST(1 AS bit), CAST(0 AS bit), N''12 Thì Trong Tiếng Anh'', ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedAt', N'Description', N'ImageUrl', N'IsActive', N'IsDeleted', N'Name', N'TopicId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Courses]'))
        SET IDENTITY_INSERT [Courses] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'DurationInMinutes', N'ExamCategoryId', N'IsActive', N'IsDeleted', N'Title', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Exams]'))
        SET IDENTITY_INSERT [Exams] ON;
    EXEC(N'INSERT INTO [Exams] ([Id], [CreatedAt], [Description], [DurationInMinutes], [ExamCategoryId], [IsActive], [IsDeleted], [Title], [UpdatedAt])
    VALUES (''77777777-7777-7777-7777-777777777777'', ''2026-01-01T00:00:00.0000000'', N''Basic Grammar Test'', 10, ''11111111-1111-1111-1111-111111111111'', CAST(1 AS bit), CAST(0 AS bit), N''Basic Grammar Test'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Description', N'DurationInMinutes', N'ExamCategoryId', N'IsActive', N'IsDeleted', N'Title', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Exams]'))
        SET IDENTITY_INSERT [Exams] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AudioUrl', N'Content', N'CreatedAt', N'Explanation', N'ImageUrl', N'IsActive', N'IsDeleted', N'QuestionGroupId', N'QuestionTypes', N'TopicId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Questions]'))
        SET IDENTITY_INSERT [Questions] ON;
    EXEC(N'INSERT INTO [Questions] ([Id], [AudioUrl], [Content], [CreatedAt], [Explanation], [ImageUrl], [IsActive], [IsDeleted], [QuestionGroupId], [QuestionTypes], [TopicId], [UpdatedAt])
    VALUES (''33333333-3333-3333-3333-000000000001'', NULL, N''She ___ to school every day.'', ''2026-01-01T00:00:00.0000000'', N''Hành động lặp đi lặp lại ở hiện tại.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000002'', NULL, N''They usually ___ (play) basketball on weekends.'', ''2026-01-01T00:00:00.0000000'', N''Có trạng từ ''''usually'''' chỉ thói quen.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000003'', NULL, N''Look! The cat ___ over the wall.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đang xảy ra lúc nói (''''Look!'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000004'', NULL, N''I ___ (study) for my TOEIC exam right now.'', ''2026-01-01T00:00:00.0000000'', N''Có trạng từ ''''right now''''.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000005'', NULL, N''She ___ three cups of coffee today.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đã hoàn thành tính đến hiện tại.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000006'', NULL, N''We ___ (see) this movie before.'', ''2026-01-01T00:00:00.0000000'', N''Trải nghiệm tính đến thời điểm hiện tại (''''before'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000007'', NULL, N''I ___ for two hours. My eyes are tired.'', ''2026-01-01T00:00:00.0000000'', N''Nhấn mạnh quá trình kéo dài 2 tiếng và để lại hậu quả hiện tại.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000008'', NULL, N''It ___ (rain) since morning.'', ''2026-01-01T00:00:00.0000000'', N''Nhấn mạnh quá trình bắt đầu từ sáng và vẫn đang tiếp diễn.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000009'', NULL, N''He ___ to Paris last year.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đã kết thúc trong quá khứ (''''last year'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000010'', NULL, N''They ___ (win) the match yesterday.'', ''2026-01-01T00:00:00.0000000'', N''Sự việc kết thúc hôm qua (''''yesterday'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000011'', NULL, N''I ___ TV when the phone rang.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đang xảy ra thì có hành động khác xen vào.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000012'', NULL, N''While we ___ (play), it started to rain.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đang kéo dài trong quá khứ (''''While'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000013'', NULL, N''By the time I arrived, they ___.'', ''2026-01-01T00:00:00.0000000'', N''Hành động xảy ra trước một thời điểm trong quá khứ.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000014'', NULL, N''She told me she ___ (finish) the job.'', ''2026-01-01T00:00:00.0000000'', N''Hành động hoàn thành trước khi hành động ''''told'''' xảy ra.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000015'', NULL, N''They ___ for hours before the rescue team arrived.'', ''2026-01-01T00:00:00.0000000'', N''Nhấn mạnh quá trình kéo dài trước một mốc quá khứ.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000016'', NULL, N''I ___ (study) English for a year before I visited London.'', ''2026-01-01T00:00:00.0000000'', N''Hành động học kéo dài liên tục trước khi đến London.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000017'', NULL, N''I think it ___ tomorrow.'', ''2026-01-01T00:00:00.0000000'', N''Dự đoán không có căn cứ rõ ràng (''''I think'''').'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000018'', NULL, N''Don''''t worry, she ___ (call) you back later.'', ''2026-01-01T00:00:00.0000000'', N''Một lời hứa hoặc quyết định ngay lúc nói.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000019'', NULL, N''This time next week, I ___ on a beach.'', ''2026-01-01T00:00:00.0000000'', N''Hành động sẽ đang diễn ra tại một thời điểm xác định trong tương lai.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000020'', NULL, N''They ___ (have) dinner when we arrive tonight.'', ''2026-01-01T00:00:00.0000000'', N''Hành động đang diễn ra trong tương lai thì bị xen vào.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000021'', NULL, N''By next year, I ___ my graduation project.'', ''2026-01-01T00:00:00.0000000'', N''Hành động sẽ hoàn thành trước một mốc thời gian tương lai.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000022'', NULL, N''They ___ (build) the new bridge by July.'', ''2026-01-01T00:00:00.0000000'', N''Hoàn thành trước tháng 7 tới.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000023'', NULL, N''By next month, he ___ here for 5 years.'', ''2026-01-01T00:00:00.0000000'', N''Nhấn mạnh khoảng thời gian kéo dài tính đến tương lai.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 0, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000''),
    (''33333333-3333-3333-3333-000000000024'', NULL, N''By the time you wake up, I ___ (drive) for 3 hours.'', ''2026-01-01T00:00:00.0000000'', N''Hành động kéo dài liên tục đến lúc bạn thức dậy.'', NULL, CAST(1 AS bit), CAST(0 AS bit), NULL, 1, ''22222222-2222-2222-2222-222222222222'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AudioUrl', N'Content', N'CreatedAt', N'Explanation', N'ImageUrl', N'IsActive', N'IsDeleted', N'QuestionGroupId', N'QuestionTypes', N'TopicId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Questions]'))
        SET IDENTITY_INSERT [Questions] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedAt', N'IsActive', N'IsCorrect', N'IsDeleted', N'QuestionId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Answers]'))
        SET IDENTITY_INSERT [Answers] ON;
    EXEC(N'INSERT INTO [Answers] ([Id], [Content], [CreatedAt], [IsActive], [IsCorrect], [IsDeleted], [QuestionId], [UpdatedAt])
    VALUES (''44444444-4444-4444-4444-000000000001'', N''go'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000001'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000002'', N''goes'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000001'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000003'', N''going'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000001'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000004'', N''is going'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000001'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000005'', N''play'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000002'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000006'', N''jumps'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000003'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000007'', N''is jumping'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000003'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000008'', N''am studying'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000004'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000009'', N''drank'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000005'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000010'', N''has drunk'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000005'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000011'', N''is drinking'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000005'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000012'', N''have seen'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000006'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000013'', N''am reading'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000007'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000014'', N''have read'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000007'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000015'', N''have been reading'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000007'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000016'', N''read'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000007'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000017'', N''has been raining'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000008'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000018'', N''went'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000009'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000019'', N''goes'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000009'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000020'', N''won'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000010'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000021'', N''watched'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000011'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000022'', N''was watching'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000011'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000023'', N''am watching'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000011'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000024'', N''were playing'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000012'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000025'', N''left'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000013'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000026'', N''had left'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000013'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000027'', N''leave'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000013'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000028'', N''were leaving'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000013'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000029'', N''had finished'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000014'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000030'', N''had been walking'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000015'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000031'', N''walked'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000015'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000032'', N''had been studying'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000016'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000033'', N''will rain'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000017'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000034'', N''rains'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000017'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000035'', N''is raining'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000017'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000036'', N''will call'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000018'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000037'', N''will be relaxing'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000019'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000038'', N''will relax'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000019'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000039'', N''relax'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000019'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000040'', N''am relaxing'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000019'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000041'', N''will be having'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000020'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000042'', N''will finish'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000021'', ''0001-01-01T00:00:00.0000000'');
    INSERT INTO [Answers] ([Id], [Content], [CreatedAt], [IsActive], [IsCorrect], [IsDeleted], [QuestionId], [UpdatedAt])
    VALUES (''44444444-4444-4444-4444-000000000043'', N''will have finished'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000021'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000044'', N''will have built'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000022'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000045'', N''will work'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000023'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000046'', N''works'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000023'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000047'', N''will have been working'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000023'', ''0001-01-01T00:00:00.0000000''),
    (''44444444-4444-4444-4444-000000000048'', N''will have been driving'', ''2026-01-01T00:00:00.0000000'', CAST(1 AS bit), CAST(1 AS bit), CAST(0 AS bit), ''33333333-3333-3333-3333-000000000024'', ''0001-01-01T00:00:00.0000000'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Content', N'CreatedAt', N'IsActive', N'IsCorrect', N'IsDeleted', N'QuestionId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Answers]'))
        SET IDENTITY_INSERT [Answers] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExamId', N'QuestionId', N'Score') AND [object_id] = OBJECT_ID(N'[ExamDetails]'))
        SET IDENTITY_INSERT [ExamDetails] ON;
    EXEC(N'INSERT INTO [ExamDetails] ([ExamId], [QuestionId], [Score])
    VALUES (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000001'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000002'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000003'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000004'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000005'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000006'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000007'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000008'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000009'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000010'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000011'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000012'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000013'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000014'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000015'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000016'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000017'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000018'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000019'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000020'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000021'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000022'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000023'', 1.0E0),
    (''77777777-7777-7777-7777-777777777777'', ''33333333-3333-3333-3333-000000000024'', 1.0E0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExamId', N'QuestionId', N'Score') AND [object_id] = OBJECT_ID(N'[ExamDetails]'))
        SET IDENTITY_INSERT [ExamDetails] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_AnswersHistories_QuestionId] ON [AnswersHistories] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Courses_TopicId] ON [Courses] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_ExamDetails_QuestionId] ON [ExamDetails] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_ExamResults_ExamId] ON [ExamResults] ([ExamId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_ExamResults_UserId] ON [ExamResults] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Exams_ExamCategoryId] ON [Exams] ([ExamCategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_InboxState_Delivered] ON [InboxState] ([Delivered]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_OutboxMessage_EnqueueTime] ON [OutboxMessage] ([EnqueueTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_OutboxMessage_ExpirationTime] ON [OutboxMessage] ([ExpirationTime]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber] ON [OutboxMessage] ([InboxMessageId], [InboxConsumerId], [SequenceNumber]) WHERE [InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_OutboxMessage_OutboxId_SequenceNumber] ON [OutboxMessage] ([OutboxId], [SequenceNumber]) WHERE [OutboxId] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_OutboxState_Created] ON [OutboxState] ([Created]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_PracticeDetails_QuestionId] ON [PracticeDetails] ([QuestionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Practices_TopicId] ON [Practices] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Questions_QuestionGroupId] ON [Questions] ([QuestionGroupId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Questions_TopicId] ON [Questions] ([TopicId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    CREATE INDEX [IX_Words_FlashCardId] ON [Words] ([FlashCardId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702063523_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260702063523_init', N'9.0.11');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260702090130_init3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260702090130_init3', N'9.0.11');
END;

COMMIT;
GO

