using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<DataTableResponse> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product(){
                    Name = "Product 1",
                    Color = "Red",
                    ListPrice = 100m,
                    ProductId = 1,
                    ProductNumber = "BCD-01"
                },
                new Product(){
                    Name = "Product 2",
                    Color = "Red",
                    ListPrice = 100m,
                    ProductId = 2,
                    ProductNumber = "BCD-02"
                },
                new Product(){
                    Name = "Product 3",
                    Color = "Red",
                    ListPrice = 100m,
                    ProductId = 3,
                    ProductNumber = "BCD-03"
                }
            };

            return new DataTableResponse
            {
                RecordsTotal = products.Count(),
                RecordsFiltered = 10,
                Data = products.ToArray()
            };
        }
    }
}