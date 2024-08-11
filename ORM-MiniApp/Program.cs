using DocumentFormat.OpenXml.Wordprocessing;
using ORM_MiniApp.Dtos.ProductDtos;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Services.Implementations;
using ORM_MiniApp.Services.Interfaces;

IProductService productService=new ProductService();
IUserService userService=new UserService();
IOrderService orderService=new OrderService();
IPaymentService paymentService=new PaymentService();
Console.WriteLine("Welcome!");
Exit:
Console.WriteLine("-------------------------------------------------------------");
Console.WriteLine("1.Product Service");
Console.WriteLine("2.User Service");
Console.WriteLine("3.Order Service");
Console.WriteLine("4.Payment Service");
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
            Console.WriteLine("7.Show Product Service menu");
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
                        product.Price=decimal.Parse(Console.ReadLine());
                        Console.Write("Enter Stock Count:");
                        product.Stock=int.Parse(Console.ReadLine());
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
                        catch(NotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:
                        List <ProductGetDto> products= await productService.GetProductsAsync();
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

                            await productService.DeleteProductAsync (prId);
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
                        goto Exit;
                        
                }
            }
        case 2:
        UserMenu:
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Welcome to the User Service");
            Console.WriteLine("1.Register");
            Console.WriteLine("2.Login");
            Console.WriteLine("3.Update User informations");
            Console.WriteLine("4.Get Users");
            Console.WriteLine("5.Export Xml");
            Console.WriteLine("6.Show User Service menu");
            Console.WriteLine("0.Exit User Service");
            while (true)
            {
                int commandU;
                Console.Write("Select command:");
                commandU = int.Parse(Console.ReadLine());
                switch (commandU)
                {
                    case 1:
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
                        goto UserMenu;
                    case 0:
                        goto Exit;


                }
            }
            break;
        case 3:
            break;
        case 4:
            break;
        case 5:
            break;
        default:
            break;
    }

}