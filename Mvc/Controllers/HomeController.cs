using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using EntityFramework.DynamicLinq;
using System.Dynamic;
using System.Data.Entity;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly OracleDbContext _oracleDbContext;

        public HomeController(OracleDbContext oracleDbContext)
        {
            _oracleDbContext = oracleDbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].ToString();
                // Skiping number of Rows count
                var start = Request.Form["start"].ToString();
                // Paging Length 10,20
                var length = Request.Form["length"].ToString();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
                // Sort Column Direction (asc, desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].ToString();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].ToString();
                // Paging Size (10,20,50,100)
                int pageSize = Convert.ToInt32(length);
                int skip = Convert.ToInt32(start);
                int recordsTotal = 0;
                // Getting all Customer data
                var customerData = (from c in _oracleDbContext.Customers
                                    select c);
                // Sorting
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                // Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name == searchValue || m.PhoneNumber == searchValue || m.City == searchValue);
                }
                // Total number of rows count 
                recordsTotal = customerData.Count();
                // Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                // Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("ShowGrid", "DemoGrid");
                }

                return View("Edit");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("ShowGrid", "DemoGrid");
                }

                int result = 0;

                if (result > 0)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}