using System.ServiceModel.Syndication;
using System.Xml;
using WebAppEmpact.Interfaces;
using WebAppEmpact.Models;

namespace WebAppEmpact.Services
{
    public class NewsService : INewsService
    {
        public List<NewsViewModel> GetNews()
        {
            var url = "https://rss.nytimes.com/services/xml/rss/nyt/World.xml";
            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            var items = feed.Items.Select(i => new NewsViewModel
            {
                Title = i.Title.Text,
                Link = i.Links.FirstOrDefault().Uri.ToString(),
                Description = i.Summary.Text,
                PublicationDate = i.PublishDate,
            }).ToList();
            return items;
        }
    }
}
