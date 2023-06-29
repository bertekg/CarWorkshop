using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers;

public class CarWorkshopController : Controller
{
    private readonly ICarWorkshopService _carWorkshopService;

    public CarWorkshopController(ICarWorkshopService carWorkshopService)
    {
        _carWorkshopService = carWorkshopService;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarWorkshopDto carWorkhop)
    {
        await _carWorkshopService.Create(carWorkhop);
        return RedirectToAction(nameof(Create)); // TODO: refactor
    }
}
