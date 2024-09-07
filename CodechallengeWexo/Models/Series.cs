using System.ComponentModel.DataAnnotations;

namespace CodechallengeWexo.Models
{
    public class Series
    {
        [Key]
        public string Guid { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; }

    }
}
