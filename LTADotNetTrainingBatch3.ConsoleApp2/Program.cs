// See https://aka.ms/new-console-template for more information
using LTADotNetTrainingBatch3.ConsoleApp2;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;


char i = 'Y';
while (i == 'Y')
{
    Console.WriteLine("Select which database service you want to choose \n Press 1 for ADO service \n Press 2 for DapperService \n Press 3 for EFCore Service");

    int serviceOption = Convert.ToInt32(Console.ReadLine());
    DatabaseService service = (DatabaseService)serviceOption;
    int result = WelcomeMessage();
    Option option = (Option)result;
    switch (service)
    {
        case DatabaseService.ADOService:

            var ADOService = new ADOService();
         
            switch (option)
            {

                case Option.CheckProduct:
                    
                    ADOService.ReadProduct();
                    break;

                case Option.CreateProduct:

                    Console.WriteLine("Add Product Name");
                    var productName = Console.ReadLine();

                    Console.WriteLine("Add quantity");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Add price");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    
                    ADOService.CreateProduct(productName, quantity, price);

                    break;

                case Option.UpdateProduct:
                    
                    ADOService.ReadProduct();
                    
                    Console.WriteLine("Choose the product ID you want to update");
                    int UpdateproductId = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Update Product Name");
                    var UpdateproductName = Console.ReadLine();
                    
                    Console.WriteLine("Update quantity");
                    int Updatequantity = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Update price");
                    decimal Updateprice = Convert.ToDecimal(Console.ReadLine());
                    
                    ADOService.UpdateProduct(UpdateproductId, UpdateproductName, Updatequantity, Updateprice);
                    
                    break;

                case Option.DeleteProduct:
                    
                    ADOService.ReadProduct();
                    
                    Console.WriteLine("Choose the product ID you want to delete");
                    int deleteProductId = Convert.ToInt32(Console.ReadLine());
                    
                    ADOService.DeleteProduct(deleteProductId);
                    
                    break;

                case Option.CreateSale:
                    
                    ADOService.ReadProduct();
                    
                    Console.WriteLine("Choose the product ID you want to buy");
                    int saleProductId = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Enter quantity you want to buy");
                    int saleQuantity = Convert.ToInt32(Console.ReadLine());
                    
                    bool checkQuantity = ADOService.checkProductQuantity(saleProductId, saleQuantity);
                    if (checkQuantity)
                    {
                        ADOService.CreateSale(saleProductId, saleQuantity);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient product quantity");
                    }

                    Console.WriteLine("Create Sale Selected");
                    
                    break;

                case Option.CheckSale:
                    
                    ADOService.ReadSale();
                    
                    break;
                
                default:
                    Console.WriteLine("Invalid Option Selected");
                    break;
            }
            break;

        case DatabaseService.DapperService:

            var dapperService = new DapperService();

            switch (option)
            {

                case Option.CheckProduct:

                    dapperService.ReadProduct();
                    break;

                case Option.CreateProduct:

                    Console.WriteLine("Add Product Name");
                    var productName = Console.ReadLine();
                    
                    Console.WriteLine("Add quantity");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    
                    Console.WriteLine("Add price");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    
                    dapperService.CreateProduct(productName, quantity, price);

                    break;

                case Option.UpdateProduct:

                    dapperService.ReadProduct();
                    
                    Console.WriteLine("Choose the product ID you want to update");
                    int UpdateproductId = Convert.ToInt32(Console.ReadLine());
                   
                    Console.WriteLine("Update Product Name");
                    var UpdateproductName = Console.ReadLine();
                    
                    Console.WriteLine("Update quantity");
                    int Updatequantity = Convert.ToInt32(Console.ReadLine());
                   
                    Console.WriteLine("Update price");
                    decimal Updateprice = Convert.ToDecimal(Console.ReadLine());

                    dapperService.UpdateProduct(UpdateproductId, UpdateproductName, Updatequantity, Updateprice);

                    break;

                case Option.DeleteProduct:

                    dapperService.ReadProduct();
                    
                    Console.WriteLine("Choose the product ID you want to delete");
                    int deleteProductId = Convert.ToInt32(Console.ReadLine());
                    
                    dapperService.DeleteProduct(deleteProductId);

                    break;

                case Option.CreateSale:

                    dapperService.ReadProduct();

                    Console.WriteLine("Choose the product ID you want to buy");
                    int saleProductId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter quantity you want to buy");
                    int saleQuantity = Convert.ToInt32(Console.ReadLine());

                    bool checkQuantity = dapperService.checkQuantity(saleProductId, saleQuantity);
                    if (checkQuantity)
                    {
                        dapperService.CreateSale(saleProductId, saleQuantity);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient product quantity");
                    }

                    Console.WriteLine("Create Sale Selected");

                    break;

                case Option.CheckSale:

                    dapperService.ReadSale();

                    break;

                default:
                    Console.WriteLine("Invalid Option Selected");
                    break;
            }
            break;

        case DatabaseService.EFCoreService:
            var EFCoreService = new EFCoreService();

            switch (option)
            {

                case Option.CheckProduct:

                    EFCoreService.ReadProduct();
                    break;

                case Option.CreateProduct:

                    Console.WriteLine("Add Product Name");
                    var productName = Console.ReadLine();

                    Console.WriteLine("Add quantity");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Add price");
                    decimal price = Convert.ToDecimal(Console.ReadLine());

                    EFCoreService.CreateProduct(productName, quantity, price);

                    break;

                case Option.UpdateProduct:

                    EFCoreService.ReadProduct();

                    Console.WriteLine("Choose the product ID you want to update");
                    int UpdateproductId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Update Product Name");
                    var UpdateproductName = Console.ReadLine();

                    Console.WriteLine("Update quantity");
                    int Updatequantity = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Update price");
                    decimal Updateprice = Convert.ToDecimal(Console.ReadLine());

                    EFCoreService.UpdateProduct(UpdateproductId, UpdateproductName, Updatequantity, Updateprice);

                    break;

                case Option.DeleteProduct:

                    EFCoreService.ReadProduct();

                    Console.WriteLine("Choose the product ID you want to delete");
                    int deleteProductId = Convert.ToInt32(Console.ReadLine());

                    EFCoreService.DeleteProduct(deleteProductId);

                    break;

                case Option.CreateSale:

                    EFCoreService.ReadProduct();

                    Console.WriteLine("Choose the product ID you want to buy");
                    int saleProductId = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter quantity you want to buy");
                    int saleQuantity = Convert.ToInt32(Console.ReadLine());

                    bool checkQuantity = EFCoreService.checkQuantity(saleProductId, saleQuantity);
                    if (checkQuantity)
                    {
                        EFCoreService.CreateSale(saleProductId, saleQuantity);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient product quantity");
                    }

                    Console.WriteLine("Create Sale Selected");

                    break;

                case Option.CheckSale:

                    EFCoreService.ReadSale();

                    break;

                default:
                    Console.WriteLine("Invalid Option Selected");
                    break;
            }

            break;

        default:
            Console.WriteLine("Invalid Service Option");
            break;
    }

    Console.Write("Do you want to continue the system? Press Y: ");
    i = Console.ReadKey().KeyChar;
    Console.Clear();
}



static int WelcomeMessage()
{
    Console.WriteLine(@"Welcome to Mini POS system where you can CRUD product and CR sale
Press
1 to check product
2 to create product
3 to update product
4 to delete product
5 to create sale
6 to check sale");
int result = Convert.ToInt32(Console.ReadLine());
return result;
}


enum Option
{
    CheckProduct = 1,
    CreateProduct = 2,
    UpdateProduct = 3,
    DeleteProduct = 4,
    CreateSale = 5,
    CheckSale = 6
}

enum DatabaseService
{
    ADOService = 1,
    DapperService = 2,
    EFCoreService = 3
}