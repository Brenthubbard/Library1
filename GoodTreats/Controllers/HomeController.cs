using Microsoft.AspNetCore.Mvc;
using GoodTreats.Models;


using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


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