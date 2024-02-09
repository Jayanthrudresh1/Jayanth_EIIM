using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _Net_project.Models;

namespace _Net_project.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;

    public AdminController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
       return RedirectToAction("LogIn","Admin");
    }

    [HttpGet]
    public IActionResult LogIn()
    {
        return View();
    }


    [HttpPost]
    public IActionResult LogIn(LoginModel admin)
    {
        int check = Repository.AdminLogin(admin);

    if (check == 1)
    {
        HttpContext.Session.SetString("Admin", "Admin");
        return RedirectToAction("Welcome");
    }

        ViewBag.ErrorMessage = "Incorrect email or password";
        return View("LogIn");
    }

        public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("LogIn","Admin");
    }

     public IActionResult Welcome()
    {
        
        string username = HttpContext.Session.GetString("Admin");
        if (username=="Admin"){
                return View();
        }
        return  RedirectToAction("LogIn");
    }

     public IActionResult LoginFailed()
    {
        return View();
    }

    public IActionResult BookingDetails()
    {
        var serviceBookings = _context.ServiceBooking.ToList();
        string username = HttpContext.Session.GetString("Admin");
        if (username=="Admin"){
                return View(serviceBookings);
        }
        return  RedirectToAction("LogIn");
    }



    
    public IActionResult UserDetails()
    {
         List<SignupModel> userList = Repository.GetUserDetails();
        string username = HttpContext.Session.GetString("Admin");
        if (username=="Admin"){
                return View(userList);
        }
        return  RedirectToAction("LogIn");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



