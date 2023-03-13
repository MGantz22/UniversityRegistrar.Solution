using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Linq;
using System.Collections.Generic;


namespace Registrar
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index() 
    {
      return View();
    }

  }
}
