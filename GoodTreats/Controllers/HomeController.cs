using Microsoft.AspNetCore.Mvc;
using GoodTreats.Models;

namespace GoodTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly TreatsContext _db;
    public HomeController(TreatsContext db)
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