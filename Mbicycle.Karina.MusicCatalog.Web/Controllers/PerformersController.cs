using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class PerformersController : Controller
    {
        private readonly MusicContext db;

        public PerformersController(MusicContext context)
        {
            this.db = context;
        }

        public ActionResult Index()
        {
            return View(db.Performers.ToList());
        }
    }
}
