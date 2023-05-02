using WebAppEmpact.Models;

namespace WebAppEmpact.Interfaces
{
    public interface INewsService
    {
        List<NewsViewModel> GetNews();
    }
}
