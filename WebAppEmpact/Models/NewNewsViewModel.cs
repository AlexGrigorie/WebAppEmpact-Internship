using System.ComponentModel.DataAnnotations;

namespace WebAppEmpact.Models
{
    public class NewNewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
