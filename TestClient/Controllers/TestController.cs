using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestClient.Services;

namespace TestClient.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITest _test;
        public TestController(ILogger<TestController> logger,ITest test)
        {
            var r = test.Fun1();
            _logger = logger;
            _test = test;
            _logger.LogInformation("Test Controller");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}