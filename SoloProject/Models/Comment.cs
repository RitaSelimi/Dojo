using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoloProject.Models
{
    public class Comment
    {
        [Key]
        public int CommentId {get; set;}
        
        [Required]
        [Display(Name="Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name="Your Comment")]
        public string Description { get; set; }

        public RegisterUser User {get; set; }
        public int UserId {get; set; }

        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}
