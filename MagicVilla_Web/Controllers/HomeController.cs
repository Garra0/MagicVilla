using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Web.Models;
using AutoMapper;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using MagicVilla_Web.models.Dto;
using MagicVilla_Web.models;
using MagicVilla_Utility;

namespace MagicVilla_Web.Controllers;

public class HomeController : Controller
{
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;
    public HomeController(IVillaService villaService, IMapper mapper)
    {
        _villaService = villaService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        List<VillaDTO> list = new();
        // "httpAcc.HttpContext.Session.GetString(SD.SessionToken)"
        // the above line i copy it from laYout.cshtml line when i used if state and then paste it here...
        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
        }
        return View(list);
    }

}

