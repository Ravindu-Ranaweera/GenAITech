using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GenAITech.Models
{
    public class GenAI
    {
        public int Id { get; set; }
        public string GenAIName { get; set; }
        public string Summary { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImageFilename { get; set; }
        public string? AnchorLink { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
