using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginregister.Models
{
    public class Like : BaseEntity
    {
        [Key]
        public int LikeId { get; set; }

       
        public int IdeaId { get; set; }
        public Idea Idea { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}