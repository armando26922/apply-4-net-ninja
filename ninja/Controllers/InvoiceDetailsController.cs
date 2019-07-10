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
    public class InvoiceDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        InvoiceManager invoices = new InvoiceManager();

        // GET: InvoiceDetails
        public ActionResult Index(long id)
        {
            ViewBag.InvoicesDetails = invoices.GetById(id).GetDetail();
            return View();
        }

        // GET: InvoiceDetails/Details/5
        public ActionResult Details(long id,long idInvoice)
        {
            Invoice invoice = invoices.GetById(idInvoice);
            InvoiceDetail invoiceDetail = invoice.GetDetailByID(id);
            ViewBag.InvoiceDetail = invoiceDetail;
            return View();
        }

        // GET: InvoiceDetails/Create
        public ActionResult Create(long idInvoice)
        {   
            
            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Id= invoices.GetLastetId(invoices.GetById(idInvoice).GetDetail());
            invoiceDetail.InvoiceId = idInvoice;
            ViewBag.InvoiceDetail = invoiceDetail;
            return View();
        }

        // POST: InvoiceDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvoiceId,Description,Amount,UnitPrice")] InvoiceDetail invoiceDetail)
        {
            Invoice invoice = null;
            if (ModelState.IsValid)
            {
                invoice = invoices.GetById(invoiceDetail.InvoiceId);
                invoice.AddDetail(invoiceDetail);
                invoices.Update(invoice);
                return RedirectToAction("Index","Invoices");
            }

            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Edit/5
        public ActionResult Edit(long id,long idInvoice)
        {
            Invoice invoice = invoices.GetById(idInvoice);
            InvoiceDetail invoiceDetail = invoices.getInvoiceDetail(invoice.GetDetail(), id);
            ViewBag.InvoiceDetail = invoiceDetail;
            return View();
        }

        // POST: InvoiceDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceId,Description,Amount,UnitPrice")] InvoiceDetail invoiceDetail)
        { Invoice invoice = null;
            if (ModelState.IsValid)
            {   invoice = invoices.GetById(invoiceDetail.InvoiceId);
                invoices.UpdateDetailInvoices(invoiceDetail, invoice);
                return RedirectToAction("Index","Invoices");
            }
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Delete/5
        public ActionResult Delete(long id,long idInvoice)
        {

            Invoice invoice = invoices.GetById(idInvoice);
            InvoiceDetail invoiceDetail = invoice.GetDetailByID(id);
            ViewBag.InvoiceDetail = invoiceDetail;
            return View();
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id,long idInvoice)
        {
            invoices.DeleteDetail(id, idInvoice);
            return RedirectToAction("Index","Invoices");
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
