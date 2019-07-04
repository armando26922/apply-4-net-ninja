using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ninja.Models;
using ninja.model.Entity;
using ninja.model.Manager;

namespace ninja.Controllers
{
    public class InvoicesController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        InvoiceManager invoices = new InvoiceManager();
        public ActionResult Index()
        {
            return View(invoices.GetAll());
        } 

        // GET: Invoices/Details/5
        public ActionResult Details(long id)
        {
 
            Invoice invoice=invoices.GetById(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {   
                invoices.Insert(invoice);
                return RedirectToAction("Index");
            }

            return View(invoice);
        }
        public ActionResult CreateDetail([Bind(Include = "Id,Type")] Invoice invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                invoices.Insert(invoiceDetail);
                return RedirectToAction("Index");
            }

            return View(invoiceDetail);
        }
        public ActionResult EdiInvoiceDetail([Bind(Include = "Id,Type")] Invoice invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                invoices.Insert(invoiceDetail);
                return RedirectToAction("Index");
            }

            return View(invoiceDetail);
        }
        public ActionResult DeleteInvoiceDetail(long id)
        {
            Invoice invoice = invoices.GetById(id);
            return View(invoice);
        }
        public ActionResult DeleteConfirmedInvoiceDetail(long id)
        {
            invoices.Delete(id);
            return RedirectToAction("Index");
        }


        // GET: Invoices/Edit/5
        public ActionResult Edit(long id)
        {
           
            Invoice invoice =invoices.GetById(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoices.Update(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(long id)
        {
            Invoice invoice=invoices.GetById(id);
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            invoices.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
