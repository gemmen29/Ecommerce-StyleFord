using BL.Bases;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        private DbContext EC_DbContext;
        public PaymentRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }

        #region CRUB

        public List<Payment> GetAllPayment()
        {
            return GetAll().ToList();
        }

        public bool InsertPayment(Payment payment)
        {
            return Insert(payment);
        }
        public void UpdatePayment(Payment payment)
        {
            Update(payment);
        }
        public void DeletePayment(int id)
        {
            Delete(id);
        }

        public bool CheckPaymentExists(Payment payment)
        {
            return GetAny(l => l.ID == payment.ID);
        }
        public Payment GetPaymentById(int id)
        {
            return GetFirstOrDefault(l => l.ID == id);
        }
        #endregion


    }
}
