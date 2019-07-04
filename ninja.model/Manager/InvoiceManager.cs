using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;
using ninja.model.Mock;

namespace ninja.model.Manager {

    public class InvoiceManager {

        private InvoiceMock _mock;

        public InvoiceManager() {

            this._mock = InvoiceMock.GetInstance();

        }

        public IList<Invoice> GetAll() {

            return this._mock.GetAll();

        }

        public Invoice GetById(long id) {

            return this._mock.GetById(id);

        }

        public void Insert(Invoice item) {

            this._mock.Insert(item);

        }
        public InvoiceDetail getInvoiceDetail(IList<InvoiceDetail> detail,long id)
        {
             for (int i = 0; i < detail.Count; i++)
                if (detail[i].Id == id)
                    return  detail[i];
            return null;
        }

        public void Update(Invoice invoice)
        {
            this._mock.Update(invoice);
        }
        public void Delete(long id) {

            Invoice invoice = this.GetById(id);
            this._mock.Delete(invoice);

        }

        public Boolean Exists(long id) {

            return this._mock.Exists(id);

        }
        public void DeleteDetail(long id,long idInvoice)
        {  try
            {
                Invoice invoice = GetById(idInvoice);
                invoice.DeleteDetailsById(getInvoiceDetail(invoice.GetDetail(), id));
                Update(invoice);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public long GetLastetId(IList<InvoiceDetail> listDetail)
        {   long mayor=-1;
            for (int i = 0; i < listDetail.Count; i++)
                if (listDetail[i].Id > mayor)
                    mayor = listDetail[i].Id;
            mayor = mayor + 1;
            return mayor;

        }
        public void UpdateDetail(long id, IList<InvoiceDetail> detail) {

            /*
              Este método tiene que reemplazar todos los items del detalle de la factura
              por los recibidos por parámetro
             */

            #region Escribir el código dentro de este bloque
            Invoice invoice = GetById(id);
            invoice.UpdateDetail(detail);
            Update(invoice);
            #endregion Escribir el código dentro de este bloque

        }

    }

}