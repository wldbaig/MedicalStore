using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    public class InvoiceController : BaseController
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
    }
}