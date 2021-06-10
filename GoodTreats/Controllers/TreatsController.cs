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


namespace GoodTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly GoodTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, GoodTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<IActionResult> Index(string searchName)
    {
      var flavor = from m in _db.Treats
                   select m;

      if (!string.IsNullOrEmpty(searchName))
      {
        flavor = flavor.Where(s => s.Name.Contains(searchName));
      }

      return View(await flavor.ToListAsync());
    }

    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name", "Treats");
      return View();
    }

    // [HttpPost]
    // public async Task<ActionResult> Create(Treat treat)
    // {
    //   var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   var currentUser = await _userManager.FindByIdAsync(userId);
    //   treat.User = currentUser;
    //   _db.Treats.Add(treat);
    //   _db.SaveChanges();
    //   if (ModelState.IsValid)
    //   {
    //     _db.Treats.Add(treat);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    //   }
    //   return View(treat);
    // }



    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int flavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (flavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = flavorId, TreatId = treat.TreatId });
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

      return RedirectToAction("Details", new { id = treat.TreatId });
    }


    public ActionResult AddFlavor(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId });
    }
    // [HttpPost]
    // public ActionResult AddTreat(Flavor flavor, int TreatId)
    // {
    //   if (TreatId != 0)
    //   {
    //     _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });
    //   }
    //   _db.SaveChanges();
    //   return RedirectToAction("Details", new { id = flavor.FlavorId });
    // }



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
    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.TreatId });
    }
  }
}

