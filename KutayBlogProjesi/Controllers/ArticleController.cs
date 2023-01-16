using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KutayBlogProjesi.Filters;
using KutayBlogProjesi.Managers;
using KutayBlogProjesi.Context;
using KutayBlogProjesi.Models.Entities.Concrete;
using KutayBlogProjesi.Models.VM.Article.Create;
using KutayBlogProjesi.Models.VM.Article.Edit;

namespace KutayBlogProjesi.Controllers
{
    [LoggedUser]
    public class ArticleController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleController(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create(string rPATH)
        {
            ViewBag.rPATH = rPATH;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel model, string rPATH)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    AuthorId = int.Parse(HttpContext.Session.GetString("userId")),
                    ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment)
                };
                _context.Articles.Add(article);
                _context.SaveChanges();
                TempData["message"] = "Article Created.";
                if (rPATH == null) return RedirectToAction("Index", "Home");
                return Redirect(rPATH);
            }
            else return View(model);
        }

        public IActionResult Edit(int id)
        {
            Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));

            if (article is not null) return View(new EditViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ArticlePictureName = article.ArticlePicture
            });
            else
            {
                TempData["error"] = "There is no data.";
                return RedirectToAction("Profile", "Home", new { username = HttpContext.Session.GetString("username") });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(model.Id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));

                if (article is null)
                {
                    ViewData["error"] = "Edit failed!";
                    return View(model);
                }

                article.Title = model.Title;
                article.Content = model.Content;

                if (model.ArticlePicture != null)
                {

                    article.ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);

                    FileManager.RemoveImageFromDisk(model.ArticlePictureName, _webHostEnvironment);
                }

                _context.SaveChanges();

                TempData["message"] = "Article Editing Completed!";
                return RedirectToAction("Profile", "Home", new { username = HttpContext.Session.GetString("username") });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Article article = _context.Articles.FirstOrDefault(x => x.Id.Equals(id) && x.AuthorId.ToString().Equals(HttpContext.Session.GetString("userId")));

            if (article is not null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
                FileManager.RemoveImageFromDisk(article.ArticlePicture, _webHostEnvironment);
                TempData["message"] = "Delete Completed!";
            }
            else TempData["error"] = "There is no data.";

            return RedirectToAction("Profile", "Home", new { username = HttpContext.Session.GetString("username") });
        }
    }
}
