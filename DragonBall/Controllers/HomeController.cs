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
        return RedirectToAction("Index_2");

        Character character = _api_DragonBall.ObtenerPersonaje(1);
        return View(character);
    }

    public async Task<IActionResult> Index_2(int page = 1, int itemsPerPage = 12, string search = "")
    {
        if (search != "")
        {
            List<Character> characterResponse = await _api_DragonBall.SearchCharacters(search);
            return View(characterResponse);
        }
        else
        {
            DragonBallResponse characterResponse = await _api_DragonBall.GetCharacterPage(page, itemsPerPage);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(characterResponse.Meta.TotalItems / (double)itemsPerPage);
            return View(characterResponse.Items);
        }
    }

    public IActionResult PeronsajeId(int personaje = 5)
    {
        Character character = _api_DragonBall.ObtenerPersonaje(personaje);
        return View(character);
    }





}
