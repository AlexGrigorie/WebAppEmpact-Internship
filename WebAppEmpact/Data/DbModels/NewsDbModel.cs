namespace WebAppEmpact.Data.DbModels
{
    public class NewsDbModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Link { get; set; }
        public DateTimeOffset PublicationDate { get; set; }

    }
}
