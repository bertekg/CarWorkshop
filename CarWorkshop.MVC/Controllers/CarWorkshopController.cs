﻿using AutoMapper;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers;

public class CarWorkshopController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CarWorkshopController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
        return View(carWorkshops);
    }

    [Route("CarWorkshop/{encodedName}/Details")]
    public async Task<IActionResult> Details(string encodedName)
    {
        var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));
        return View(dto);
    }

    [Route("CarWorkshop/{encodedName}/Edit")]
    public async Task<IActionResult> Edit(string encodedName)
    {
        var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedName));

        if(dto.IsEditable == false)
        {
            return RedirectToAction("NoAccess", "Home");
        }

        EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);

        return View(model);
    }

    [HttpPost]
    [Route("CarWorkshop/{encodedName}/Edit")]
    public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand command)
    {
        if (ModelState.IsValid == false)
        {
            return View(command);
        }
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Owner,Moderator,Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Owner,Moderator,Admin")]
    public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
    {
        if (ModelState.IsValid == false)
        {
            return View(command);
        }
        await _mediator.Send(command);

        this.SetNotification("success", $"Created carworkshop: {command.Name}");

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Owner")]
    [Route("CarWorkshop/CarWorkshopService")]
    public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }
        await _mediator.Send(command);

        return Ok();
    }
}
