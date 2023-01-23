using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo_Travel.Models
{
    public class RecentPost
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
