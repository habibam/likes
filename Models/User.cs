using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace loginregister.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Alias { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]

        public string ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        public List<Like> Likes { get; set; }
  

        public User()
        {
            Likes = new List<Like>();
        }
            
    }

    

    public class LoginUser : BaseEntity
    {


        [Key]
        public int User_Id { get; set; }

        [Required]
        [EmailAddress]
        public string LogEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmLogPassword { get; set; }

    }

    public abstract class BaseEntity
    {

    }
}