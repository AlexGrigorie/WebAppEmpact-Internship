namespace WebAppEmpact.Models
{
    public class NewsViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
    }
}
