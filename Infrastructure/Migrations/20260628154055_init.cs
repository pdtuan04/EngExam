using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RootCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlashCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxState",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveCount = table.Column<int>(type: "int", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Consumed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxState", x => x.Id);
                    table.UniqueConstraint("AK_InboxState_MessageId_ConsumerId", x => new { x.MessageId, x.ConsumerId });
                });

            migrationBuilder.CreateTable(
                name: "OutboxState",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxState", x => x.OutboxId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    ExamCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_ExamCategories_ExamCategoryId",
                        column: x => x.ExamCategoryId,
                        principalTable: "ExamCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meaning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMemorized = table.Column<bool>(type: "bit", nullable: false),
                    FlashCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_FlashCards_FlashCardId",
                        column: x => x.FlashCardId,
                        principalTable: "FlashCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnqueueTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboxMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InboxConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ResponseAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FaultAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.SequenceNumber);
                    table.ForeignKey(
                        name: "FK_OutboxMessage_InboxState_InboxMessageId_InboxConsumerId",
                        columns: x => new { x.InboxMessageId, x.InboxConsumerId },
                        principalTable: "InboxState",
                        principalColumns: new[] { "MessageId", "ConsumerId" });
                    table.ForeignKey(
                        name: "FK_OutboxMessage_OutboxState_OutboxId",
                        column: x => x.OutboxId,
                        principalTable: "OutboxState",
                        principalColumn: "OutboxId");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practices_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTypes = table.Column<int>(type: "int", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    QuestionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionGroup_QuestionGroupId",
                        column: x => x.QuestionGroupId,
                        principalTable: "QuestionGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompleteAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResults_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamDetails",
                columns: table => new
                {
                    ExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDetails", x => new { x.ExamId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_ExamDetails_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticeDetails",
                columns: table => new
                {
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeDetails", x => new { x.PracticeId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_PracticeDetails_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticeDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnswersHistories",
                columns: table => new
                {
                    ExamResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersHistories", x => new { x.ExamResultId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_AnswersHistories_ExamResults_ExamResultId",
                        column: x => x.ExamResultId,
                        principalTable: "ExamResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswersHistories_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("05f2400b-5471-466a-8b7e-27752367e4d6"), null, "User", "USER" },
                    { new Guid("10f2400b-5471-466a-8b7e-27752367e4d6"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "ImageUrl", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { new Guid("8d581a98-361e-4333-a651-74e88ef572a4"), 0, null, "f67e2437-61a2-4458-ac14-de7ab48158b6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@gmail.com", true, null, true, false, null, "USER@GMAIL.COM", "USER", "AQAAAAIAAYagAAAAEN8TWXW9pNZ+VVyeftOLixsSfyDOtPTZpv84QtbFESyzd6kZ0i70eIPvnvNBKX0Q9Q==", null, false, "DF7GIIY7UNBVCVLZD73QO6PGSVQXBSTW", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user" },
                    { new Guid("9ae1058d-b602-4025-ab1d-74e7bced8f3b"), 0, null, "6e66d8c1-89da-46df-bc24-ec54c7e7e7cf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, null, true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFY87mzNg88TIJtuXRcRIeT0MXYto4NkcukxwFGpl+p5IHBJVqlPbyFx9UJIOmu7eA==", null, false, "3XVVZIW5RPRWT7MKN3Y6VRNTHXY2JGK5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" }
                });

            migrationBuilder.InsertData(
                table: "ExamCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grammar examination category", "/uploads/images/category_img.jpg", true, false, "Grammar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2af67565-75f7-4511-9b67-3762e917c173"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vocabulary exam", "/uploads/images/category_img.jpg", true, false, "Vocabulary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("48b31fd9-e2a2-4b6a-9884-e2b6c664715b"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Listening exam", "/uploads/images/category_img.jpg", true, false, "Listening", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c5f9dd20-276f-4a4a-bbb1-26b795a8514c"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reading", "/uploads/images/category_img.jpg", true, false, "Reading", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 thì cơ bản trong tiếng anh.", true, false, "12 Thì Trong Tiếng Anh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("05f2400b-5471-466a-8b7e-27752367e4d6"), new Guid("8d581a98-361e-4333-a651-74e88ef572a4") },
                    { new Guid("10f2400b-5471-466a-8b7e-27752367e4d6"), new Guid("9ae1058d-b602-4025-ab1d-74e7bced8f3b") }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Content", "CreatedAt", "Description", "ImageUrl", "IsActive", "IsDeleted", "Name", "TopicId", "UpdatedAt" },
                values: new object[] { new Guid("2b82c46a-209b-4c86-b917-9ee78a51efeb"), "<h1>12 Th&igrave; Trong Tiếng Anh</h1>\r\n<h2>Giới thiệu</h2>\r\n<p>Th&igrave; (Tense) l&agrave; một trong những phần ngữ ph&aacute;p quan trọng nhất trong tiếng Anh. Việc sử dụng đ&uacute;ng th&igrave; gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian, trạng th&aacute;i v&agrave; qu&aacute; tr&igrave;nh của h&agrave;nh động. Hệ thống ngữ ph&aacute;p tiếng Anh bao gồm 12 th&igrave; cơ bản, được chia th&agrave;nh ba mốc thời gian ch&iacute;nh: hiện tại, qu&aacute; khứ v&agrave; tương lai. Mỗi mốc thời gian lại c&oacute; bốn dạng: đơn, tiếp diễn, ho&agrave;n th&agrave;nh v&agrave; ho&agrave;n th&agrave;nh tiếp diễn.</p>\r\n<hr>\r\n<h1>I. C&aacute;c th&igrave; hiện tại</h1>\r\n<h2>1. Hiện tại đơn (Simple Present)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>Khẳng định: S + V(s/es)</p></li>\r\n<li><p>Phủ định: S + do/does not + V</p></li>\r\n<li><p>Nghi vấn: Do/Does + S + V?</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả th&oacute;i quen, sở th&iacute;ch.</p></li>\r\n<li><p>Diễn tả sự thật hiển nhi&ecirc;n.</p></li>\r\n<li><p>Diễn tả lịch tr&igrave;nh, thời gian biểu.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I go to school every day.</p></li>\r\n<li><p>The sun rises in the east.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>2. Hiện tại tiếp diễn (Present Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + am/is/are + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động đang diễn ra tại thời điểm n&oacute;i.</p></li>\r\n<li><p>Diễn tả kế hoạch trong tương lai gần.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>She is studying English now.</p></li>\r\n<li><p>We are meeting our teacher tomorrow.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>3. Hiện tại ho&agrave;n th&agrave;nh (Present Perfect)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + have/has + V3/ed</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động xảy ra trong qu&aacute; khứ nhưng c&ograve;n li&ecirc;n quan đến hiện tại.</p></li>\r\n<li><p>Diễn tả kinh nghiệm hoặc trải nghiệm.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I have finished my homework.</p></li>\r\n<li><p>She has visited Japan twice.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>4. Hiện tại ho&agrave;n th&agrave;nh tiếp diễn (Present Perfect Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + have/has been + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động bắt đầu trong qu&aacute; khứ v&agrave; vẫn tiếp tục đến hiện tại.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>They have been learning English for three years.</p></li>\r\n<li><p>I have been waiting for an hour.</p></li>\r\n</ul>\r\n<hr>\r\n<h1>II. C&aacute;c th&igrave; qu&aacute; khứ</h1>\r\n<h2>5. Qu&aacute; khứ đơn (Simple Past)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + V2/ed</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động đ&atilde; xảy ra v&agrave; kết th&uacute;c trong qu&aacute; khứ.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I visited my grandparents last weekend.</p></li>\r\n<li><p>She bought a new laptop yesterday.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>6. Qu&aacute; khứ tiếp diễn (Past Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + was/were + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động đang diễn ra tại một thời điểm trong qu&aacute; khứ.</p></li>\r\n<li><p>Diễn tả h&agrave;nh động bị h&agrave;nh động kh&aacute;c xen v&agrave;o.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I was reading when he called.</p></li>\r\n<li><p>They were playing football at 5 p.m.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>7. Qu&aacute; khứ ho&agrave;n th&agrave;nh (Past Perfect)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + had + V3/ed</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động xảy ra trước một h&agrave;nh động kh&aacute;c trong qu&aacute; khứ.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>She had left before I arrived.</p></li>\r\n<li><p>They had finished dinner when we came.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>8. Qu&aacute; khứ ho&agrave;n th&agrave;nh tiếp diễn (Past Perfect Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + had been + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Nhấn mạnh qu&aacute; tr&igrave;nh của h&agrave;nh động k&eacute;o d&agrave;i trước một thời điểm hoặc h&agrave;nh động trong qu&aacute; khứ.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>He had been working for five hours before taking a break.</p></li>\r\n<li><p>They had been waiting for a long time before the bus arrived.</p></li>\r\n</ul>\r\n<hr>\r\n<h1>III. C&aacute;c th&igrave; tương lai</h1>\r\n<h2>9. Tương lai đơn (Simple Future)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + will + V</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả quyết định tức thời.</p></li>\r\n<li><p>Dự đo&aacute;n hoặc lời hứa.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I will help you.</p></li>\r\n<li><p>It will rain tomorrow.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>10. Tương lai tiếp diễn (Future Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + will be + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động sẽ đang diễn ra tại một thời điểm trong tương lai.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>This time tomorrow, I will be studying.</p></li>\r\n<li><p>They will be traveling next week.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>11. Tương lai ho&agrave;n th&agrave;nh (Future Perfect)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + will have + V3/ed</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Diễn tả h&agrave;nh động sẽ ho&agrave;n th&agrave;nh trước một thời điểm trong tương lai.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>I will have graduated by next year.</p></li>\r\n<li><p>She will have completed the project before Friday.</p></li>\r\n</ul>\r\n<hr>\r\n<h2>12. Tương lai ho&agrave;n th&agrave;nh tiếp diễn (Future Perfect Continuous)</h2>\r\n<h3>C&ocirc;ng thức</h3>\r\n<ul>\r\n<li><p>S + will have been + V-ing</p></li>\r\n</ul>\r\n<h3>C&aacute;ch d&ugrave;ng</h3>\r\n<ul>\r\n<li><p>Nhấn mạnh khoảng thời gian một h&agrave;nh động k&eacute;o d&agrave;i đến một thời điểm trong tương lai.</p></li>\r\n</ul>\r\n<h3>V&iacute; dụ</h3>\r\n<ul>\r\n<li><p>By next month, I will have been working here for two years.</p></li>\r\n<li><p>They will have been studying for six hours by midnight.</p></li>\r\n</ul>\r\n<hr>\r\n<h1>Kết luận</h1>\r\n<p>Mười hai th&igrave; trong tiếng Anh gi&uacute;p người học diễn đạt ch&iacute;nh x&aacute;c thời gian v&agrave; trạng th&aacute;i của h&agrave;nh động. Để sử dụng th&agrave;nh thạo, cần nắm vững c&ocirc;ng thức, dấu hiệu nhận biết v&agrave; c&aacute;ch d&ugrave;ng của từng th&igrave;. Việc luyện tập thường xuy&ecirc;n th&ocirc;ng qua n&oacute;i, viết v&agrave; l&agrave;m b&agrave;i tập sẽ gi&uacute;p người học sử dụng c&aacute;c th&igrave; một c&aacute;ch tự nhi&ecirc;n v&agrave; ch&iacute;nh x&aacute;c hơn trong giao tiếp cũng như trong học tập.</p>", new DateTime(2026, 6, 1, 14, 17, 35, 0, DateTimeKind.Unspecified), "Cách dùng và công thức của 12 Thì Trong Tiếng Anh", "images/fd75ef51-c277-4856-8f5f-a70515953e2d_Screenshot 2026-06-01 210443.png", true, false, "12 Thì Trong Tiếng Anh", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationInMinutes", "ExamCategoryId", "IsActive", "IsDeleted", "Title", "UpdatedAt" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic Grammar Test", 10, new Guid("11111111-1111-1111-1111-111111111111"), true, false, "Basic Grammar Test", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AudioUrl", "Content", "CreatedAt", "Explanation", "ImageUrl", "IsActive", "IsDeleted", "QuestionGroupId", "QuestionTypes", "TopicId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-000000000001"), null, "She ___ to school every day.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động lặp đi lặp lại ở hiện tại.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000002"), null, "They usually ___ (play) basketball on weekends.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Có trạng từ 'usually' chỉ thói quen.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000003"), null, "Look! The cat ___ over the wall.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang xảy ra lúc nói ('Look!').", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000004"), null, "I ___ (study) for my TOEIC exam right now.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Có trạng từ 'right now'.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000005"), null, "She ___ three cups of coffee today.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đã hoàn thành tính đến hiện tại.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000006"), null, "We ___ (see) this movie before.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trải nghiệm tính đến thời điểm hiện tại ('before').", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000007"), null, "I ___ for two hours. My eyes are tired.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình kéo dài 2 tiếng và để lại hậu quả hiện tại.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000008"), null, "It ___ (rain) since morning.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình bắt đầu từ sáng và vẫn đang tiếp diễn.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000009"), null, "He ___ to Paris last year.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đã kết thúc trong quá khứ ('last year').", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000010"), null, "They ___ (win) the match yesterday.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự việc kết thúc hôm qua ('yesterday').", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000011"), null, "I ___ TV when the phone rang.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang xảy ra thì có hành động khác xen vào.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000012"), null, "While we ___ (play), it started to rain.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang kéo dài trong quá khứ ('While').", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000013"), null, "By the time I arrived, they ___.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động xảy ra trước một thời điểm trong quá khứ.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000014"), null, "She told me she ___ (finish) the job.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động hoàn thành trước khi hành động 'told' xảy ra.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000015"), null, "They ___ for hours before the rescue team arrived.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh quá trình kéo dài trước một mốc quá khứ.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000016"), null, "I ___ (study) English for a year before I visited London.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động học kéo dài liên tục trước khi đến London.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000017"), null, "I think it ___ tomorrow.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dự đoán không có căn cứ rõ ràng ('I think').", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000018"), null, "Don't worry, she ___ (call) you back later.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một lời hứa hoặc quyết định ngay lúc nói.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000019"), null, "This time next week, I ___ on a beach.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động sẽ đang diễn ra tại một thời điểm xác định trong tương lai.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000020"), null, "They ___ (have) dinner when we arrive tonight.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động đang diễn ra trong tương lai thì bị xen vào.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000021"), null, "By next year, I ___ my graduation project.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động sẽ hoàn thành trước một mốc thời gian tương lai.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000022"), null, "They ___ (build) the new bridge by July.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàn thành trước tháng 7 tới.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000023"), null, "By next month, he ___ here for 5 years.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhấn mạnh khoảng thời gian kéo dài tính đến tương lai.", null, true, false, null, 0, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-000000000024"), null, "By the time you wake up, I ___ (drive) for 3 hours.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành động kéo dài liên tục đến lúc bạn thức dậy.", null, true, false, null, 1, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Content", "CreatedAt", "IsActive", "IsCorrect", "IsDeleted", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-000000000001"), "go", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000002"), "goes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000003"), "going", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000004"), "is going", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000005"), "play", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000006"), "jumps", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000007"), "is jumping", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000008"), "am studying", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000009"), "drank", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000010"), "has drunk", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000011"), "is drinking", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000012"), "have seen", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000013"), "am reading", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000014"), "have read", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000015"), "have been reading", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000016"), "read", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000017"), "has been raining", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000018"), "went", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000019"), "goes", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000020"), "won", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000021"), "watched", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000022"), "was watching", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000023"), "am watching", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000024"), "were playing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000025"), "left", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000026"), "had left", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000027"), "leave", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000028"), "were leaving", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000029"), "had finished", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000014"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000030"), "had been walking", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000031"), "walked", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000015"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000032"), "had been studying", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000016"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000033"), "will rain", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000034"), "rains", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000035"), "is raining", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000017"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000036"), "will call", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000018"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000037"), "will be relaxing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000038"), "will relax", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000039"), "relax", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000040"), "am relaxing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000019"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000041"), "will be having", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000020"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000042"), "will finish", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000043"), "will have finished", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000021"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000044"), "will have built", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000045"), "will work", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000046"), "works", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000047"), "will have been working", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000023"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("44444444-4444-4444-4444-000000000048"), "will have been driving", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, new Guid("33333333-3333-3333-3333-000000000024"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ExamDetails",
                columns: new[] { "ExamId", "QuestionId", "Score" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000001"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000002"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000003"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000004"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000005"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000006"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000007"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000008"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000009"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000010"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000011"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000012"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000013"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000014"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000015"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000016"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000017"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000018"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000019"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000020"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000021"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000022"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000023"), 1.0 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-000000000024"), 1.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersHistories_QuestionId",
                table: "AnswersHistories",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TopicId",
                table: "Courses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_QuestionId",
                table: "ExamDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_UserId",
                table: "ExamResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ExamCategoryId",
                table: "Exams",
                column: "ExamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxState_Delivered",
                table: "InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                table: "OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true,
                filter: "[InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_OutboxId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true,
                filter: "[OutboxId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                table: "OutboxState",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeDetails_QuestionId",
                table: "PracticeDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_TopicId",
                table: "Practices",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionGroupId",
                table: "Questions",
                column: "QuestionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TopicId",
                table: "Questions",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_FlashCardId",
                table: "Words",
                column: "FlashCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AnswersHistories");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "ExamDetails");

            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.DropTable(
                name: "PracticeDetails");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "InboxState");

            migrationBuilder.DropTable(
                name: "OutboxState");

            migrationBuilder.DropTable(
                name: "Practices");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "FlashCards");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "QuestionGroup");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "ExamCategories");
        }
    }
}
