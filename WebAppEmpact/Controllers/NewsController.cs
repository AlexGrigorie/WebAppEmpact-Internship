using Microsoft.AspNetCore.Mvc;
using WebAppEmpact.Data;
using WebAppEmpact.Data.DbModels;
using WebAppEmpact.Helper;
using WebAppEmpact.Interfaces;
using WebAppEmpact.Models;

namespace WebAppEmpact.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ApplicationDbContext _applicationDbContext;

        public NewsController(INewsService newsService, ApplicationDbContext applicationDbContext)
        {
            _newsService = newsService;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index(int? pageNumber, string sortOrder, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["CurrentFilter"] = searchString;

            var newsViewModel = _applicationDbContext.News.Select(n => new NewsViewModel
            {
                Title = n.Title,
                Description = n.Description,
                PublicationDate = n.PublicationDate,
                Link = n.Link,
            }).ToList();

            var sortedNews = _newsService.GetNews();
            sortedNews.AddRange(newsViewModel);
            if (!String.IsNullOrEmpty(searchString))
            {
                sortedNews = sortedNews.Where(sn => sn.Title.ToLower().Contains(searchString.ToLower())
                                       || sn.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "title_desc":
                    sortedNews = sortedNews.OrderByDescending(s => s.Title).ToList();
                    break;
                case "date_asc":
                    sortedNews = sortedNews.OrderBy(s => s.PublicationDate).ToList();
                    break;
                case "date_desc":
                    sortedNews = sortedNews.OrderByDescending(s => s.PublicationDate).ToList();
                    break;
                default:
                    sortedNews = sortedNews.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 7;

            var news = Pagination<NewsViewModel>.Create(sortedNews, pageNumber ?? 1, pageSize);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }
        public IActionResult AddNews()
        {
            var newNewsViewModel = new NewNewsViewModel();
            return View(newNewsViewModel);
        }

        [HttpPost]
        public IActionResult AddNewStation(NewNewsViewModel newsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddNews", newsViewModel);
            }
            _applicationDbContext.News.Add(new NewsDbModel
            {
                Title = newsViewModel.Title,
                Description = newsViewModel.Description,
                Publisher = newsViewModel.Publisher,
                Link = newsViewModel.Link,
                PublicationDate = DateTimeOffset.Now
            });
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Index", "News");
        }
    }
}
