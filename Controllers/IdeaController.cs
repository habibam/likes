using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginregister.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace loginregister.Controllers
{
    public class IdeaController : Controller
    {

        private MainContext _context;

        public IdeaController(MainContext context)
        {
            _context = context;
        }

        [HttpPost]
        [HttpGet]
        [Route("addidea")]
        public IActionResult addidea(string IdeaText)
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGGEDPERSON" + loggedperson);
            if (loggedperson != null)
            {
                Idea newidea = new Idea
                {
                    IdeaText = IdeaText,
                    CreatedById = (int)loggedperson,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,

                };

                System.Console.WriteLine("NEWIDEAAA" + newidea);

                _context.Add(newidea);
                _context.SaveChanges();

                return RedirectToAction("LandingPage", "Home");

            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        [Route("users/{userdetails}")]
        public IActionResult userdetails(int userdetails)
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGGEDPERSON" + loggedperson);
            if (loggedperson != null)
            {

                List<Idea> ReturnedIdeas = _context.ideas
                    .Include(x => x.CreatedBy)
                    .Where(f => f.CreatedById == userdetails)
                    .ToList();

                @ViewBag.Ideas = ReturnedIdeas;


                Idea Ideas = _context.ideas
                    .Include(x => x.CreatedBy)
                    .Where(f => f.CreatedById == userdetails)
                    .FirstOrDefault();


                @ViewBag.random = Ideas;

                List<Like> Userslikes = _context.likes
                    .Where(r => r.UserId == userdetails)
                    .ToList();
                @ViewBag.Likelists = Userslikes;


                return View("OneUserDetails");

            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        [Route("bright_ideas/{IdeaId}")]


        public IActionResult usersperlike(int IdeaId)
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<Idea> UsersperLike = _context.ideas.Include(x => x.Likes).ThenInclude(z => z.User).Where(d => d.IdeaId == IdeaId).ToList();

                List<Like> Likedby = _context.likes.Where(i => i.IdeaId == IdeaId).Distinct().ToList();

                @ViewBag.Likedby = Likedby;
                var filteredlikes = UsersperLike.Distinct().ToList();
                @ViewBag.filteredlikes = filteredlikes;
                @ViewBag.UsersperLike = UsersperLike;
                return View("Likes");
            }
        }

        [HttpPost]
        [HttpGet]
        [Route("adduserlike/{IdeaId}")]
        public IActionResult adduserlike(int IdeaId)
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Like newlike = new Like
                {
                    IdeaId = (int)IdeaId,
                    UserId = (int)loggedperson
                };

                _context.Add(newlike);
                _context.SaveChanges();
                return RedirectToAction("LandingPage", "Home");
            }
        }


        [HttpGet]
        [Route("delete/{IdeaId}")]

        public IActionResult deleteIdea(int IdeaId)
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var ToDelete = _context.ideas.Include(w => w.Likes).ThenInclude(y => y.User).Where(d => d.IdeaId == IdeaId).SingleOrDefault();
                _context.ideas.Remove(ToDelete);
                _context.SaveChanges();
                return RedirectToAction("LandingPage", "Home");
            }
        }

    }
}