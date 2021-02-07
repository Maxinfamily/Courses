using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
        [Route("blog")] //Doesnt apply to actions without Route attribute
        // GET: /<controller>/
        public IActionResult Index()
        {
            //return new ContentResult { Content = "Blog posts" };

            var posts = new[]
            {
                new Post
                {
                    Title = "My blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is a great blog post, don't you think?",
                },
                new Post
                {
                    Title = "My second blog post",
                    Posted = DateTime.Now,
                    Author = "Jess Chadwick",
                    Body = "This is ANOTHER great blog post, don't you think?",
                },
            };

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Jess Chadwick",
                Body = "This is a great blog post, don't you think?",
            };

            return View(post);
        }

        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }



        //[Route(@"{year:min(2000)}/{month:range(1,12)}/{key}")]
        //public IActionResult Post(int year, int month, string key)
        //{
        //    ViewBag.Title = "My blog post";
        //    ViewBag.Posted = DateTime.Now;
        //    ViewBag.Author = "Jess Chadwick";
        //    ViewBag.Body = "This is a great blog post, don't you think?";

        //    return View();
        //}

        //[Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        //public IActionResult Post (int year, int month, string key)
        //{
        //    //return new ContentResult { Content = string.Format("Year: {0}; Month: {1}; Key: {2}", year, month, key) };

        //    return View();
        //}

        //public IActionResult Post(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new ContentResult { Content = "Null" };
        //    }
        //    else
        //    {
        //        return new ContentResult { Content = id.ToString() };
        //    }
        //}

        // Other way to do the above "Post" method
        //public IActionResult Post (int id = -1)
        //{
        //    return new ContentResult { Content = id.ToString() };
        //}
    }
}
