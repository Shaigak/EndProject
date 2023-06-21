﻿namespace MyEndProjectCode.Models
{
    public class BlogComment : BaseEntity
    {
        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public string AppUserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string? Subject { get; set; }

        public string Message { get; set; }


    }
}