﻿using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        
        public string Username { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int PostId { get; set; }
    
        public int UserId { get; set; }
    }
}
