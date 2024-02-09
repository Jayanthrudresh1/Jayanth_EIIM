using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _Net_project.Models;

namespace _Net_project.Controllers;

public class UserController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
   
    public UserController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
       return RedirectToAction("LogIn","User");
    }
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpGet]
    public IActionResult LogIn()
    {
        return View();
    }

    public IActionResult BookingConfirm()
    {
         string username = HttpContext.Session.GetString("UserName");

    if (string.IsNullOrEmpty(username))
    {
        return RedirectToAction("LogIn");
    }
    var serviceBookings = _context.ServiceBooking.ToList();

            return View(serviceBookings);
    }


    [HttpPost]
    public IActionResult LogIn(SignupModel user)
    {
       Tuple<int, String> result = Repository.UserLogin(user);

    if (result.Item1 == 1)
    {
        HttpContext.Session.SetString("UserName", result.Item2);
        return RedirectToAction("UserBooking");
    }

        ViewBag.ErrorMessage = "Incorrect email or password";
        return View("LogIn");
    }

    [HttpGet]
    public IActionResult UserBooking()
{
    string username = HttpContext.Session.GetString("UserName");

    if (string.IsNullOrEmpty(username))
    {
        return RedirectToAction("LogIn");
    }
    
    return View();
}
    
    [HttpPost]
    public IActionResult SignUp(SignupModel user)
    {
          int check = Repository.UserSignup(user);

    if (check == 1)
    {
        return RedirectToAction("LogIn");
    }

        ViewBag.ErrorMessage = "Not signed In";
        return View("SignUp");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("LogIn","User");
    }


        [HttpPost]
        public IActionResult UserBooking(ServiceBookingModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var serviceBookingEntity = new ServiceBookingModel
                {
                    PatientName = viewModel.PatientName,
                    PatientAge = viewModel.PatientAge,
                    DoctorName = viewModel.DoctorName,
                    ServiceType = viewModel.ServiceType,
                    PreferredDate = viewModel.PreferredDate,
                    PreferredTime = viewModel.PreferredTime,
                   
                };

                // Add the entity to the DbContext and save changes to the database
                _context.ServiceBooking.Add(serviceBookingEntity);
                _context.SaveChanges();

                // Redirect to a different page after successful addition
                return RedirectToAction("BookingConfirm"); // Change "Index" and "Home" to your desired destination
            }

            // If ModelState is not valid, return to the same page or show an error message
            ViewBag.ErrorMessage = "Booking Has Not Done";
            return View();
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



