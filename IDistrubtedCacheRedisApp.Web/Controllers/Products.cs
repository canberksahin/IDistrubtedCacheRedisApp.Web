using IDistrubtedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            Byte[] byteproduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distributedCache.Set("product:1", byteproduct);

            //await _distributedCache.SetStringAsync("product:1",jsonProduct,cacheEntryOptions);

            return View();
        }

        public async Task<IActionResult> Show()
        {

            //ViewBag.name = await _distributedCache.GetStringAsync("surname.netcore");

            string jsonProduct = _distributedCache.GetString("product:1");



            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.product = p.Name;


            return View();
        }

        public async Task<IActionResult> Delete()
        {
            await _distributedCache.RemoveAsync("surname.netcore");

            return View();
        }

        public IActionResult ImageCash()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images/unnamed.jpg");

            Byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("image", imageByte);
            return View();

        }

        public IActionResult ImageUrl()
        {
            byte[] path = _distributedCache.Get("image");

            return File(path,"image/jpeg");


        }
    }
}
