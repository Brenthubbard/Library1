using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GoodTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;


namespace GoodTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly GoodTreatsContext _db;

    public TreatsController(GoodTreatsContext db)
    {
      _db = db;
    }

    public async Task<IActionResult> Index(string searchTreat)
    {
      var treats = from m in _db.Treats
                   select m;

      if (!string.IsNullOrEmpty(searchTreat))
      {
        treats = treats.Where(s => s.Name.Contains(searchTreat));
      }

      return View(await treats.ToListAsync());
    }
    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]


    // [HttpPost]
    // public async Task<ActionResult> Create(Treat treat)
    // {
    //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(userId);
    //   treat.User = currentUser;
    //   if (ModelState.IsValid)
    //   {
    //     _db.Treats.Add(treat);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    //   }
    //   return View(treat);
    // }






    [HttpPost]
    public ActionResult Create(Flavor flavor, int flavorId)
    {

      _db.Flavors.Add(flavor);
      // _db.Flavors.Add(new Flavor() {FlavorId  = 5});
      _db.SaveChanges();
      if (flavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = flavorId, TreatId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int flavorId)
    {
      if (flavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = flavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // public async Task<IActionResult> Index(string searchTitle)
    // {
    //   var books = from m in _db.Flavor
    //               select m;

    //   if (!string.IsNullOrEmpty(searchTitle))
    //   {
    //     books = books.Where(s => s.Title.Contains(searchTitle));
    //   }

    //   return View(await books.ToListAsync());
    // }
  }
}

