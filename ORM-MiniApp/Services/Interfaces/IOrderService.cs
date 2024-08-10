using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IOrderService
    {
        //        CreateOrder: Yeni sifariş yaratmaq. (Sifariş yarananda statusu pending olacaq). 

        //Əgər sifariş məbləği sıfırdan azdırsa və ya istifadəçi tapılmazsa, InvalidOrderException qaytarılmalıdır.

        Task CreateOrder(OrderDto order);
        //CancelOrder: Mövcud sifarişi ləğv etmək.
        Task CancelOrder(int id);

        //Əgər ləğv edilmək istənilən sifariş tapılmazsa və ya artıq ləğv edilmişsə, NotFoundException və ya OrderAlreadyCancelledException fırladılmalıdır.
        //CompleteOrder:  Mövcud sifarişi bitirmək. (Sifarişin statusu compeleted olacaq).
        Task CompleteOrder(int id);


        //Əgər bitirmək istənilən sifariş tapılmazsa və ya artıq bitmişsə edilmişsə, NotFoundException və ya OrderAlreadyCompletedException fırladılmalıdır.
        //GetOrders: Bütün sifarişləri siyahı şəklində qaytarmaq.
        //AddOrderDetail: Yeni sifariş detalını əlavə etmək.
        Task<List<Order>> GetOrders();
        Task AddOrderDetail(int id);

        //Əgər əlavə edilən məhsulun miqdarı sıfırdan azdırsa və ya məhsulun qiyməti mənfi olsa

        //InvalidOrderDetailException. 

        //Əgər əlavə edilən sifariş detalları mövcud olmayan bir sifarişə aid edilməyə çalışılırsa

        //NotFoundException.

        //Əgər əlavə edilən məhsul sistemdə mövcud deyilsə

        //NotFoundException.
    }
}
