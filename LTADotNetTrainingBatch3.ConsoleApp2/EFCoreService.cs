using LTADotNetTrainingBatch3.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTADotNetTrainingBatch3.ConsoleApp2
{
    public class EFCoreService 
    {
        
        public void ReadProduct()
        {
            AppDbContext db = new AppDbContext();
            var products = db.TblProducts.ToList();
            foreach (var product in products)
            {
                Console.WriteLine("Product ID: " + product.ProductId + " Product Name:" + product.ProductName + " Quantity: " + product.Quantity + " Price: " + product.Price);
            }

        }

        public void CreateProduct(string productName, int quantity, decimal price)
        {
            AppDbContext db = new AppDbContext();
            var newProduct = new TblProduct()
            {
                ProductName = productName,
                Quantity = quantity,
                Price = price,
                DeleteFlag = false,
                CreatedDateTime = DateTime.Now
            };
            db.TblProducts.Add(newProduct);
            int result = db.SaveChanges();
            string message = result > 0 ? "Create successful" : "Create failed";
            Console.WriteLine(message);
        }

        public void UpdateProduct(int productID, string productName, int quantity, decimal price)
        {
            AppDbContext db = new AppDbContext();
            var existingProduct = db.TblProducts.FirstOrDefault(p => p.ProductId == productID);
            if (existingProduct != null)
            {
                existingProduct.ProductName = productName;
                existingProduct.Quantity = quantity;
                existingProduct.Price = price;
                existingProduct.ModifiedDateTime = DateTime.Now;
                
                int result = db.SaveChanges();
                string message = result > 0 ? "Update successful" : "Update failed";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void DeleteProduct(int productID)
        {
            AppDbContext db = new AppDbContext();
            var existingProduct = db.TblProducts.FirstOrDefault(p => p.ProductId == productID);
            if (existingProduct != null)
            {
                db.TblProducts.Remove(existingProduct);
                int result = db.SaveChanges();
                string message = result > 0 ? "Delete successful" : "Delete failed";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        decimal ProductPrice;
        public bool checkQuantity(int productId, int quantity) { 
        
        AppDbContext db = new AppDbContext();
            var checkquanitity = db.TblProducts.FirstOrDefault(p => p.ProductId == productId);
            if (checkquanitity != null) {
                if (checkquanitity.Quantity > quantity) {
                    ProductPrice = checkquanitity.Price;
                return true;
                }
            }
            return false;
        }

        public void ReadSale()
        {
            AppDbContext db = new AppDbContext();
            var sale = db.TblSales.ToList();
            foreach (var sales in sale) { 
            Console.WriteLine(" Product ID:" + sales.ProductId + " Quantity: " + sales.Quantity + " Price: " + sales.Price );
        
            }
        }

        public void CreateSale(int productID, int quantity)
        {
            AppDbContext db = new AppDbContext();
            var newSale = new TblSale()
            {
                ProductId = productID,
                Quantity = quantity,
                Price = ProductPrice,
                DeleteFlag = false,
                CreatedDateTime = DateTime.Now
            };
            db.TblSales.Add(newSale);
            int result = db.SaveChanges();
            string message = result > 0 ? "Create successful" : "Create failed";

            //Update quanitity of product
            var updateQty = db.TblProducts.FirstOrDefault(db => db.ProductId == productID);
            if (updateQty != null) {
                updateQty.Quantity = updateQty.Quantity - quantity;
                db.SaveChanges();
            }

            Console.WriteLine(message);
        }
    }
}
