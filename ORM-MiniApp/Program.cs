using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Dtos.PaymentDtos;
using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Implementations;
using ORM_MiniApp.Repositories.Interfaces;
using ORM_MiniApp.Services.Implementations;
using ORM_MiniApp.Services.Interfaces;



IProductService productService = new ProductService();
IUserService userService = new UserService();
IOrderService orderService = new OrderService();
IPaymentService paymentService = new PaymentService();
Console.WriteLine("Welcome!");
while (true)
{
UserMenu:
    Console.WriteLine("-----------------------------------------------------------------");
    Console.WriteLine("1.Register");
    Console.WriteLine("2.Login");
    Console.WriteLine("3.Update User informations");
    Console.WriteLine("4.Get UserOrders");
    Console.WriteLine("5.Export Xml");
    Console.WriteLine("6.Show User Service menu");
    Console.WriteLine("0.Finish Program");
    while (true)
    {
        int commandU = InputPositiveInt("Select Command:");

        switch (commandU)
        {
            case 1:

                try
                {
                    UserRegDto user = new UserRegDto();
                    Console.Write("Enter the User name:");
                    user.FullName = Console.ReadLine();
                    Console.Write("Enter Email:");
                    user.Email = Console.ReadLine();
                    Console.Write("Enter Password:");
                    user.Password = Console.ReadLine();
                    Console.Write("Enter the Adress:");
                    user.Address = Console.ReadLine();
                    await userService.RegisterUser(user);
                }
                catch (InvalidUserInformationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(SameEmailException e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 2:
                try
                {
                    UserLoginDto userLogin = new UserLoginDto();
                    Console.Write("Enter the User name:");
                    userLogin.FullName = Console.ReadLine();
                    Console.Write("Enter Password:");
                    userLogin.Password = Console.ReadLine();
                    await userService.Login(userLogin);
                    goto ServicesMenu;
                }
                catch (UserAuthenticationException e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 3:
                try
                {
                    AppDbContext context = new AppDbContext();
                    int userId = InputPositiveInt("Enter Id for Update:");
                    var userUp = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
                    Console.Write("Enter the new User name:");
                    userUp.FullName = Console.ReadLine();
                    Console.Write("Enter new Email:");
                    userUp.Email = Console.ReadLine();
                    Console.Write("Enter new Password:");
                    userUp.Password = Console.ReadLine();
                    Console.Write("Enter new Adress:");
                    userUp.Address = Console.ReadLine();
                    userService.Update(userUp);
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                break;
            case 4:
                try
                {
                    int IdUserOrder = InputPositiveInt("Enter Id for User Orders:");
                    var userOrders = await userService.GetUserOrders(IdUserOrder);
                    foreach (var order in userOrders)
                    {
                        Console.WriteLine($"User Name:{order.User.FullName} TotalAmount:{order.TotalAmount} Status:{order.Status}");
                    }
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
            case 5:

                break;
            case 6:
                goto UserMenu;
            case 0:
                Console.WriteLine("Program Finished!");
                return;
            default:
                goto UserMenu;


        }
    }
ServicesMenu:

    Console.WriteLine("-------------------------------------------------------------");
    Console.WriteLine("1.Product Service");
    Console.WriteLine("2.Order Service");
    Console.WriteLine("3.Payment Service");
    Console.WriteLine("4.Show Services");
    Console.WriteLine("0.LogOut Account");

    while (true)
    {
        int command = InputPositiveInt("Select Service:");

        switch (command)
        {
            case 1:
            ProductMenu:
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Welcome to the Product Service");
                Console.WriteLine("1.Add Product");
                Console.WriteLine("2.Update Product");
                Console.WriteLine("3.Get all products");
                Console.WriteLine("4.Get Product By Id");
                Console.WriteLine("5.Delete Product");
                Console.WriteLine("6.Search Products By Name");
                Console.WriteLine("7.Show Product Service Menu");
                Console.WriteLine("0.Exit Porduct Service");
                while (true)
                {
                    int commandP = InputPositiveInt("Select command:");

                    switch (commandP)
                    {
                        case 1:
                            ProductPostDto product = new ProductPostDto();
                            Console.Write("Enter the product name:");
                            product.Name = Console.ReadLine();
                            product.Price = InputDecimalPositive("Enter Price:");
                            product.Stock = InputPositiveInt("Enter Stock Count:");
                            Console.Write("Enter the description:");
                            product.Description = Console.ReadLine();
                            await productService.AddProductAsync(product);
                            break;
                        case 2:
                            try
                            {
                                int id = InputPositiveInt("Enter the Product Id for Update:");
                                var productUp = await productService.GetProductByIdAsync(id);
                                Console.Write("Enter the new product name:");
                                productUp.Name = Console.ReadLine();
                                productUp.Price = InputDecimalPositive("Enter new Price:");
                                productUp.Stock = InputPositiveInt("Enter new Stock Count:");
                                Console.Write("Enter the new description:");
                                productUp.Description = Console.ReadLine();
                                await productService.UpdateProductAsync(productUp);
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 3:
                            List<ProductGetDto> products = await productService.GetProductsAsync();
                            foreach (var item in products)
                            {
                                Console.WriteLine($"Id:{item.Id} Name:{item.Name} Price:{item.Price} " +
                                    $"Stock:{item.Stock} Description:{item.Description}");
                            }
                            break;
                        case 4:
                            try
                            {
                                int prId = InputPositiveInt("Enter the Product Id:");

                                ProductGetDto productGetById = await productService.GetProductByIdAsync(prId);
                                Console.WriteLine($"Id:{productGetById.Id} Name:{productGetById.Name} Price:{productGetById.Price} " +
                                        $"Stock:{productGetById.Stock} Description:{productGetById.Description}");
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }


                            break;
                        case 5:
                            try
                            {
                                int prId = InputPositiveInt("Enter the Product Id:");

                                await productService.DeleteProductAsync(prId);
                                Console.WriteLine("Product Removed!");
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 6:

                            try
                            {
                                Console.WriteLine("Enter the name which do you seacrh:");
                                string name = Console.ReadLine();
                                var productSearch = await productService.SearchProducts(name);
                                foreach (var item in productSearch)
                                {
                                    Console.WriteLine($"Id:{item.Id} Name:{item.Name} Price:{item.Price} " +
                                        $"Stock:{item.Stock} Description:{item.Description}");
                                }

                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 7:
                            goto ProductMenu;
                        case 0:
                            goto ServicesMenu;
                        default:
                            goto ProductMenu;

                    }
                }

            case 2:
            OrderMenu:
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Welcome to the Order Service");
                Console.WriteLine("1.Create Order");
                Console.WriteLine("2.Cancel Order");
                Console.WriteLine("3.Complete Order");
                Console.WriteLine("4.Get Orders");
                Console.WriteLine("5.Add OrderDetail");
                Console.WriteLine("6.Show Order Service menu");
                Console.WriteLine("0.Exit Order Service");
                while (true)
                {
                    int commandO = InputPositiveInt("Select command:");
                    switch (commandO)
                    {
                        case 1:

                            try
                            {
                                OrderDto order = new OrderDto();
                                order.UserId = InputPositiveInt("Enter the User Id:");
                                order.TotalAmount = InputDecimalPositive("Enter the Total Amount:");
                                await orderService.CreateOrder(order);
                            }
                            catch (InvalidUserInformationException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                int cancelId = InputPositiveInt("Enter Order Id for cancel:");
                                await orderService.CancelOrder(cancelId);
                            }
                            catch (OrderAlreadyCancelledException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                int completeId = InputPositiveInt("Enter Order Id for Complete:");
                                await orderService.CompleteOrder(completeId);
                            }
                            catch (OrderAlreadyCompletedException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;


                        case 4:
                            var orders = await orderService.GetOrders();
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Order id:{order.Id}  Order's User:{order.User.FullName} Total Amount:{order.TotalAmount} Status:{order.Status}");
                            }
                            break;
                        case 5:
                            try
                            {
                                OrderDetail Od = new OrderDetail();
                                Od.OrderId = InputPositiveInt("Enter the order Id:");
                                Od.ProductId = InputPositiveInt("Enter the product Id:");
                                Od.Quantity = InputPositiveInt("Enter the quantity");
                                Od.PricePerItem = InputDecimalPositive("Enter price for per item:");
                                await orderService.AddOrderDetail(Od);
                            }
                            catch(InvalidOrderDetailException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (NotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }




                            break;
                        case 6:
                            goto OrderMenu;
                        case 0:
                            goto ServicesMenu;
                        default:
                            goto OrderMenu;


                    }
                }
            case 3:
            PaymentMenu:
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Welcome to the Payment Service");
                Console.WriteLine("1.Make Payment");
                Console.WriteLine("2.Get All Payments");
                Console.WriteLine("3.Show Payment Service menu");
                Console.WriteLine("0.Exit Order Service");
                while (true)
                {
                    int commandPy = InputPositiveInt("Select command:");
                    switch (commandPy)
                    {
                        case 1:
                            PaymentDto payment = new PaymentDto();
                            payment.OrderId = InputPositiveInt("Enter the Order Id:");
                            payment.Amount = InputDecimalPositive("Enter the Total Amount:");
                            await paymentService.MakePayment(payment);

                            break;
                        case 2:
                            var payments = await paymentService.GetAllPayment();
                            foreach (var p in payments)
                            {
                                Console.WriteLine($"Payment Amouunt{p.Amount} Order Id:{p.OrderId}");
                            }
                            break;
                        case 3:
                            goto PaymentMenu;
                        case 0:
                            goto ServicesMenu;
                        default:
                            goto PaymentMenu;


                    }
                }

            case 0:
                Console.WriteLine("You logout account!");
                goto UserMenu;
            default:
                goto ServicesMenu;
        }

    }
}

static int InputPositiveInt(string prompt)
{
    int result;
    Console.Write(prompt);

    while (!int.TryParse(Console.ReadLine(), out result) || result < 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive integer.");
        Console.Write(prompt);
    }

    return result;
}
static decimal InputDecimalPositive(string prompt)
{
    decimal result;
    Console.Write(prompt);
    while (!decimal.TryParse(Console.ReadLine(), out result) || result <= 0)
    {
        Console.WriteLine("Invalid input. Please enter a positive decimal.");
        Console.Write(prompt);
    }
    return result;
}