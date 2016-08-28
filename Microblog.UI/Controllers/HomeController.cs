namespace Microblog.UI.Controllers
{
    using System.Web.Mvc;    
    using Incoding.MvcContrib;

    public class HomeController : IncControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignInPartial()
        {
            return View();
        }
        public ActionResult SignUpPartial()
        {
            return View();
        }

        public ActionResult RequestForFriendship()
        {
            return View();
        }
    }
}