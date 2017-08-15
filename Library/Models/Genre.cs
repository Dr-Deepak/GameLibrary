namespace Library.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        
        public Genre()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GenreId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreateDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Edit Date")]
        public DateTime? EditDate { get; set; } = DateTime.UtcNow;


        [Display(Name = "Games")]
        [InverseProperty("Genre")]
        public virtual ICollection<GameGenre> Games { get; set; } = new HashSet<GameGenre>();

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
