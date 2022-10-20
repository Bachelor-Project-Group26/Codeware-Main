﻿using System.ComponentModel.DataAnnotations;

namespace BPR_API.DBModels
{
    public class Reaction
    {
        [Key]
        public int PostId { get; set; }
        [Key]
        public int UserId { get; set; }
        [Required]
        public int ReactionNumber { get; set; } // 1 - Like | 2 - Dislike
    }
}
