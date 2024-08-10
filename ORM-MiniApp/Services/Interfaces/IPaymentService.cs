using ORM_MiniApp.Dtos.PaymentDtos;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IPaymentService
    {
        Task MakePayment(PaymentDto payment);
        Task<List<PaymentDto>> GetAllPayment();
    }
}
