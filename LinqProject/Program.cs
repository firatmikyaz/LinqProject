using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Gerçek sistemde hazır gelir
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1,CategoryName="Bilgisayar"},
                new Category{CategoryId=2,CategoryName="Telefon"}
            };
            //Gerçek sistemde hazır gelir.
            List<Product> products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Acer Laptop",QuantityPerUnit="32 gb ram",UnitPrice=10000,UnitsInStock=5},
                new Product{ProductId=2,CategoryId=1,ProductName="Asus Laptop",QuantityPerUnit="16 gb ram",UnitPrice=18000,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=1,ProductName="Hp Laptop",QuantityPerUnit="8 gb ram",UnitPrice=18000,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Samsung Telefon",QuantityPerUnit="4 gb ram",UnitPrice=5000,UnitsInStock=15},
                new Product{ProductId=5,CategoryId=2,ProductName="Apple Telefon",QuantityPerUnit="4 gb ram",UnitPrice=8000,UnitsInStock=0}
            };

            //Test(products);
            //GetProducts(products);

            //AnyTest(products);

            //FindTest(products);

            //FindAllTest(products);

            //AscDescTest(products);

            //classicLinqTest(products);

            //Join ile Linq yazımı
            //Enumareble : itereyit edilebilir demek,yani array tabanlı yapı
            //Product ve Category classlarını CategoryId üzerinden ilişkilendirip,birleştireceğiz.

            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice>5000
                         orderby p.UnitPrice descending
                         select new ProductDto { ProductId = p.ProductId, CategoryName = c.CategoryName, ProductName = p.ProductName, UnitPrice = p.UnitPrice };
            foreach (var productDto in result)
            {
                //2.yazım şekli 'String İnterpolasyon'
                Console.WriteLine("{0}---{1}", productDto.ProductName, productDto.CategoryName);
            }
        }

        private static void classicLinqTest(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000 //Fiyatı 6000'den fazla olanları getir
                         orderby p.UnitPrice descending, p.ProductName descending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice,};//Bu alan yok! O yüzden Join!
                                                                                                                                                   //ProductId,ProductName ve unitPrice ProductDto'nun alanları
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //İçerisinde 'top' kemlimesi geçen ürün isimlerini getir.
            var result = products.Where(p => p.ProductName.Contains("top")).OrderByDescending(p => p.UnitPrice).ThenByDescending(p => p.ProductName);
            foreach (var product in result)               //Fiyat azalana göre sırala ve Z'den A'ya sırala
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            //Listenin içerisinden bir string ifade bulalım.
            var result = products.FindAll(p => p.ProductName.Contains("top"));
            Console.WriteLine(result); //Bir list of liste döner
        }

        private static void FindTest(List<Product> products)
        {
            //Bir ürünün detayına gitmek için Find kullanırız.Bir product döner
            var result = products.Find(p => p.ProductId == 3);
            Console.WriteLine(result.ProductName); //Result'un productName'i döner
        }

        private static void AnyTest(List<Product> products)
        {
            //aradığın elemandan hiç var mı sorgusunu yapar 'Any'.Sonu. Bool döner
            var result = products.Any(p => p.ProductName == "Acer Laptop");
            Console.WriteLine(result);
        }

        private static void Test(List<Product> products)
        {
            Console.WriteLine("Algoritmik---");
            //1-ürün isimlerini listeleyelim.
            /*foreach (var product in products)
            {
                Console.WriteLine(product.ProductName);
            }*/

            //2-Ürün fiyatı 5000 bin üstü olanlar
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 & product.UnitsInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
            Console.WriteLine("Linq--------");
            var result = products.Where(p => p.UnitPrice > 5000 & p.UnitsInStock > 3);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        static List<Product> GetProducts(List<Product> products)
        {
            //Dönen her bir ürün yeni listeye eklemem gerek.Linq'suz bir çözüm
            List<Product> filteredProducts = new List<Product>();
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 & product.UnitsInStock > 3)
                {
                    filteredProducts.Add(product);
                }
            }
            return filteredProducts;
        }
        static List<Product> GetProductsLinq(List<Product> products)
        {
            return products.Where(p => p.UnitPrice > 5000 & p.UnitsInStock > 3).ToList();
        }

        //Join ve Dto konusu
        class ProductDto //Select p yerine belli alanları çekmek istiyorum.
        {
            public int ProductId { get; set; }
            public string CategoryName { get; set; } //Bu alan Product'ta yok.
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
        }

        //ürünlerle çalışacağım,bir Liste formatında oluşturacağım.
        class Product
        {
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public string ProductName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
        }
        class Category
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
        }
    }
}
