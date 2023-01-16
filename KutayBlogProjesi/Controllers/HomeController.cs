using KutayBlogProjesi.Context;
using KutayBlogProjesi.Filters;
using KutayBlogProjesi.Models;
using KutayBlogProjesi.Models.VM.Home.Profile;
using KutayBlogProjesi.Models.VM.Home.Overview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KutayBlogProjesi.Models.Entities.Concrete;
using KutayBlogProjesi.Models.VM.Article.Edit;
using Microsoft.AspNetCore.Http;
using KutayBlogProjesi.Models.DTOs;

namespace KutayBlogProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<ArticleViewModel> list = _context.Articles.OrderByDescending(x => x.CreateDate)
                                           .Take(20)
                                           .Select(x => new ArticleViewModel()
                                           {
                                               Id = x.Id,
                                               AuthorId = x.Author.Id.ToString(),
                                               AuthorName = x.Author.Username,
                                               ArticlePicture = string.IsNullOrEmpty(x.ArticlePicture) ? "null.png" : x.ArticlePicture,
                                               Title = x.Title,
                                               Content = x.Content,
                                               CreatedTime = x.CreateDate,
                                           }).ToList();
            return View(list);
        }

        [HttpGet("[controller]/[action]/{username}")]
        public IActionResult Profile(string username)
        {
            List<ArticleViewModel> list = _context.Articles
                .Where(x => x.Author.Username.Equals(username))
                .OrderByDescending(x => x.CreateDate)
                .Select(x => new ArticleViewModel()
                {
                    Id = x.Id,
                    AuthorId = x.Author.Id.ToString(),
                    AuthorName = x.Author.Username,
                    ArticlePicture = string.IsNullOrEmpty(x.ArticlePicture) ? "null.png" : x.ArticlePicture,
                    Title = x.Title,
                    Content = x.Content,
                    CreatedTime = x.CreateDate
                }).ToList();
            return View(list);
        }



        

        public IActionResult Overview(int id)
        {
            var model = _context.Articles.Select(x => new OverviewViewModel()
            {
                Id = x.Id,
                Author = x.Author.Username,
                ArticlePicture = string.IsNullOrEmpty(x.ArticlePicture) ? "null.png" : x.ArticlePicture,
                Title = x.Title,
                Content = x.Content,
                CreatedTime = x.CreateDate
            }).FirstOrDefault(x => x.Id.Equals(id));

            return View(model);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
