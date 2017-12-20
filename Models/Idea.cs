using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginregister.Models
{
    public class Idea : BaseEntity
    {
        [Key]
        public int IdeaId { get; set; }

        [Required(ErrorMessage = "Idea input can't be blank and has to be at least 10 characters long.")]
        [StringLength(1000, MinimumLength = 10)]
        public string IdeaText { get; set; }
        
    
        public int CreatedById { get; set; }
        
        public User CreatedBy { get; set; }
        public List<Like> Likes { get; set; }
        
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public Idea()
        {
            Likes = new List<Like>();
        }     
        
    }
}