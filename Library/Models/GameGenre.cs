namespace Library.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GameGenreId { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; }

        [Required]
        [StringLength(128)]
        public string GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [Required]
        [StringLength(128)]
        public string GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Game.ToString(), Genre.ToString());
        }
    }
}
