BEGIN TRANSACTION;
ALTER TABLE [ExamResults] ADD [TotalScore] float NOT NULL DEFAULT 0.0E0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260801121644_add-totalscorefield', N'9.0.11');

COMMIT;
GO

