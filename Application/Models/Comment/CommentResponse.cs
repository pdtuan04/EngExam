using Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Comment
{
    public sealed record CommentResponse
    {
        public Guid Id { get; }
        public Guid CourseId { get; }
        public Guid UserId { get; }
        public string UserName { get; }
        public string UserAvatarUrl { get; }
        public string Content { get; }
        public Guid? ParentId { get; }
        public Guid RootCommentId { get; }
        public string Path { get; }
        public int Level { get; }
        public bool IsDeleted { get; }
        public int ReplyCount { get; }

        public CommentResponse(Guid id, Guid courseId, Guid userId, string userName, string? userAvatarUrl, string content, Guid? parentId, Guid rootCommentId, string path, int level, bool isDeleted, int replyCount)
        {
            Id = id;
            CourseId = courseId;
            UserId = userId;
            UserName = userName;
            UserAvatarUrl = string.IsNullOrEmpty(userAvatarUrl) ? null : userAvatarUrl.GetFileUrl();
            Content = content;
            ParentId = parentId;
            RootCommentId = rootCommentId;
            Path = path;
            Level = level;
            IsDeleted = isDeleted;
            ReplyCount = replyCount;
        }
    }
}
