using afterlife_caretakers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class PaymentService
    {
        private Models.ALCDBContext _context;
        public PaymentService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddPayment(Payment newpayment)
        {
            DateTime date1 = DateTime.UtcNow;
            newpayment.TimeStamp = date1;
            if (PaymentExists(newpayment.Id))
            {
                return false;
            }
            _context.Add(newpayment);
            _context.SaveChanges();
            return true;
        }
        public List<Payment> GetAllPayments()
        {
            List<Payment> AllPayments = new List<Payment>();
            AllPayments = _context.Payment.ToList();
            return AllPayments;
        }
        public Payment GetPaymentById(int id)
        {
            Payment thePayment = _context.Payment.Where(c => c.Id == id).FirstOrDefault();
            return thePayment;
        }
        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(c => c.Id == id);
        }
    }
}
