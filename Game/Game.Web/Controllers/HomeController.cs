using Akka.Actor;
using Akka.Hosting;
using Game.ActorModel.Actors;
using Game.ActorModel.Messages;
using Game.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Game.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActorRef _actor;

        public HomeController(ILogger<HomeController> logger, IRequiredActor<EchoActor> echoActor)
        {
            _logger = logger;
            _actor = echoActor.ActorRef;
        }

        public IActionResult Index()
        {
            _actor.Tell(new LoggingMessage("Hello Index..."));
            return View();
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
}