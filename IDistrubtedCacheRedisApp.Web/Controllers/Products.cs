using IDistrubtedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDistrubtedCacheRedisApp.Web.Controllers
{
    public class Products : Controller
    {
        private IDistributedCache _distributedCache;

        public Products(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            //await _distributedCache.SetStringAsync("surname.netcore", "Denemeee",cacheEntryOptions);

            Product product = new Product { Id=1,Name="Product1",Price=9.99};
            string jsonProduct = JsonConvert.SerializeObject(product);
            await _distributedCache.SetStringAsync("product:1",jsonProduct,cacheEntryOptions);

            return View();
        }

        public async Task<IActionResult> Show()
        {
            
            ViewBag.name = await _distributedCache.GetStringAsync("surname.netcore");
            return View();
        }

        public async Task<IActionResult> Delete()
        {
            await _distributedCache.RemoveAsync("surname.netcore");

            return View();
        }
    }
}
