using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FPTBook.Models;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers;

public class HomeController : Controller
{
    private readonly FptbookContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    public IActionResult Index(int page = 1, int pageSize = 4)
    {
        var fptbookContext = _context.Books.Include(b => b.Cat).Skip((page - 1) * pageSize).Take(pageSize);
        ViewBag.TotalPage = Math.Ceiling((double)_context.Books.Count() / pageSize);
        ViewBag.Page = page;
        return View(fptbookContext.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
