using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ChatRoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChatRoom
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId == 0)
            {
                RedirectToAction("Login", "Account");
            }
            var comments = db.Comments.Include(x => x.Replies).OrderByDescending(x => x.CreatedOn).ToList();
            return View(comments); 
        }

        // Post: ChatRoom/PostReply
        [HttpPost]
        public ActionResult PostReply(ReplyVM obj)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if(userId == 0)
            {
                RedirectToAction("Login", "Account");
            }
            Reply r = new Reply();
            r.Text = obj.Reply;
            r.CommentId = obj.CID;
            r.UserId = userId;
            r.CreatedOn = DateTime.Now;
            db.Replies.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Post: ChatRoom/CommentReply
        [HttpPost]
        public ActionResult PostComment(string CommentText)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId == 0)
            {
                RedirectToAction("Login", "Account");
            }
            Comment c = new Comment();
            c.Text = CommentText;
            c.CreatedOn = DateTime.Now;
            c.UserId = userId;
            db.Comments.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}