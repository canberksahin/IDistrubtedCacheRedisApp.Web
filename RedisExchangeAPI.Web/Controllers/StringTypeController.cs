using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDb(0);

            db.StringSet("name", "Canberk Şahin");
            db.StringSet("ziyaretci",100);

            ViewBag.name = db.StringGet("name");

            return View();
        }
    }
}
