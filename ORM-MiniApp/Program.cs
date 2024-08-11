using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Dtos.PaymentDtos;
using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Exceptions;
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
        int commandU;
        Console.Write("Select command:");
        commandU = int.Parse(Console.ReadLine());
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
                    Console.WriteLine("Enter Id for Update:");
                    int userId = int.Parse(Console.ReadLine());
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
                    Console.Write("Enter id for User Orders:");
                    int IdUserOrder = int.Parse(Console.ReadLine());
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
    Console.WriteLine("You Login Successfully!");
    Console.WriteLine("-------------------------------------------------------------");
    Console.WriteLine("1.Product Service");
    Console.WriteLine("2.Order Service");
    Console.WriteLine("3.Payment Service");
    Console.WriteLine("4.Show Services");
    Console.WriteLine("0.LogOut Account");

    while (true)
    {
        int command;
        Console.Write("Select Service:");
        command = int.Parse(Console.ReadLine());
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
                    int commandP;
                    Console.Write("Select command:");
                    commandP = int.Parse(Console.ReadLine());
                    switch (commandP)
                    {
                        case 1:
                            ProductPostDto product = new ProductPostDto();
                            Console.Write("Enter the product name:");
                            product.Name = Console.ReadLine();
                            Console.Write("Enter Price:");
                            product.Price = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter Stock Count:");
                            product.Stock = int.Parse(Console.ReadLine());
                            Console.Write("Enter the description:");
                            product.Description = Console.ReadLine();
                            await productService.AddProductAsync(product);
                            break;
                        case 2:
                            try
                            {
                                Console.Write("Enter the Product Id for Update:");
                                int id = int.Parse(Console.ReadLine());
                                var productUp = await productService.GetProductByIdAsync(id);
                                Console.Write("Enter the new product name:");
                                productUp.Name = Console.ReadLine();
                                Console.Write("Enter new Price:");
                                productUp.Price = decimal.Parse(Console.ReadLine());
                                Console.Write("Enter new Stock Count:");
                                productUp.Stock = int.Parse(Console.ReadLine());
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
                                Console.Write("Enter the Product Id:");
                                int prId = int.Parse(Console.ReadLine());

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
                                Console.Write("Enter the Product Id:");
                                int prId = int.Parse(Console.ReadLine());

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
                    int commandO;
                    Console.Write("Select command:");
                    commandO = int.Parse(Console.ReadLine());
                    switch (commandO)
                    {
                        case 1:

                            try
                            {
                                OrderDto order = new OrderDto();
                                Console.Write("Enter the User Id:");
                                order.UserId = int.Parse(Console.ReadLine());
                                Console.Write("Enter the Total Amount:");
                                order.TotalAmount = int.Parse(Console.ReadLine());
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
                                Console.Write("Enter Order Id for cancel:");
                                int cancelId = int.Parse(Console.ReadLine());
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
                                Console.Write("Enter Order Id for Complete:");
                                int completeId = int.Parse(Console.ReadLine());
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
                                Console.WriteLine($"Order's User:{order.User.FullName} Total Amount:{order.TotalAmount} Status:{order.Status}");
                            }
                            break;
                        case 5:

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
                    int commandPy;
                    Console.Write("Select command:");
                    commandPy = int.Parse(Console.ReadLine());
                    switch (commandPy)
                    {
                        case 1:
                            PaymentDto payment = new PaymentDto();
                            Console.Write("Enter the Order Id:");
                            payment.OrderId = int.Parse(Console.ReadLine());
                            Console.Write("Enter the Total Amount:");
                            payment.Amount = decimal.Parse(Console.ReadLine());
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