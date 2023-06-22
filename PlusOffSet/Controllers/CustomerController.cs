using Microsoft.AspNetCore.Mvc;
using PlusOffSet.DAL;
using PlusOffSet.Models;

namespace PlusOffSet.Controllers
{
    public class CustomerController : Controller
    {
        // private readonly CustomerDAL _dAL;
        CustomerDAL _customerDAL = new CustomerDAL();

       /* public CustomerController(CustomerDAL _customerDAL)
        {
            _customerDAL = CustomerDAL;
        }*/
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var customers = _customerDAL.GetAllcustomer();

                if (customers.Count == 0)
                {
                    TempData["InfoMessage"] = "Currently customer not available in the Database.";
                }
                return View(customers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(customer cm)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }


        // GET: Customer/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(customer customer)
        {
            try
            {
                int id = 0;

                if (ModelState.IsValid)
                {
                    id = _customerDAL.Insertcustomer(customer);

                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Customer details saved successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to insert the customer";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        //POST: Customer/Edit/
        public ActionResult Edit(int Id)
        {
            try
            {
                var customerList = _customerDAL.GetcustomerID(Id).FirstOrDefault();

                if (customerList == null)
                {
                    TempData["ErrorMessage"] = "Customer details not available with the customer Id : " + Id;
                    return RedirectToAction("Index");
                }
                return View(customerList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Customer/Edit/
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateCustomer(customer customer)
        {
            try
            {
                int Id = 0;

                if (ModelState.IsValid)
                {
                    Id = _customerDAL.UpdateCustomer(customer);

                    if (Id > 0)
                    {
                        TempData["SuccessMessage"] = "Customer details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the customer.";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var customerList = _customerDAL.GetcustomerID(id).FirstOrDefault();

                if (customerList == null)
                {
                    TempData["ErrorMessage"] = "Customer details not available with the customer Id : " + id;
                    return RedirectToAction("Index");
                }
                return View(customerList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                int customerid = 0;

                if (ModelState.IsValid)
                {
                    customerid = _customerDAL.DeleteCustomer(id);

                    if (customerid > 0)
                    {
                        TempData["SuccessMessage"] = "Customer deleted successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Customer to delete the customer";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
