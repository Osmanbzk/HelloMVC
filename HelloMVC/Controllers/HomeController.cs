using HelloMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace HelloMVC.Controllers
{
    public class HomeController : Controller
    {
        //Bilgisayar üzerinde veri cache etmek için kullanılıyor. Test için ideal.
        //System.Runtime.Cache referansı eklenmeli Referans kısmına
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;

            if (customers == null)
                customers = new List<Customer>();
        }
        public void SaveCache()
        {
            cache["customers"] = customers;
        }


        public PartialViewResult Basket()
        {
            BasketViewModel model = new BasketViewModel
            {
                BasketCount = 5,
                BasketTotal = "500 ₺"
            };

            return PartialView(model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Manuel data ile yapılan kodlama
        /*public ActionResult ViewCustomer(Customer postedCustomer)
           {
               Customer customer = new Customer();

               customer.Id = Guid.NewGuid().ToString();
               customer.Name = postedCustomer.Name;
               customer.Telephone = postedCustomer.Telephone;

               return View(customer);
           }*/
        #endregion


        public ActionResult ViewCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
                return View(customer);

            return HttpNotFound();
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("CustomerList");
        }

        public ActionResult EditCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
                return View(customer);

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer, string id)
        {
            var customerToEdit = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customerToEdit.Name = customer.Name;
                customerToEdit.Telephone = customer.Telephone;
                SaveCache();

                return RedirectToAction("CustomerList");
            }

            return HttpNotFound();
        }

        public ActionResult DeleteCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
                return View(customer);

            return HttpNotFound();
        }

        [ActionName("DeleteCustomer")]
        [HttpPost]
        public ActionResult ConfirmDeleteCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                customers.Remove(customer);
                return RedirectToAction("CustomerList");
            }

            return HttpNotFound();
        }

        public ActionResult CustomerList()
        {
            //List<Customer> customers = new List<Customer>();

            //customers.Add(new Customer { Name = "Ahmet", Telephone = "147852" });
            //customers.Add(new Customer { Name = "Cihat", Telephone = "852963" });
            //customers.Add(new Customer { Name = "Elif", Telephone = "963852" });

            return View(customers);
        }
    }
}