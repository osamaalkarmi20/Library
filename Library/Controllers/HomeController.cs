using DataLayer.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using ServiceLayer.Interface;
using System.Diagnostics;
using System.Globalization;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBook _book;
		private readonly IShelf _shelf;

		private readonly IHtmlLocalizer<HomeController> _localizer;  
      public HomeController(ILogger<HomeController> logger, IBook book,IShelf shelf, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _book = book;
			_shelf = shelf;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
			var Shelves = await _shelf.GetAll();
			var Books = await _book.GetAll();

			List<int> BookQuantitys = new List<int>();
			List<string> BookLabels = new List<string>();
			List<string> ShelfLabels = new List<string>();
			List<int> ShelfBooksCount = new List<int>();
			foreach (var item in Books)
			{
				BookLabels.Add(item.Name);
				BookQuantitys.Add(item.Quantity);

			}
			foreach (var item in Shelves)
			{
				ShelfLabels.Add(item.Name);
				ShelfBooksCount.Add(item.BookCount);
			}

			var BL = BookLabels.ToArray();
			var BQ = BookQuantitys.ToArray();
			var SL = ShelfLabels.ToArray();
			var SBC = ShelfBooksCount.ToArray();
			ViewData["BookLabels"] = BL;
			ViewData["BookQuantitys"] = BQ;
			ViewData["ShelfLabels"] = SL;
			ViewData["ShelfBooksCount"] = SBC;
			return View(Shelves);
	}
		[HttpPost]
        public IActionResult CultureMangement(string culture, string returnurl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),new CookieOptions { Expires =DateTimeOffset.Now.AddDays(30)});
            return LocalRedirect(returnurl);

        }


        public IActionResult Privacy()
        {
            return View();
        }
     


    }
}
