using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class ChatRoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChatRoom
        public ActionResult Index()
        {
            var comments = db.Comments.Include(x => x.Replies).ToList();
            return View(comments); 
        }
    }
}