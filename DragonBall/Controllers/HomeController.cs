using Api_DragonBall.Controllers;
using Class.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DragonBallController _api_DragonBall;

    public HomeController(ILogger<HomeController> logger, DragonBallController api_DragonBall)
    {
        _logger = logger;
        _api_DragonBall = api_DragonBall ?? throw new ArgumentNullException(nameof(api_DragonBall));
    }

    public IActionResult Index()
    {
        Character character = _api_DragonBall.ObtenerPersonaje(1);
        return View(character);
    }

}
