BEGIN TRANSACTION;
CREATE TABLE [Vocabularies] (
    [Id] uniqueidentifier NOT NULL,
    [Word] nvarchar(max) NOT NULL,
    [Phonetic] nvarchar(max) NOT NULL,
    [Meaning] nvarchar(max) NOT NULL,
    [PronunciationAudioUrl] nvarchar(max) NOT NULL,
    [PartOfSpeech] int NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Vocabularies] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260808164052_addvoca', N'9.0.11');

COMMIT;
GO

