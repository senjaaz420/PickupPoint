using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DDDiplom.Models;
using Microsoft.EntityFrameworkCore;

namespace DDDiplom.Services.GenerationRandomOrders
{
    public class GenerationRandomOrders
    {
        //private Random r = new Random();

        //private string Names = "../DDDiplom/wwwroot/txt/Names.txt";
        //private string SurNames = "../DDDiplom/wwwroot/txt/SurNames.txt";
        //private string SecondNames = "../DDDiplom/wwwroot/txt/SecondNames.txt";

        //public GenerationRandomOrders()
        //{

        //    var startTimeSpan = TimeSpan.Zero;
        //    var periodTimeSpan = TimeSpan.FromMinutes(20);

        //    var timer = new System.Threading.Timer((e) =>
        //    {
        //        Generate();
        //    }, null, startTimeSpan, periodTimeSpan);
        //}


        //private string GetRandomStringFromFile(string fileName)
        //{
        //    var file = File.ReadLines(fileName).ToList();
        //    int count = file.Count();
        //    int skip = r.Next(0, count);
        //    string line = file.Skip(skip).First();
        //    return line;
        //}

        //public void Generate()
        //{

        //    List<Product> goodsList = new List<Product>();
        //    Product randomItem;

        //    using (DDDiplomContext db = new DDDiplomContext())
        //    {

        //        for (int i = 0; i < r.Next(1,9); i++)
        //        {
        //            randomItem = db.Products.ToArray()[r.Next(db.Products.Count())];
        //            goodsList.Add(randomItem);
        //        }

        //        int clientId = r.Next(1, 99999);
        //        Client client = new Client
        //        {
        //            //Id = clientId,
        //            Name = GetRandomStringFromFile(Names),
        //            Surname = GetRandomStringFromFile(SurNames),
        //            Secondname = GetRandomStringFromFile(SecondNames),
        //            PhoneNumber = "+37529" + r.Next(1111111, 9999999).ToString()
        //        };

        //        WorkPlace workPlace = db.WorkPlaces.ToArray()[r.Next(db.WorkPlaces.Count())];

        //        var clientID = client.Id;

        //        Order order = new Order
        //        {
        //            //Id = db.Orders.LastOrDefault().Id + 1,
        //            IsPaid = "нет",
        //            OrderTime = DateTime.Now,
        //            OrderProducts = goodsList,
        //            ClientId = clientID,
        //            Client = client,
        //            Summary = goodsList.Sum(x => x.Price),
        //            WorkPlace = workPlace,
        //            WorkPlaceId = workPlace.Id

        //        };

        //        foreach (var item in goodsList)
        //        {
        //            db.ProductLists.Add(new ProductList
        //            {
        //                CategoryId = item.Category != null ? item.Category.Id : 0,
        //                Name = item.Name,
        //                OrderdId = clientID,
        //                Price = item.Price
        //            });
        //        }
                
        //        //db.Orders.Add(order);

        //        Debug.WriteLine(DateTime.Now + " New Order was added");

        //        db.SaveChanges();
        //    }



        //}

    }
}
