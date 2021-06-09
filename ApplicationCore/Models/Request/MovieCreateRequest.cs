using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
    public class MovieCreateRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(2084)]
        public string Overview { get; set; }
        [StringLength(2084)]
        public string Tagline { get; set; }
        [Required]
        [Range(0, 5000000000)]
        public decimal? Budget { get; set; }
        [Range(0, 5000000000)]
        [RegularExpression("^(\\d{1,18})(.\\d{1})?$")]
        public decimal? Revenue { get; set; }
        [Url]
        public string ImdbUrl { get; set; }
        [Url]
        public string TmdbUrl { get; set; }
        [Required]
        [Url]
        public string PosterUrl { get; set; }
        [Required]
        [Url]
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        [Range(.99, 49)]
        public decimal? Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(150)]
        public string CreatedBy { get; set; }
        [MaxLength(150)]
        public string UpdatedBy { get; set; }
        public decimal? Rating { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Cast> Casts { get; set; }

    }
}
