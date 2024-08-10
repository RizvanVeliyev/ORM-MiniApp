using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.PaymentDtos;
using ORM_MiniApp.Models;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    internal class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        public PaymentService()
        {
            _context = new AppDbContext();
        }
        public async Task<List<PaymentDto>> GetAllPayment()
        {
            var payments = await _context.Payments.AsNoTracking().ToListAsync();
            var paymentDtos = payments.Select(payment => new PaymentDto()
            {
                OrderId=payment.OrderId,
                Amount=payment.Amount

            }).ToList();
            return paymentDtos;
        }

        public async Task MakePayment(PaymentDto payment)
        {
            var paymentDto = new Payment()
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentDate = DateTime.UtcNow
            };
            await _context.Payments.AddAsync(paymentDto);
            await _context.SaveChangesAsync();
        }
    }
}
