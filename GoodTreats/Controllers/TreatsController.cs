using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GoodTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;



namespace GoodTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly TreatsContext _db;

    public AuthorsController(TreatsContext db)
    {
      _db = db;
    }

    // public ActionResult Index()
    // {
    //   List<Author> model = _db.Authors.ToList();
    //   return View(model);
    // }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treats treats, int flavorId)
    {
      _db.Flavors.Add(flavor);
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
    //   var books = from m in _db.Books
    //               select m;

    //   if (!string.IsNullOrEmpty(searchTitle))
    //   {
    //     books = books.Where(s => s.Title.Contains(searchTitle));
    //   }

    //   return View(await books.ToListAsync());
    // }
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
  }
}

