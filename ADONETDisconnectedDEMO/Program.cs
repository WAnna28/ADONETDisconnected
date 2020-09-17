using ADONETDisconnectedDEMO.DataAccess;
using ADONETDisconnectedDEMO.Models;
using System;
using System.Collections.Generic;

namespace ADONETDisconnectedDEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductDALDC productDAL = new ProductDALDC();

            // InsertProduct
            for (int i = 1; i < 10; i++)
            {
                productDAL.InsertProduct(
                    new Product()
                    { ProductId = i, ProductName = "demo" + i, IntroductionDate = DateTime.Now, Price = i * 10, Url = $"https://demo/{i}" }
                );

                Console.WriteLine($"ResultText for product {i}: {productDAL.ResultText}");
            }

            // GetProductsAsGenericList
            List<Product> products = productDAL.GetProductsAsGenericList();

            Console.WriteLine("\n\nProductId ProductName IntroductionDate\t\tUrl\tPrice");
            foreach (var product in products)
            {
                Console.WriteLine($"  {product.ProductId}\t\t{product.ProductName}\t{product.IntroductionDate.ToShortDateString()}\t{product.Url}\t{product.Price}");
            }
            Console.WriteLine($"{productDAL.ResultText}");

            // GetProductsAsGenericListWithWhere
            products = productDAL.GetProductsAsGenericListWithWhere("demo5");
            Console.WriteLine("\n\nProductId ProductName IntroductionDate\t\tUrl\tPrice");
            foreach (var product in products)
            {
                Console.WriteLine($"  {product.ProductId}\t\t{product.ProductName}\t{product.IntroductionDate.ToShortDateString()}\t{product.Url}\t{product.Price}");
            }
            Console.WriteLine($"{productDAL.ResultText}");

            Console.ReadLine();
        }
    }
}