using Microsoft.AspNetCore.Mvc;
using GoodTreats.Models;

namespace GoodTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly GoodTreatsContext _db;
    public HomeController(GoodTreatsContext db)
    {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index()

    {
      return View();
    }
  }
}