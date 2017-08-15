using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{   

    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RatingId { get; set; }


        [Required]
        [StringLength(128)]
        [Display(Name = "User")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }


        [Required]
        [StringLength(250)]
        [Display(Name = "Game")]
        public string GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int GameRating { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreateDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Edit Date")]
        public DateTime? EditDate { get; set; } = DateTime.UtcNow;
    }
}
